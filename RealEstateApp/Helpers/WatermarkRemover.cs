using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.PixelFormats;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System;

namespace RealEstateApp.Helpers
{
    public class WatermarkRemover
    {
        private readonly ILogger<WatermarkRemover> _logger;

        public WatermarkRemover(ILogger<WatermarkRemover>? logger = null)
        {
            _logger = logger ?? NullLogger<WatermarkRemover>.Instance;
        }

        public async Task<bool> RemoveWatermarkAsync(Stream inputStream, Stream outputStream)
        {
            try
            {
                // Load the image
                using var image = await Image.LoadAsync(inputStream);

                // Convert to Rgba32 format for processing
                var rgbaImage = image.CloneAs<Rgba32>();

                // Detect and remove watermark
                bool watermarkRemoved = RemoveWatermark(rgbaImage);

                // Save the processed image
                await rgbaImage.SaveAsync(outputStream, image.Metadata.DecodedImageFormat);

                return watermarkRemoved;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error removing watermark from image");

                // If something fails, just copy the original image
                inputStream.Position = 0;
                await inputStream.CopyToAsync(outputStream);

                return false;
            }
        }

        private bool RemoveWatermark(Image<Rgba32> image)
        {
            try
            {
                // Get dimensions
                int width = image.Width;
                int height = image.Height;

                // Common watermark locations are bottom-right, center, and all corners
                // Here we'll implement a few strategies to detect and remove watermarks

                // 1. Check for semi-transparent overlay in the corners
                bool watermarkDetected = false;

                // Bottom right corner (common watermark location)
                if (DetectCornerWatermark(image, Corner.BottomRight))
                {
                    RemoveCornerWatermark(image, Corner.BottomRight);
                    watermarkDetected = true;
                }

                // Bottom left corner
                if (DetectCornerWatermark(image, Corner.BottomLeft))
                {
                    RemoveCornerWatermark(image, Corner.BottomLeft);
                    watermarkDetected = true;
                }

                // Center watermark detection (more complex)
                if (DetectCenterWatermark(image))
                {
                    RemoveCenterWatermark(image);
                    watermarkDetected = true;
                }

                // 2. Look for repeating patterns (tiled watermarks)
                if (DetectTiledWatermark(image))
                {
                    RemoveTiledWatermark(image);
                    watermarkDetected = true;
                }

                return watermarkDetected;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in watermark removal algorithm");
                return false;
            }
        }

        private bool DetectCornerWatermark(Image<Rgba32> image, Corner corner)
        {
            // Simple detection of semi-transparent areas in corners
            int sampleSize = (int)(Math.Min(image.Width, image.Height) * 0.15); // 15% of the smallest dimension
            int startX, startY;

            switch (corner)
            {
                case Corner.TopLeft:
                    startX = 0;
                    startY = 0;
                    break;
                case Corner.TopRight:
                    startX = image.Width - sampleSize;
                    startY = 0;
                    break;
                case Corner.BottomLeft:
                    startX = 0;
                    startY = image.Height - sampleSize;
                    break;
                case Corner.BottomRight:
                default:
                    startX = image.Width - sampleSize;
                    startY = image.Height - sampleSize;
                    break;
            }

            // Count semi-transparent pixels
            int semiTransparentCount = 0;
            int totalPixels = 0;

            for (int y = startY; y < startY + sampleSize && y < image.Height; y++)
            {
                for (int x = startX; x < startX + sampleSize && x < image.Width; x++)
                {
                    var pixel = image[x, y];

                    // Check if pixel is semi-transparent (0.1 < alpha < 0.9)
                    if (pixel.A > 25 && pixel.A < 230)
                    {
                        semiTransparentCount++;
                    }

                    totalPixels++;
                }
            }

            // If more than 5% of pixels are semi-transparent, likely a watermark
            return semiTransparentCount > totalPixels * 0.05;
        }

