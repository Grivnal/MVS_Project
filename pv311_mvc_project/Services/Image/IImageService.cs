namespace pv311_mvc_project.Services.Image
{
    public interface IImageService
    {
        Task<string?> SaveImageAsync(IFormFile image, string path);
        bool DeleteImage(string path);
    }
}