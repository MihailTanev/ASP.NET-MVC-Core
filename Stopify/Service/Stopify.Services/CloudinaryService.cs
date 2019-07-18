namespace Stopify.Services
{
    using System.IO;
    using System.Threading.Tasks;
    using CloudinaryDotNet;
    using CloudinaryDotNet.Actions;
    using Microsoft.AspNetCore.Http;

    public class CloudinaryService : ICloudinaryService
    {
        private readonly Cloudinary cloudinary;

        public CloudinaryService(Cloudinary cloudinary)
        {
            this.cloudinary = cloudinary;
        }

        public async Task<string> UploadPictureAsync(IFormFile pictureFile, string fileName)
        {
            byte[] content;

            using(var ms = new MemoryStream())
            {
                await pictureFile.CopyToAsync(ms);
                content = ms.ToArray();
            }

            UploadResult uploadResult = null;

            using(var ms=new MemoryStream(content))
            {
                ImageUploadParams imageUploadParams = new ImageUploadParams
                {
                    Folder = "product_images",
                    File = new FileDescription(fileName,ms)
                };

                uploadResult = this.cloudinary.Upload(imageUploadParams);
             }

            return uploadResult?.SecureUri.AbsoluteUri;
        }
    }
}
