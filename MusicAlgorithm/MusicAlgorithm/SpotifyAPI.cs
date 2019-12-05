using System;
using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Threading.Tasks;

namespace MusicAlgorithm
{
    /*
     * This class contains all the spotify api request that we need to make.
     */
    class SpotifyAPI
    {
        JsonSerializer serializer = new JsonSerializer();

        private String OAuth;
        private String urlPlaying = "https://api.spotify.com/v1/me/player/currently-playing?market=NL";
        private String urlTrack = "https://api.spotify.com/v1/audio-features/";
        private String urlAnalysis = "https://api.spotify.com/v1/audio-analysis/";
        private String urlPause = "https://api.spotify.com/v1/me/player/pause";
        private String urlResume = "https://api.spotify.com/v1/me/player/play";
        private String urlNext = "	https://api.spotify.com/v1/me/player/next";
        private String urlPrevious = "https://api.spotify.com/v1/me/player/previous";

        public SpotifyAPI(String OAuth)
        {
            this.OAuth = OAuth;
        }

        // This method makes a http request to get the current track.
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
                        using (StreamWriter file = File.CreateText(@"..\..\..\Resources\CurrentTrack.json"))
                        {
                            if (myContent != "")
                            {
                                JObject currentTrackJson = JObject.Parse(myContent);
                                dynamic currentTrackData = currentTrackJson;
                                String name = currentTrackData.item.album.artists[0].name;
                                Console.WriteLine("Artist name: " + name);
                                name = currentTrackData.item.name;
                                Console.WriteLine("Track name: " + name);
                                serializer.Serialize(file, currentTrackJson);
                            }
                        }
                    }
                }
            }
        }

        // This method makes a http request to get the track features.
        public async Task getTrackFeatures(String filepath)
        {
            using (StreamReader file = File.OpenText(filepath))
            {
                JObject currentTrackJson = (JObject)serializer.Deserialize(file, typeof(JObject));
                dynamic currentTrackData = currentTrackJson;
                String id = currentTrackData.item.id;
                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", OAuth);
                    using (HttpResponseMessage response = await client.GetAsync(urlTrack + id))
                    {
                        using (HttpContent content = response.Content)
                        {
                            String myContent = await content.ReadAsStringAsync();
                            using (StreamWriter newFile = File.CreateText(@"..\..\..\Resources\TrackFeatures.json"))
                            {
                                JObject trackFeaturesJson = JObject.Parse(myContent);
                                dynamic trackFeaturesData = trackFeaturesJson;
                                Console.WriteLine("Danceability: " + trackFeaturesData.danceability);
                                Console.WriteLine("Energy: " + trackFeaturesData.energy);
                                Console.WriteLine("BPM: " + trackFeaturesData.tempo);
                                serializer.Serialize(newFile, trackFeaturesJson);
                            }
                        }
                    }
                }
            }
        }

        // This method makes a http request to get the track analysis.
        public async Task getTrackAnalysis(String filepath)
        {
            using (StreamReader file = File.OpenText(filepath))
            {
                JObject currentTrackJson = (JObject)serializer.Deserialize(file, typeof(JObject));
                dynamic currentTrackData = currentTrackJson;
                String id = currentTrackData.item.id;
                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", OAuth);
                    using (HttpResponseMessage response = await client.GetAsync(urlAnalysis + id))
                    {
                        using (HttpContent content = response.Content)
                        {
                            String myContent = await content.ReadAsStringAsync();
                            using (StreamWriter newFile = File.CreateText(@"..\..\..\Resources\TrackAnalysis.json"))
                            {
                                JObject trackAnalysisJson = JObject.Parse(myContent);
                                dynamic trackAnalysisData = trackAnalysisJson;
                                serializer.Serialize(newFile, trackAnalysisJson);
                            }
                        }
                    }
                }
            }
        }

        // This method makes a http request to get a artist.
        public async Task getArtist(String filepath)
        {
            using (StreamReader file = File.OpenText(filepath))
            {
                JObject currentTrackJson = (JObject)serializer.Deserialize(file, typeof(JObject));
                dynamic currentTrackData = currentTrackJson;
                String id = currentTrackData.context.href;
                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", OAuth);
                    using (HttpResponseMessage response = await client.GetAsync(id))
                    {
                        using (HttpContent content = response.Content)
                        {
                            String myContent = await content.ReadAsStringAsync();
                            using (StreamWriter newFile = File.CreateText(@"..\..\..\Resources\Artist.json"))
                            {
                                JObject artistJson = JObject.Parse(myContent);
                                dynamic artistData = artistJson;
                                serializer.Serialize(newFile, artistJson);
                            }
                        }
                    }
                }
            }
        }

        public async Task pauseTrack()
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", OAuth);
                await client.PutAsync(urlPause, null);
            }
        }

        public async Task resumeTrack()
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", OAuth);
                await client.PutAsync(urlResume, null);
            }
        }

        public async Task nextTrack()
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", OAuth);
                await client.PostAsync(urlNext, null);
            }
        }

        public async Task previousTrack()
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", OAuth);
                await client.PostAsync(urlPrevious, null);
            }
        }

        public async void playerControl(String control)
        {
            switch (control)
            {
                case "pause":
                    await pauseTrack();
                    break;

                case "resume":
                    await resumeTrack();
                    break;

                case "next":
                    await nextTrack();
                    break;

                case "previous":
                    await previousTrack();
                    break;
            }
        }

        // this method runs all the above mentioned methods.
        public async void spotifyAPIRequest(String filepath)
        {
            await getCurrentTrack();
            await getTrackFeatures(filepath);
            await getTrackAnalysis(filepath);
            await getArtist(filepath);
        }

        // this method returns a values from jsons.
        public dynamic getData(String data)
        {
            dynamic returnAble = null;
            using (StreamReader currentTrackFile = File.OpenText(@"..\..\..\Resources\CurrentTrack.json"))
            using (StreamReader trackFeaturesFile = File.OpenText(@"..\..\..\Resources\TrackFeatures.json"))
            {
                JObject currentTrackJson = (JObject)serializer.Deserialize(currentTrackFile, typeof(JObject));
                dynamic currentTrackData = currentTrackJson;
                JObject trackFeaturesJson = (JObject)serializer.Deserialize(trackFeaturesFile, typeof(JObject));
                dynamic trackFeaturesData = trackFeaturesJson;
                switch (data)
                {

                    case "artist":
                        returnAble = currentTrackData.item.album.artists[0].name;
                        break;

                    case "track":
                        returnAble = currentTrackData.item.name;
                        break;

                    case "danceability":
                        returnAble = trackFeaturesData.danceability;
                        break;

                    case "energy":
                        returnAble = trackFeaturesData.energy;
                        break;


                    case "bpm":
                        returnAble = trackFeaturesData.tempo;
                        break;
                }
            }
            return returnAble;
        }
    }
}
