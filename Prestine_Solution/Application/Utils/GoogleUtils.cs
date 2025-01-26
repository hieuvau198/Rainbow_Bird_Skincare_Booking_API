using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Utils
{
    public class GoogleTokenValidator
    {
        public async Task<GoogleUserInfo?> ValidateAsync(string idToken)
        {
            var client = new HttpClient();
            var googleUrl = $"https://www.googleapis.com/oauth2/v3/tokeninfo?id_token={idToken}";
            var response = await client.GetAsync(googleUrl);

            if (!response.IsSuccessStatusCode)
                return null;

            var payload = await response.Content.ReadAsStringAsync();
            var googleUser = JsonConvert.DeserializeObject<GoogleUserInfo>(payload);

            return googleUser;
        }
    }

    public class GoogleUserInfo
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public string Picture { get; set; }
    }

}
