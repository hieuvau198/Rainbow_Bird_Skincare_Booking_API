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

            //var firebaseServiceAccount = new Dictionary<string, string>
            //{
            //    { "type", _configuration["Firebase:ServiceAccount:Type"] },
            //    { "project_id", _configuration["Firebase:ServiceAccount:ProjectId"] },
            //    { "private_key_id", _configuration["Firebase:ServiceAccount:PrivateKeyId"] },
            //    { "client_email", _configuration["Firebase:ServiceAccount:ClientEmail"] },
            //    { "client_id", _configuration["Firebase:ServiceAccount:ClientId"] }
            //};

            var result = new
            {
                Google = new
                {
                    ClientId = googleClientId,
                    ClientSecret = googleClientSecret
                }
                //,
                //Firebase = new
                //{
                //    ServiceAccount = firebaseServiceAccount
                //}
            };

            return Ok(result);
        }
    }
}
