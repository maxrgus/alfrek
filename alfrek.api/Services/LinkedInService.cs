using alfrek.api.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace alfrek.api.Services
{
    public class LinkedInService : IOauthService
    {
        private String BaseUrl = "";
        public async void GetToken(string authorizationCode)
        {
            using (HttpClient client = new HttpClient())
            using (HttpResponseMessage response = await client.GetAsync(BaseUrl))
            using (HttpContent content = response.Content)
            {
                string data = await content.ReadAsStringAsync();
                if (data != null)
                {
                    Debug.WriteLine("LINKEDINSERVICE: GetToken - " + data);
                }
            }
        }
    }
}
