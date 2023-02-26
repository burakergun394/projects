using ImageProcessor;
using ImageProcessor.Imaging;
using PusulaGroup.WebApp.Domain.Constants;
using System.Drawing;
using System.IO;
using System.IO.Pipes;
using System.Text;

namespace PusulaGroup.WebApp.Core.Helpers
{
    public class ImageHelper
    {
        private readonly IWebHostEnvironment hostEnvironment;

        public ImageHelper(IWebHostEnvironment hostEnvironment)
        {
            this.hostEnvironment = hostEnvironment;
        }

        public bool IsImageExtensionValid(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return false;

            var extension = Path.GetExtension(file.FileName);

            return extension == ".png"
                || extension == ".jpeg"
                || extension == ".jpg";
        }

        public async Task<string> SaveImageAsync(IFormFile file, string parentFolderName, string childFolderName, int width = 0, int height = 0)
        {
            if (file == null || file.Length == 0)
                return string.Empty;

            var folderPath = Path.Combine(hostEnvironment.WebRootPath, parentFolderName, childFolderName);
            var extension = Path.GetExtension(file.FileName);
            var fileName = $"{Guid.NewGuid()}{extension}";
            var filePath = Path.Combine(folderPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create, FileAccess.ReadWrite))
            {
                await file.CopyToAsync(stream);
            }

            using (ImageFactory imageFactory = new(preserveExifData: true))
            {
                imageFactory.Load(filePath)
                            .Quality(80)
                            .Resize(new ResizeLayer(new Size(width, height), ResizeMode.Pad, AnchorPosition.Center))
                            .BackgroundColor(Color.Transparent)
                            .Save(filePath);
            }

            return $"/{parentFolderName}/{childFolderName}/{fileName}";
        }

        public void DeleteImage(string path)
        {
            if (string.IsNullOrEmpty(path))
                return;

            StringBuilder sb = new(path);
            if (path.StartsWith("/"))
                sb.Remove(0, 1);
            sb.Replace("/", "\\");
            var filePath = Path.Combine(hostEnvironment.WebRootPath, sb.ToString());
            if (File.Exists(filePath))
            {
                File.SetAttributes(filePath, FileAttributes.Normal);
                File.Delete(filePath);
            }
        }
    }
}
