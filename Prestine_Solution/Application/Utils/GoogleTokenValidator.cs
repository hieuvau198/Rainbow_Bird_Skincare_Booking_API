using Google.Apis.Auth;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Utils
{
    public class GoogleTokenValidator
    {
        private readonly IConfiguration _configuration;

        public GoogleTokenValidator(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<GoogleJsonWebSignature.Payload> ValidateTokenAsync(string idToken, string clientId)
        {
            try
            {
                var validationSettings = new GoogleJsonWebSignature.ValidationSettings
                {
                    Audience = new[] { clientId }
                };

                var payload = await GoogleJsonWebSignature.ValidateAsync(idToken, validationSettings);
                return payload;
            }
            catch (InvalidJwtException)
            {
                throw new UnauthorizedAccessException("Invalid Google token");
            }
        }
    }
}
