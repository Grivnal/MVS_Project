
using Microsoft.AspNetCore.Hosting;

namespace pv311_mvc_project.Services.Image
{
    public class ImageService(IWebHostEnvironment webHostEnvironment) : IImageService
    {
        public bool DeleteImage(string path)
        {
            string root = webHostEnvironment.WebRootPath;
            string imagePath = Path.Combine(root, path);

            if (File.Exists(imagePath))
            {
                File.Delete(imagePath);
                return true;
            }

            return false;
        }

        public async Task<string?> SaveImageAsync(IFormFile image, string path)
        {
            try
            {
                var types = image.ContentType.Split("/");

                if (types[0] != "image")
                {
                    return null;
                }

                string root = webHostEnvironment.WebRootPath;
                string imageName = $"{Guid.NewGuid()}.{types[1]}";
                string imagePath = Path.Combine(root, path, imageName);

                using (var fileStream = File.Create(imagePath))
                {
                    using var stream = image.OpenReadStream();
                    await stream.CopyToAsync(fileStream);
                }

                return imageName;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}