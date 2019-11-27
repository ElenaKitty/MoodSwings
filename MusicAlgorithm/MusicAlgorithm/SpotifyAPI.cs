using System;
using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
namespace MusicAlgorithm
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

        public async void getCurrentTrack()
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", OAuth);
                using (HttpResponseMessage response = await client.GetAsync(urlPlaying))
                {
                    using (HttpContent content = response.Content)
                    {
                        String myContent = await content.ReadAsStringAsync();
                        using (StreamWriter file = File.CreateText(@"C:\Users\bodhi\source\repos\MusicAlgorithm\MusicAlgorithm\Resources\CurrentTrack.json"))
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

        public async void getTrackFeatures(String filepath)
        {
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
                            using (StreamWriter jsonFile = File.CreateText(@"C:\Users\bodhi\source\repos\MusicAlgorithm\MusicAlgorithm\Resources\TrackFeatures.json"))
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

        public async void getTrackAnalysis(String filepath)
        {
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
                            using (StreamWriter jsonFile = File.CreateText(@"C:\Users\bodhi\source\repos\MusicAlgorithm\MusicAlgorithm\Resources\TrackAnalysis.json"))
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

        public String getArtistName()
        {
            using (StreamReader file = File.OpenText(@"C:\Users\bodhi\source\repos\MusicAlgorithm\MusicAlgorithm\Resources\CurrentTrack.json"))
            {
                JObject json = (JObject)serializer.Deserialize(file, typeof(JObject));
                dynamic jsonData = json;
                String name = jsonData.item.album.artists[0].name;
                return name;
            }
        }

        public String getTrackName()
        {
            using (StreamReader file = File.OpenText(@"C:\Users\bodhi\source\repos\MusicAlgorithm\MusicAlgorithm\Resources\CurrentTrack.json"))
            {
                JObject json = (JObject)serializer.Deserialize(file, typeof(JObject));
                dynamic jsonData = json;
                String name = jsonData.item.name;
                return name;
            }

        }

        public float getDanceAbility()
        {
            using (StreamReader file = File.OpenText(@"C:\Users\bodhi\source\repos\MusicAlgorithm\MusicAlgorithm\Resources\TrackFeatures.json"))
            {
                JObject json = (JObject)serializer.Deserialize(file, typeof(JObject));
                dynamic jsonData = json;
                float danceability = jsonData.danceability;
                return danceability;
            }

        }

        public float getEnergy()
        {
            using (StreamReader file = File.OpenText(@"C:\Users\bodhi\source\repos\MusicAlgorithm\MusicAlgorithm\Resources\TrackFeatures.json"))
            {
                JObject json = (JObject)serializer.Deserialize(file, typeof(JObject));
                dynamic jsonData = json;
                float energy = jsonData.energy;
                return energy;
            }

        }

        public float getBPM()
        {
            using (StreamReader file = File.OpenText(@"C:\Users\bodhi\source\repos\MusicAlgorithm\MusicAlgorithm\Resources\TrackFeatures.json"))
            {
                JObject json = (JObject)serializer.Deserialize(file, typeof(JObject));
                dynamic jsonData = json;
                float tempo = jsonData.tempo;
                return tempo;
            }

        }
    }
}
