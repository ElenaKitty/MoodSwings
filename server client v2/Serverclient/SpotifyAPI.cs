using System;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Serverclient
{
    class SpotifyAPI
    {
        JsonSerializer serializer = new JsonSerializer();

        private String OAuth;
        private String urlPlaying = "https://api.spotify.com/v1/me/player/currently-playing?market=NL";
        private String urlTrack = "https://api.spotify.com/v1/audio-features/";
        private String urlAnalysis = "https://api.spotify.com/v1/audio-analysis/";

        public SpotifyAPI(String OAuth)
        {
            this.OAuth = OAuth;
        }

        public async Task getCurrentTrack()
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", OAuth);
                using (HttpResponseMessage response = await client.GetAsync(urlPlaying))
                {
                    using (HttpContent content = response.Content)
                    {
                        String myContent = await content.ReadAsStringAsync();
                        using (StreamWriter file = File.CreateText(@"Resources\CurrentTrack.json"))
                        {
                            if (myContent != "")
                            {
                                JObject json = JObject.Parse(myContent);
                                dynamic jsonData = json;
                                String name = jsonData.item.album.artists[0].name;
                                Console.WriteLine("Artist name: " + name);
                                name = jsonData.item.name;
                                Console.WriteLine("Track name: " + name);
                                serializer.Serialize(file, json);
                            }
                        }
                    }
                }
            }
        }

        public async Task getTrackFeatures(String filepath)
        {
            await getCurrentTrack();
            using (StreamReader file = File.OpenText(filepath))
            {
                JObject json = (JObject)serializer.Deserialize(file, typeof(JObject));
                dynamic jsonData = json;
                String id = jsonData.item.id;
                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", OAuth);
                    using (HttpResponseMessage response = await client.GetAsync(urlTrack + id))
                    {
                        using (HttpContent content = response.Content)
                        {
                            String myContent = await content.ReadAsStringAsync();
                            using (StreamWriter jsonFile = File.CreateText(@"Resources\TrackFeatures.json"))
                            {
                                JObject jObject = JObject.Parse(myContent);
                                dynamic jObjectData = jObject;
                                Console.WriteLine("Danceability: " + jObjectData.danceability);
                                Console.WriteLine("Energy: " + jObjectData.energy);
                                Console.WriteLine("BPM: " + jObjectData.tempo);
                                serializer.Serialize(jsonFile, jObject);
                            }
                        }
                    }
                }
            }
        }

        public async Task getTrackAnalysis(String filepath)
        {
            await getCurrentTrack();
            await getTrackFeatures(filepath);
            using (StreamReader file = File.OpenText(filepath))
            {
                JObject json = (JObject)serializer.Deserialize(file, typeof(JObject));
                dynamic jsonData = json;
                String id = jsonData.item.id;
                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", OAuth);
                    using (HttpResponseMessage response = await client.GetAsync(urlAnalysis + id))
                    {
                        using (HttpContent content = response.Content)
                        {
                            String myContent = await content.ReadAsStringAsync();
                            using (StreamWriter jsonFile = File.CreateText(@"Resources\TrackAnalysis.json"))
                            {
                                JObject jObject = JObject.Parse(myContent);
                                dynamic jObjectData = jObject;
                                serializer.Serialize(jsonFile, jObject);
                            }
                        }
                    }
                }
            }
        }

        public dynamic getData(String data)
        {
            dynamic returnAble = null;
            switch (data)
            {
                case "artist":
                    using (StreamReader file = File.OpenText(@"Resources\CurrentTrack.json"))
                    {
                        JObject json = (JObject)serializer.Deserialize(file, typeof(JObject));
                        dynamic jsonData = json;
                        returnAble = jsonData.item.album.artists[0].name;

                    }
                    break;

                case "track":
                    using (StreamReader file = File.OpenText(@"Resources\CurrentTrack.json"))
                    {
                        JObject json = (JObject)serializer.Deserialize(file, typeof(JObject));
                        dynamic jsonData = json;
                        returnAble = jsonData.item.name;
                    }
                    break;

                case "danceability":
                    using (StreamReader file = File.OpenText(@"Resources\TrackFeatures.json"))
                    {
                        JObject json = (JObject)serializer.Deserialize(file, typeof(JObject));
                        dynamic jsonData = json;
                        returnAble = jsonData.danceability;
                    }
                    break;

                case "energy":
                    using (StreamReader file = File.OpenText(@"Resources\TrackFeatures.json"))
                    {
                        JObject json = (JObject)serializer.Deserialize(file, typeof(JObject));
                        dynamic jsonData = json;
                        returnAble = jsonData.energy;
                    }
                    break;


                case "bpm":
                    using (StreamReader file = File.OpenText(@"Resources\TrackFeatures.json "))
                    {
                        JObject json = (JObject)serializer.Deserialize(file, typeof(JObject));
                        dynamic jsonData = json;
                        returnAble = jsonData.tempo;
                    }
                    break;
            }
            return returnAble;
        }
    }
}
