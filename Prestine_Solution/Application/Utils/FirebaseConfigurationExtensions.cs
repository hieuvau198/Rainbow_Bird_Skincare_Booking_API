using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
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

            // Production: Use ServiceAccountJson
            return configuration["Firebase:ServiceAccountJson"];
        }
    }
}