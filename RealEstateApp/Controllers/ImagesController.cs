using Microsoft.AspNetCore.Mvc;
using RealEstateApp.Services;
using System.Collections.Generic;
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace RealEstateApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly ImageService _imageService;
        private readonly ILogger<ImagesController> _logger;

        public ImagesController(
            ImageService imageService,
            ILogger<ImagesController> logger)
        {
            _imageService = imageService;
            _logger = logger;
        }

        // POST: api/Images/Upload/{propertyId}
        [HttpPost("Upload/{propertyId}")]
        public async Task<IActionResult> Upload(int propertyId, List<IFormFile> files)
        {
            try
            {
                if (files == null || files.Count == 0)
                {
                    return BadRequest("No files uploaded.");
                }

                var results = await _imageService.ProcessAndSaveImagesAsync(propertyId, files);
                return Ok(new { SuccessCount = results.Count, Message = "Images uploaded successfully" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error uploading images for property {PropertyId}", propertyId);
                return StatusCode(500, "An error occurred while processing your request. Please try again.");
            }
        }

        // DELETE: api/Images/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var result = await _imageService.DeleteImageAsync(id);
                if (result)
                {
                    return Ok(new { Success = true, Message = "Image deleted successfully" });
                }
                else
                {
                    return NotFound(new { Success = false, Message = "Image not found" });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting image {ImageId}", id);
                return StatusCode(500, "An error occurred while processing your request. Please try again.");
            }
        }
    }
}
