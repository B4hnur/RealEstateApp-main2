using Microsoft.EntityFrameworkCore;
using RealEstateApp.Data;
using RealEstateApp.Helpers;
using RealEstateApp.Models;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using System;

namespace RealEstateApp.Services
{
    public class ImageService
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _environment;
        private readonly IConfiguration _configuration;
        private readonly ILogger<ImageService> _logger;
        private readonly WatermarkRemover _watermarkRemover;
        private readonly string _uploadPath;
        private readonly long _maxFileSize;

        public ImageService(
            ApplicationDbContext context,
            IWebHostEnvironment environment,
            IConfiguration configuration,
            ILogger<ImageService> logger)
        {
            _context = context;
            _environment = environment;
            _configuration = configuration;
            _logger = logger;
            _watermarkRemover = new WatermarkRemover();

            // Get settings from configuration
            _uploadPath = _configuration.GetValue<string>("Storage:UploadPath") ?? "wwwroot/uploads";
            _maxFileSize = _configuration.GetValue<long>("Storage:MaxFileSizeMB", 10) * 1024 * 1024; // Convert MB to bytes

            // Ensure upload directory exists
            var fullPath = Path.Combine(_environment.ContentRootPath, _uploadPath);
            if (!Directory.Exists(fullPath))
            {
                Directory.CreateDirectory(fullPath);
            }
        }

        public async Task<List<PropertyImage>> ProcessAndSaveImagesAsync(int propertyId, List<IFormFile> files)
        {
            var savedImages = new List<PropertyImage>();

            // Check if property exists
            var property = await _context.Properties.FindAsync(propertyId);
            if (property == null)
            {
                throw new ArgumentException($"Property with ID {propertyId} not found.");
            }

            foreach (var file in files)
            {
                try
                {
                    // Validate file
                    if (file.Length <= 0 || file.Length > _maxFileSize)
                    {
                        _logger.LogWarning("File size {Size} for {FileName} is invalid", file.Length, file.FileName);
                        continue;
                    }

                    // Check if it's an image
                    if (!IsImageFile(file.FileName))
                    {
                        _logger.LogWarning("File {FileName} is not an image", file.FileName);
                        continue;
                    }

                    // Create unique filename
                    var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
                    var fileName = $"{Guid.NewGuid()}{extension}";
                    var filePath = Path.Combine(_environment.ContentRootPath, _uploadPath, fileName);

                    // Process image and remove watermark
                    bool watermarkRemoved = false;

                    using (var stream = new MemoryStream())
                    {
                        await file.CopyToAsync(stream);
                        stream.Position = 0;

                        // Process image to remove watermark
                        using (var outputStream = new FileStream(filePath, FileMode.Create))
                        {
                            watermarkRemoved = await _watermarkRemover.RemoveWatermarkAsync(stream, outputStream);
                        }
                    }

                    // Create and save the database record
                    var image = new PropertyImage
                    {
                        PropertyId = propertyId,
                        FileName = fileName,
                        OriginalFileName = file.FileName,
                        ContentType = file.ContentType,
                        FileSize = file.Length,
                        FileExtension = extension,
                        IsWatermarkRemoved = watermarkRemoved,
                        IsPrimary = !(await _context.PropertyImages.AnyAsync(i => i.PropertyId == propertyId)),
                        UploadedAt = DateTime.Now
                    };

                    _context.PropertyImages.Add(image);
                    await _context.SaveChangesAsync();

                    savedImages.Add(image);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error processing image {FileName} for property {PropertyId}",
                        file.FileName, propertyId);
                }
            }

            return savedImages;
        }

        public async Task<bool> DeleteImageAsync(int imageId)
        {
            var image = await _context.PropertyImages.FindAsync(imageId);

            if (image == null)
                return false;

            try
            {
                // Delete file from disk
                var filePath = Path.Combine(_environment.ContentRootPath, _uploadPath, image.FileName);
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }

                // Remove from database
                _context.PropertyImages.Remove(image);
                await _context.SaveChangesAsync();

                // If it was primary, set another one as primary
                if (image.IsPrimary)
                {
                    var nextImage = await _context.PropertyImages
                        .FirstOrDefaultAsync(i => i.PropertyId == image.PropertyId);

                    if (nextImage != null)
                    {
                        nextImage.IsPrimary = true;
                        await _context.SaveChangesAsync();
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting image {ImageId}", imageId);
                return false;
            }
        }

        public async Task<bool> SetPrimaryImageAsync(int imageId)
        {
            var image = await _context.PropertyImages.FindAsync(imageId);

            if (image == null)
                return false;

            try
            {
                // Clear primary status from all other images for this property
                var propertyImages = await _context.PropertyImages
                    .Where(i => i.PropertyId == image.PropertyId)
                    .ToListAsync();

                foreach (var img in propertyImages)
                {
                    img.IsPrimary = (img.Id == imageId);
                }

                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error setting primary image {ImageId}", imageId);
                return false;
            }
        }

        private bool IsImageFile(string fileName)
        {
            var extension = Path.GetExtension(fileName).ToLowerInvariant();
            return extension == ".jpg" || extension == ".jpeg" ||
                   extension == ".png" || extension == ".gif" ||
                   extension == ".bmp" || extension == ".webp";
        }
    }
}
