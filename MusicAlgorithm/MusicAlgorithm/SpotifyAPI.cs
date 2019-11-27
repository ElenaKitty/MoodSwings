using System;
using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Collections.Generic;
namespace MusicAlgorithm
{
    class SpotifyAPI
    {
        private String OAuth;
        private String url = "https://api.spotify.com/v1/me/player/currently-playing?market=NL";

        public SpotifyAPI(String OAuth)
        {
            this.OAuth = OAuth;
        }

        public async void getRequest(String url)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", OAuth);
                using (HttpResponseMessage response = await client.GetAsync(url))
                {
                    using (HttpContent content = response.Content)
                    {
                        String myContent = await content.ReadAsStringAsync();
                        using (StreamWriter file = File.CreateText(@"C:\Users\bodhi\source\repos\MusicAlgorithm\MusicAlgorithm\Resources\song.json"))
                        {
                            if (myContent != "")
                            {
                                JObject json = JObject.Parse(myContent);
                                dynamic jsondata = json;
                                object name = jsondata.item.album.artists[0].name;
                                Console.WriteLine("Hiero is dat naampie " + name);
                                JsonSerializer serializer = new JsonSerializer();
                                serializer.Serialize(file, json);
                            }
                        }
                    }
                }
            }
        }
    }
}
