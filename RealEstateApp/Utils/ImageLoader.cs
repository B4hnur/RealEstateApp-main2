using System;
using System.Drawing;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RealEstateApp.Utils
{
    public static class ImageLoader
    {
        private static readonly WebClient WebClient = new WebClient();

        public static async Task<Image> LoadImageAsync(string imageUrl)
        {
            if (string.IsNullOrEmpty(imageUrl))
                return null;

            try
            {
                byte[] imageData = await WebClient.DownloadDataTaskAsync(imageUrl);
                using (MemoryStream ms = new MemoryStream(imageData))
                {
                    return Image.FromStream(ms);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading image: {ex.Message}");
                return null;
            }
        }

        public static void LoadImageIntoPictureBox(string imageUrl, PictureBox pictureBox, Image defaultImage = null)
        {
            if (string.IsNullOrEmpty(imageUrl))
            {
                pictureBox.Image = defaultImage;
                return;
            }

            pictureBox.Image = defaultImage;

            Task.Run(async () =>
            {
                try
                {
                    var image = await LoadImageAsync(imageUrl);

                    if (pictureBox.InvokeRequired)
                    {
                        pictureBox.Invoke(new Action(() =>
                        {
                            pictureBox.Image = image ?? defaultImage;
                            pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
                        }));
                    }
                    else
                    {
                        pictureBox.Image = image ?? defaultImage;
                        pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error loading image into picture box: {ex.Message}");

                    if (pictureBox.InvokeRequired)
                    {
                        pictureBox.Invoke(new Action(() => pictureBox.Image = defaultImage));
                    }
                    else
                    {
                        pictureBox.Image = defaultImage;
                    }
                }
            });
        }

        public static void CancelPendingImageLoads()
        {
            try
            {
                WebClient.CancelAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error canceling image loads: {ex.Message}");
            }
        }
    }
}