        private void RemoveCornerWatermark(Image<Rgba32> image, Corner corner)
        {
            int areaSize = (int)(Math.Min(image.Width, image.Height) * 0.2); // 20% of the smallest dimension
            int startX, startY;

            switch (corner)
            {
                case Corner.TopLeft:
                    startX = 0;
                    startY = 0;
                    break;
                case Corner.TopRight:
                    startX = image.Width - areaSize;
                    startY = 0;
                    break;
                case Corner.BottomLeft:
                    startX = 0;
                    startY = image.Height - areaSize;
                    break;
                case Corner.BottomRight:
                default:
                    startX = image.Width - areaSize;
                    startY = image.Height - areaSize;
                    break;
            }

            // Create inpainting mask (white = areas to inpaint)
            using var mask = new Image<L8>(image.Width, image.Height);
            mask.Mutate(x => x.Fill(Color.Black)); // Start with all black

            // Mark watermark area as white in the mask
            for (int y = startY; y < startY + areaSize && y < image.Height; y++)
            {
                for (int x = startX; x < startX + areaSize && x < image.Width; x++)
                {
                    var pixel = image[x, y];

                    // If semi-transparent or full transparency, mark for inpainting
                    if (pixel.A < 230)
                    {
                        mask[x, y] = new L8(255);
                    }
                }
            }

            // Simple watermark removal by content-aware fill (simplified inpainting)
            for (int y = startY; y < startY + areaSize && y < image.Height; y++)
            {
                for (int x = startX; x < startX + areaSize && x < image.Width; x++)
                {
                    if (mask[x, y].PackedValue > 128) // If marked for inpainting
                    {
                        // Simple approach: Sample from nearby non-watermarked pixels
                        // In a real implementation, more sophisticated inpainting would be used
                        var referencePoint = GetNearestReferencePoint(x, y, mask);
                        if (referencePoint.HasValue)
                        {
                            image[x, y] = image[referencePoint.Value.X, referencePoint.Value.Y];
                        }
                        else
                        {
                            // If no reference found, set to fully opaque
                            image[x, y] = new Rgba32(image[x, y].R, image[x, y].G, image[x, y].B, 255);
                        }
                    }
                }
            }
        }

        private bool DetectCenterWatermark(Image<Rgba32> image)
        {
            // For center watermarks, check a region in the center
            int centerX = image.Width / 2;
            int centerY = image.Height / 2;
            int sampleSize = (int)(Math.Min(image.Width, image.Height) * 0.3); // 30% of the smallest dimension

            int startX = centerX - sampleSize / 2;
            int startY = centerY - sampleSize / 2;

            // Count semi-transparent pixels
            int semiTransparentCount = 0;
            int totalPixels = 0;

            for (int y = startY; y < startY + sampleSize && y < image.Height; y++)
            {
                for (int x = startX; x < startX + sampleSize && x < image.Width; x++)
                {
                    if (x >= 0 && y >= 0 && x < image.Width && y < image.Height)
                    {
                        var pixel = image[x, y];

                        // Check if pixel is semi-transparent
                        if (pixel.A > 25 && pixel.A < 230)
                        {
                            semiTransparentCount++;
                        }

                        totalPixels++;
                    }
                }
            }

            // If more than 5% of pixels are semi-transparent, likely a watermark
            return semiTransparentCount > totalPixels * 0.05;
        }

