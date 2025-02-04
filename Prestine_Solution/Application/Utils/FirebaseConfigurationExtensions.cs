using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using System.Text;

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

            // Production: Retrieve and decode the Base64 string from configuration
            var base64EncodedJson = configuration["Firebase:ServiceAccountJson"];

            if (!string.IsNullOrWhiteSpace(base64EncodedJson))
            {
                try
                {
                    // Decode from Base64
                    byte[] decodedBytes = Convert.FromBase64String(base64EncodedJson);
                    string decodedJson = Encoding.UTF8.GetString(decodedBytes);

                    // Verify the decoded string is valid JSON
                    JObject.Parse(decodedJson);

                    return decodedJson;
                }
                catch (FormatException)
                {
                    throw new InvalidOperationException("Invalid Base64 encoding for Firebase Service Account JSON.");
                }
                catch (Newtonsoft.Json.JsonReaderException)
                {
                    throw new InvalidOperationException("Decoded Firebase Service Account JSON is not in a valid format.");
                }
            }

            throw new InvalidOperationException("Firebase Service Account JSON is missing in production configuration.");
        }
    }
}