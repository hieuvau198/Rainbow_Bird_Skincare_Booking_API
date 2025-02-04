using Application.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Application.Services
{
    public class FirebaseCredentialProvider : IFirebaseCredentialProvider
    {
        private readonly IConfiguration _configuration;
        private readonly IHostEnvironment _environment;  // Changed from IWebHostEnvironment

        public FirebaseCredentialProvider(IConfiguration configuration, IHostEnvironment environment)  // Changed here too
        {
            _configuration = configuration;
            _environment = environment;
        }

        public string GetServiceAccountJson()
        {
            if (_environment.IsDevelopment())  // This will now work
            {
                // Rest of the code remains the same
                var serviceAccount = _configuration.GetSection("Firebase:ServiceAccount").Get<JsonElement>();
                return serviceAccount.GetRawText();
            }
            else
            {
                return _configuration["FIREBASE_SERVICE_ACCOUNT"];
            }
        }
    }
}
