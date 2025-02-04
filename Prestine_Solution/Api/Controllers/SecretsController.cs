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

            // If empty, try nested configuration
            if (string.IsNullOrEmpty(firebaseServiceAccountJson))
            {
                var serviceAccountSection = _configuration.GetSection("Firebase:ServiceAccount");
                if (serviceAccountSection.Exists())
                {
                    var serviceAccountDict = serviceAccountSection.GetChildren()
                        .ToDictionary(
                            x => char.ToLowerInvariant(x.Key[0]) + x.Key.Substring(1),
                            x => x.Value
                        );

                    firebaseServiceAccountJson = JsonConvert.SerializeObject(serviceAccountDict);
                }
            }

            // Check if Firebase credentials are correctly retrieved
            bool isFirebaseConfigured = !string.IsNullOrEmpty(firebaseServiceAccountJson);

            var result = new
            {
                Google = new
                {
                    ClientId = googleClientId,
                    ClientSecret = googleClientSecret
                },
                Firebase = new
                {
                    IsConfigured = isFirebaseConfigured,
                    Message = isFirebaseConfigured ? "Firebase is set up correctly!" : "Firebase is NOT configured properly."
                }
            };

            return Ok(result);
        }
    }
}
