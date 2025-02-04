using Google.Apis.Auth.OAuth2;
using Google.Cloud.Storage.V1;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SecretsController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public SecretsController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public IActionResult GetSecrets()
        {
            var googleClientId = _configuration["Google:ClientId"];
            var googleClientSecret = _configuration["Google:ClientSecret"];

            // Try direct configuration first
            var firebaseServiceAccountJson = _configuration["Firebase:ServiceAccountJson"];

            //// If empty, try nested configuration
            //if (string.IsNullOrEmpty(firebaseServiceAccountJson))
            //{
            //    var serviceAccountSection = _configuration.GetSection("Firebase:ServiceAccount");
            //    if (serviceAccountSection.Exists())
            //    {
            //        var serviceAccountDict = serviceAccountSection.GetChildren()
            //            .ToDictionary(
            //                x => char.ToLowerInvariant(x.Key[0]) + x.Key.Substring(1),
            //                x => x.Value
            //            );

            //        firebaseServiceAccountJson = JsonConvert.SerializeObject(serviceAccountDict);
            //    }
            //}

            // Check if Firebase credentials are correctly retrieved
            bool canCreateStorageClient = false;
            string firebaseMessage = "Firebase is NOT configured properly.";

            if (!string.IsNullOrEmpty(firebaseServiceAccountJson))
            {
                try
                {
                    // Load Firebase credentials
                    var credential = GoogleCredential.FromJson(firebaseServiceAccountJson);

                    // Attempt to create a StorageClient
                    var storageClient = StorageClient.Create(credential);
                    canCreateStorageClient = true;
                    firebaseMessage = "Firebase StorageClient was created successfully!";
                }
                catch (Exception ex)
                {
                    firebaseMessage = $"Failed to create StorageClient: {ex.Message}";
                }
            }

            var result = new
            {
                Google = new
                {
                    ClientId = googleClientId,
                    ClientSecret = googleClientSecret
                },
                Firebase = new
                {
                    IsConfigured = !string.IsNullOrEmpty(firebaseServiceAccountJson),
                    CanCreateStorageClient = canCreateStorageClient,
                    Message = firebaseMessage
                }
            };

            return Ok(result);
        }
    }
}