        private void RemoveCenterWatermark(Image<Rgba32> image)
        {
            // Similar approach to corner watermark removal, but for center region
            int centerX = image.Width / 2;
            int centerY = image.Height / 2;
            int areaSize = (int)(Math.Min(image.Width, image.Height) * 0.35); // 35% of the smallest dimension

            int startX = centerX - areaSize / 2;
            int startY = centerY - areaSize / 2;

            // Create inpainting mask
            using var mask = new Image<L8>(image.Width, image.Height);
            mask.Mutate(x => x.Fill(Color.Black));

            // Mark center watermark area in mask
            for (int y = startY; y < startY + areaSize && y < image.Height; y++)
            {
                for (int x = startX; x < startX + areaSize && x < image.Width; x++)
                {
                    if (x >= 0 && y >= 0 && x < image.Width && y < image.Height)
                    {
                        var pixel = image[x, y];

                        if (pixel.A < 230)
                        {
                            mask[x, y] = new L8(255);
                        }
                    }
                }
            }

            // Perform inpainting
            for (int y = startY; y < startY + areaSize && y < image.Height; y++)
            {
                for (int x = startX; x < startX + areaSize && x < image.Width; x++)
                {
                    if (x >= 0 && y >= 0 && x < image.Width && y < image.Height)
                    {
                        if (mask[x, y].PackedValue > 128)
                        {
                            var referencePoint = GetNearestReferencePoint(x, y, mask);
                            if (referencePoint.HasValue)
                            {
                                image[x, y] = image[referencePoint.Value.X, referencePoint.Value.Y];
                            }
                            else
                            {
                                image[x, y] = new Rgba32(image[x, y].R, image[x, y].G, image[x, y].B, 255);
                            }
                        }
                    }
                }
            }
        }

        private bool DetectTiledWatermark(Image<Rgba32> image)
        {
            // Simplified detection of tiled watermarks
            // This would be very complex in a real system and would use frequency analysis

            // For this example, we'll just check a grid of points for semi-transparency
            int gridSize = Math.Min(image.Width, image.Height) / 10;
            if (gridSize < 5) gridSize = 5;

            int semiTransparentPoints = 0;
            int totalPoints = 0;

            for (int y = gridSize; y < image.Height; y += gridSize)
            {
                for (int x = gridSize; x < image.Width; x += gridSize)
                {
                    var pixel = image[x, y];

                    if (pixel.A > 25 && pixel.A < 230)
                    {
                        semiTransparentPoints++;
                    }

                    totalPoints++;
                }
            }

            // If more than 15% of grid points are semi-transparent, likely a tiled watermark
            return semiTransparentPoints > totalPoints * 0.15;
        }

        private void RemoveTiledWatermark(Image<Rgba32> image)
        {
            // For tiled watermarks, we need to address the entire image
            // Create a mask for the entire image
            using var mask = new Image<L8>(image.Width, image.Height);
            mask.Mutate(x => x.Fill(Color.Black));

            // Mark all semi-transparent pixels
            for (int y = 0; y < image.Height; y++)
            {
                for (int x = 0; x < image.Width; x++)
                {
                    var pixel = image[x, y];

                    if (pixel.A > 25 && pixel.A < 230)
                    {
                        mask[x, y] = new L8(255);
                    }
                }
            }

            // Apply median filter to reduce watermark without full inpainting
            image.Mutate(x => x.MedianBlur(3));

            // Fix remaining semi-transparent pixels
            for (int y = 0; y < image.Height; y++)
            {
                for (int x = 0; x < image.Width; x++)
                {
                    if (mask[x, y].PackedValue > 128)
                    {
                        var pixel = image[x, y];
                        image[x, y] = new Rgba32(pixel.R, pixel.G, pixel.B, 255);
                    }
                }
            }
        }

        private Point? GetNearestReferencePoint(int x, int y, Image<L8> mask)
        {
            // Look for a nearby pixel that is not marked for inpainting
            // Start with small radius and increase
            for (int radius = 3; radius < 50; radius += 2)
            {
                for (int dy = -radius; dy <= radius; dy++)
                {
                    for (int dx = -radius; dx <= radius; dx++)
                    {
                        // Only check points on the approximate circle perimeter
                        if (dx * dx + dy * dy >= radius * radius - radius && dx * dx + dy * dy <= radius * radius + radius)
                        {
                            int nx = x + dx;
                            int ny = y + dy;

                            if (nx >= 0 && ny >= 0 && nx < mask.Width && ny < mask.Height)
                            {
                                if (mask[nx, ny].PackedValue < 128) // Not marked for inpainting
                                {
                                    return new Point(nx, ny);
                                }
                            }
                        }
                    }
                }
            }

            return null; // No good reference found
        }

        private enum Corner
        {
            TopLeft,
            TopRight,
            BottomLeft,
            BottomRight
        }
    }
}
