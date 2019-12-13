using System;
using System.Collections.Generic;
using System.Net.Http;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System.IO;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace MoodSwing
{
    class SpotifyAuth
    {
        private String authorizationUrl = "https://accounts.spotify.com/authorize?client_id=5dc276b9432a4b55b0e1070fa5569441&response_type=code&redirect_uri=https://mysite.com/callback/&scope=user-read-currently-playing%20user-modify-playback-state%20user-read-playback-state&state=34fFs29kd09";

        IWebDriver driver = new ChromeDriver();
        JsonSerializer serializer = new JsonSerializer();
        public async Task<String> requestAuth()
        {
            String authToken = "No token received";
            driver.Navigate().GoToUrl(authorizationUrl);
            while (!driver.Url.Contains("https://mysite.com/callback/?code=")) ;
            String code = driver.Url.Substring(34);
            code = code.Split('&')[0];
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", "NWRjMjc2Yjk0MzJhNGI1NWIwZTEwNzBmYTU1Njk0NDE6NGIzZWU1MmZkMmVmNDRkNGE2YWU3YTUxNTIwZDgxNzA=");
                var dict = new Dictionary<String, String>();
                dict.Add("grant_type", "authorization_code");
                dict.Add("code", code);
                dict.Add("redirect_uri", "https://mysite.com/callback/");
                var req = new HttpRequestMessage(HttpMethod.Post, "https://accounts.spotify.com/api/token")
                {
                    Content = new FormUrlEncodedContent(dict)
                };
                var res = await client.SendAsync(req);
                using (StreamWriter file = File.CreateText(@"..\..\Resources\Report.json"))
                {
                    JObject authJson = JObject.Parse(res.Content.ReadAsStringAsync().Result);
                    dynamic authData = authJson;
                    authToken = authData.access_token;
                    serializer.Serialize(file, authJson);
                }
            }
            return authToken;
        }

        public async Task<String> getAuth()
        {
            return await requestAuth();
        }
    }
}
