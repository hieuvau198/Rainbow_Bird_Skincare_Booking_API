﻿using Application.Interfaces;
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
using Newtonsoft.Json.Linq;

namespace Application.Services
{
    public class FirebaseImageService : IImageService
    {
        private readonly StorageClient _storageClient;
        private readonly string _bucketName;

        public FirebaseImageService(IConfiguration configuration)
        {
            _bucketName = "flipbook-firebase.appspot.com";

            var base64ServiceJson = "ewogICJ0eXBlIjogInNlcnZpY2VfYWNjb3VudCIsCiAgInByb2plY3RfaWQiOiAiZmxpcGJvb2stZmlyZWJhc2UiLAogICJwcml2YXRlX2tleV9pZCI6ICJkNDkwNjQwYjU3NWViMmRlZjQ1ODZlNzA3YTI0Y2MyNzRmNDBmZTI0IiwKICAicHJpdmF0ZV9rZXkiOiAiLS0tLS1CRUdJTiBQUklWQVRFIEtFWS0tLS0tXG5NSUlFdlFJQkFEQU5CZ2txaGtpRzl3MEJBUUVGQUFTQ0JLY3dnZ1NqQWdFQUFvSUJBUUNYdm1ON2JubzY2dWpJXG5oTkhMUGkySjJIVCtSZkhCc25KNTZ3Q1kzdnZ0MWpTLzZPUVh2alEwN2JHNW1NWWYvdGkwSHpJVWwwcWJQVmVJXG5QUXpTK2dKaUYrZUkrSFV0UVhPNEJoVHk0TUpwR3VBckF5bS9hWkdQK3U3cHJzU3VIOXVpcXBaZ1ZpZFZrNGtFXG4vQm0ybWVCN2d6cGJBcnhKc09BR2k4S0pZYVUvb0ozbHVkMGVJTmhyU1ZNVWNiSW1MaGlWQ2I4QkhxVENqc0pEXG41YldNREw5c1BNNCtFYXRkcFF1c3UyUGROSTRSWmdRZm91elRDdGJaU3UxMGprcE5GcGo3d3VIVHY4TThHcTgvXG43MGRGWXhjak02NjQrYlMrTFFIWVhQL1hvbURLZzJFOGV0eEk2ejZrWUZ4Tjhadnd6Z0ZiT0N5dnlhZE1lNkYwXG5YTEkvOGM1bEFnTUJBQUVDZ2dFQUVUcEgwQjBvV21VT0hhbmFxYkQ2K1pIdUltb3RldGk0SDNoYlBuL2VhVSt1XG4rSFRINUp3dkVDMUdSclIrRmViWWtvYVNLSDFPdHBZOXlGVnFEYy9ka21aMVhuc2F6cE1HUU1mTC9CRWhjVTVnXG5VZkhQZlJCT3V1SjUybVVCcG1VdWllYkZJTlhYTEdPT0pGYzgwaHJoUGhTVmZQeXdCTXZFZTJuRm9kUklyeEx2XG53NG15bk5PejZObUM3eXFBVkdMTWRtVThkMStza2VhV3V3TWV2VkZPNXdMaHJ3Q2pHVTQ0SDA2MkQ0RHdzUXdMXG5BdldWOGhVbTdTS1BjS3VrL2U5RHBZemx6cGUwNmxua2FNRzBvcENxMTRqVzl6T3VOUVB1YzVQdUFaa0p0ME51XG5qeFlkMk5oZXJRZUpNRUcvYWVWWUpFRUtSa0V3Q245WVFjOFRSa1ZHMXdLQmdRRFZ4Y3E2ZzBYckZsVEIvMDViXG5kTitYbkFYYmozU2FmYnNNWm81RjdJL3RvVXIyZGVnd1pBa0YxNnppZWJMQVZkMlpoTldXOWdUa3h2anpvdEw1XG5DOHM4TE9BcGtNYlhwUWNVMy9rSk9lZGd3YUIxLzNKdXdsY3hwelg4MWVmRWwraXQyczlob0p1TURaa3ZFTGJqXG5EZEwvbUVhNTRaQ3FJQkN5Y1lib0ZURUY4d0tCZ1FDMXQ5K2xyTUMzOHQyZ0N3OG8xVGp2cjN0cnNHcWRNc2FDXG4xaXhJS1Qrc0JWYndaMUNDeFNFQmJXVnYvU3BlMjlVU1ZkVFNjVmhxeXhtRE5uelVlbGt3Nlg5VExSKzJqUVQwXG5SZnN1OHNsTlExTmhkZGloVTJDVE1IZmw1ZWp4MHhaYzcySWdrTitJU2pkOUVhT0kvUGN2dmZ0K2ZsbUc4bmJZXG52ck1BaU1RNFJ3S0JnUUMvWi9JMVBnVUVrV0lpc2E0L1JVNU9PVzBsUWpWdGZ0WlVMQitIakdEeXJGQ3FqTGZ4XG5YQ0NZRXB6Qnk2VzVnU2lCcE9aNTNKNVZHYk1lc3RPa0dtTkc1Z2R3TUNsYVBIRXl4N2Y4QXRTaFZiMk82Y0pVXG5XYjRvdjBjZnM0ZHFCM3BXOEd4dlJaY0F0OHhJei9aeEpwZWVNNEpnUFErQ3hHTXU0MmVmdGhuRzhRS0JnQ0dlXG5wRDBGcWg0ZVM4eVpYek9oeDBmcEFuK1pBeENVWFUvRmlpbkxuK0VXbDlBZ2ZTL0VndWU5c3ArMmlnbEV5TFg4XG50VVE5L2lxNzZydHc4RVZyWVdjQVBETktUT3k4U0dkZEx5eXZkSGpiOU9nNklsc3VqdGFNaUJJN3FBNWRqR3lqXG5TVmRYRmxRanp3SlBxaDdsRm1KNTFyYS9iNWJjOHdvRXRoOXFMa3R2QW9HQVVUclNaWmtDQyt3RG5XTTdtY3IxXG5URUhjUHAxQmlOc0Z1UlpCV3dqYWg3TUF1eHRXd1JoNjdOcHUrd3IxRHRCUk13QXRKVVpnTG5wZTN1d2JXVmFtXG5KYUVZZFVhemZGblJSQ1JUZXN0U1AxZFBaU3pxVE0xOGZ0MldCaFZ5T3N5YStYWFdIVXl0a1UzSGRXQWJxMnYxXG5GRzg1ZmhoQmRzcnBMVHFTS01rQ0ZuOD1cbi0tLS0tRU5EIFBSSVZBVEUgS0VZLS0tLS1cbiIsCiAgImNsaWVudF9lbWFpbCI6ICJmaXJlYmFzZS1hZG1pbnNkay1ueW8zeEBmbGlwYm9vay1maXJlYmFzZS5pYW0uZ3NlcnZpY2VhY2NvdW50LmNvbSIsCiAgImNsaWVudF9pZCI6ICIxMDY4Mjk4OTcwNTk0OTgwNDUxMDQiLAogICJhdXRoX3VyaSI6ICJodHRwczovL2FjY291bnRzLmdvb2dsZS5jb20vby9vYXV0aDIvYXV0aCIsCiAgInRva2VuX3VyaSI6ICJodHRwczovL29hdXRoMi5nb29nbGVhcGlzLmNvbS90b2tlbiIsCiAgImF1dGhfcHJvdmlkZXJfeDUwOV9jZXJ0X3VybCI6ICJodHRwczovL3d3dy5nb29nbGVhcGlzLmNvbS9vYXV0aDIvdjEvY2VydHMiLAogICJjbGllbnRfeDUwOV9jZXJ0X3VybCI6ICJodHRwczovL3d3dy5nb29nbGVhcGlzLmNvbS9yb2JvdC92MS9tZXRhZGF0YS94NTA5L2ZpcmViYXNlLWFkbWluc2RrLW55bzN4JTQwZmxpcGJvb2stZmlyZWJhc2UuaWFtLmdzZXJ2aWNlYWNjb3VudC5jb20iLAogICJ1bml2ZXJzZV9kb21haW4iOiAiZ29vZ2xlYXBpcy5jb20iCn0K";
            byte[] decodedBytes = Convert.FromBase64String(base64ServiceJson);
            string decodedJson = Encoding.UTF8.GetString(decodedBytes);

            // Verify the decoded string is valid JSON
            JObject.Parse(decodedJson);

            var firebaseServiceAccountJson = decodedJson;

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