using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
            var firebaseServiceAccountJson = _configuration["Firebase:ServiceAccountJson"];

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
