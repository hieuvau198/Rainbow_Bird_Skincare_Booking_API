using Application.Interfaces;
using Google.Cloud.Storage.V1;
using Microsoft.AspNetCore.Http;
using Google.Apis.Storage.v1.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Google.Apis.Auth.OAuth2;

namespace Application.Services
{
    public class FirebaseImageService : IImageService
    {
        private readonly StorageClient _storageClient;
        private readonly string _bucketName;

        public FirebaseImageService(IConfiguration configuration)
        {
            _bucketName = "flipbook-firebase.appspot.com";

            // Retrieve service account JSON from configuration
            var firebaseServiceAccountJson = configuration["Firebase:ServiceAccountJson"];

            if (string.IsNullOrEmpty(firebaseServiceAccountJson))
            {
                throw new InvalidOperationException("Service account JSON is required.");
            }

            // Set up the Google credential from the service account
            var googleCredential = GoogleCredential.FromJson(firebaseServiceAccountJson);
            _storageClient = StorageClient.Create(googleCredential);
        }

        public async Task<string> UploadImageAsync(IFormFile image)
        {
            if (image == null || image.Length == 0)
                return null;

            try
            {
                string uniqueFileName = $"{Guid.NewGuid()}{Path.GetExtension(image.FileName)}";
                string objectName = $"therapist-profiles/{uniqueFileName}";

                // Create object options with public access
                var objectOptions = new UploadObjectOptions
                {
                    PredefinedAcl = PredefinedObjectAcl.PublicRead
                };

                using (var stream = image.OpenReadStream())
                {
                    var obj = await _storageClient.UploadObjectAsync(
                        _bucketName,
                        objectName,
                        GetContentType(image.FileName),
                        stream,
                        options: objectOptions);

                    // Construct a direct public URL
                    return $"https://storage.googleapis.com/{_bucketName}/{objectName}";
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error uploading to Firebase: {ex.Message}");
                return null;
            }
        }

        private string GetContentType(string fileName)
        {
            var ext = Path.GetExtension(fileName).ToLowerInvariant();
            switch (ext)
            {
                case ".jpg":
                case ".jpeg":
                    return "image/jpeg";
                case ".png":
                    return "image/png";
                case ".gif":
                    return "image/gif";
                case ".webp":
                    return "image/webp";
                default:
                    return "application/octet-stream";
            }
        }

        public async Task DeleteImageAsync(string imageUrl)
        {
            if (string.IsNullOrEmpty(imageUrl))
                return;

            try
            {
                // Extract the object name from the URL
                var objectName = imageUrl
                    .Replace($"https://storage.googleapis.com/{_bucketName}/", "")
                    .Split('?')[0]; // Remove any query parameters

                if (!string.IsNullOrEmpty(objectName))
                {
                    await _storageClient.DeleteObjectAsync(_bucketName, objectName);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting from Firebase: {ex.Message}");
            }
        }
    }
}