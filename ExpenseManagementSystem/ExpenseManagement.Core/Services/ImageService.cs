using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using ExpenseManagement.Core.Interfaces;
using ExpenseManagement.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseManagement.Core.Services
{
    public class ImageService : IImageService
    {
        private readonly IConfiguration _config;
        private readonly Cloudinary _cloudinary;
        private readonly ImageUploadSettings _accountSettings;
        public ImageService(IConfiguration config, IOptions<ImageUploadSettings> accountSettings,
            Cloudinary cloudinary)
        {
            _accountSettings = accountSettings.Value;
            _config = config;
            _cloudinary = cloudinary;
        }
        public async Task<UploadResult> UploadAsync(IFormFile image)
        {
            var pictureSize = Convert.ToInt64(_config.GetSection("PhotoSettings:Size").Get<string>());
            if (image.Length > pictureSize)
            {
                throw new ArgumentException("File size exceeded");
            }
            var pictureFormat = false;

            var listOfImageExtensions = _config.GetSection("PhotoSettings:Formats").Get<List<string>>();

            foreach (var item in listOfImageExtensions)
            {
                if (image.FileName.EndsWith(item))
                {
                    pictureFormat = true;
                    break;
                }

            }

            if (pictureFormat == false)
            {
                throw new ArgumentException("File format not supported");
            }

            var uploadResult = new ImageUploadResult();

            //fetch the image using image stream
            using (var imageStream = image.OpenReadStream())
            {
                string filename = Guid.NewGuid().ToString() + image.FileName;
                uploadResult = await _cloudinary.UploadAsync(new ImageUploadParams()
                {
                    File = new FileDescription(filename, imageStream),
                    Transformation = new Transformation().Crop("thumb").Gravity("face").Width(150)
                });
            }
            return uploadResult;
        }
    }
}
