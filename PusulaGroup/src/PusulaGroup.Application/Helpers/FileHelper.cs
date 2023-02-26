using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using PusulaGroup.Domain.Constants;
using System;

namespace PusulaGroup.Application.Helpers
{
    public class ImageHelper
    {
        private readonly IWebHostEnvironment environment;

        public ImageHelper(IWebHostEnvironment environment)
        {
            this.environment = environment;
        }
       
        public bool IsExist(string imagePath)
        {
            var wwwroot = environment.WebRootPath;
            var path = Path.Combine(wwwroot, imagePath);

            return File.Exists(path);
        }

        public string GetFullPath(string imagePath)
        {
            var wwwroot = environment.WebRootPath;
            var path = Path.Combine(wwwroot, imagePath);

            if (!File.Exists(path))
            {
                path = Path.Combine(wwwroot, PusulaGroupConstants.ImagePathNoImage);
            }

            return path;
        }
    }
}
