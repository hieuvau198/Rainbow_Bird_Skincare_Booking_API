using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;

namespace Application.Utils
{
    public static class FirebaseConfigurationExtensions
    {
        public static string GetFirebaseServiceAccountJson(this IConfiguration configuration, IHostEnvironment environment = null)
        {
            // Check if environment is null or not in development
            bool isDevelopment = environment != null &&
                                 environment.EnvironmentName == Environments.Development;

            // Development: Check user secrets
            if (isDevelopment)
            {
                var serviceAccountSection = configuration.GetSection("Firebase:ServiceAccount");
                if (serviceAccountSection.Exists())
                {
                    var serviceAccountDict = serviceAccountSection.GetChildren()
                        .ToDictionary(
                            x => char.ToLowerInvariant(x.Key[0]) + x.Key.Substring(1),
                            x => x.Value
                        );

                    // Save the serialized JSON to configuration
                    var serviceAccountJson = JsonConvert.SerializeObject(serviceAccountDict);
                    configuration["Firebase:ServiceAccountJson"] = serviceAccountJson;

                    return serviceAccountJson;
                }
            }

            // Production: Retrieve the existing JSON string from configuration
            var serviceAccountJsonProd = configuration["Firebase:ServiceAccountJson"];

            // Ensure it's a properly formatted JSON string
            if (!string.IsNullOrWhiteSpace(serviceAccountJsonProd))
            {
                try
                {
                    // Attempt to parse to verify JSON validity
                    JObject.Parse(serviceAccountJsonProd);
                    return serviceAccountJsonProd;
                }
                catch (JsonReaderException)
                {
                    throw new InvalidOperationException("Invalid Firebase Service Account JSON format in production.");
                }
            }

            throw new InvalidOperationException("Firebase Service Account JSON is missing in production configuration.");
        }
    }
}