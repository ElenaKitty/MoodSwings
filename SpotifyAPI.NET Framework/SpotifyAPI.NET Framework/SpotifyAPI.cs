using System;
using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace MoodSwing
{
    /*
     * This class contains all the spotify api request that we need to make.
     */
    class SpotifyAPI
    {
        JsonSerializer serializer = new JsonSerializer();
        SpotifyAuth auth = new SpotifyAuth();

        private String OAuth;
        private String urlPlaying = "https://api.spotify.com/v1/me/player/currently-playing?market=NL";
        private String urlTrack = "https://api.spotify.com/v1/audio-features/";
        private String urlAnalysis = "https://api.spotify.com/v1/audio-analysis/";
        private String urlPause = "https://api.spotify.com/v1/me/player/pause";
        private String urlResume = "https://api.spotify.com/v1/me/player/play";
        private String urlNext = "	https://api.spotify.com/v1/me/player/next";
        private String urlPrevious = "https://api.spotify.com/v1/me/player/previous";
        private String urlPlayback = "https://api.spotify.com/v1/me/player?market=NL";

        public SpotifyAPI()
        {
            this.OAuth = auth.getAuth().Result;
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
                        using (StreamWriter file = File.CreateText(@"..\..\Resources\CurrentTrack.json"))
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
                String id = "";
                try
                {
                    id = currentTrackData.item.id;
                }
                catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException e)
                {
                    Console.WriteLine($"No current track to analyse: '{e}'\n");
                }
                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", OAuth);
                    using (HttpResponseMessage response = await client.GetAsync(urlTrack + id))
                    {
                        using (HttpContent content = response.Content)
                        {
                            String myContent = await content.ReadAsStringAsync();

                            using (StreamWriter newFile = File.CreateText(@"..\..\Resources\TrackFeatures.json"))
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
                String id = "";
                try
                {
                    id = currentTrackData.item.id;
                }
                catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException e)
                {
                    Console.WriteLine($"No current track to analyse: '{e}'\n");
                }
                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", OAuth);
                    using (HttpResponseMessage response = await client.GetAsync(urlAnalysis + id))
                    {
                        using (HttpContent content = response.Content)
                        {
                            String myContent = await content.ReadAsStringAsync();
                            using (StreamWriter newFile = File.CreateText(@"..\..\Resources\TrackAnalysis.json"))
                            {
                                JObject trackAnalysisJson = new JObject();
                                try
                                {
                                    JObject.Parse(myContent);
                                }
                                catch (JsonReaderException e)
                                {
                                    Console.WriteLine($"No content to convert to json: '{e}'\n");
                                }
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
                            using (StreamWriter newFile = File.CreateText(@"..\..\Resources\Artist.json"))
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

        public async Task currentPlayback()
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", OAuth);
                using (HttpResponseMessage response = await client.GetAsync(urlPlayback))
                {
                    using (HttpContent content = response.Content)
                    {
                        String myContent = await content.ReadAsStringAsync();
                        using (StreamWriter newFile = File.CreateText(@"..\..\Resources\Playback.json"))
                        {
                            JObject playbackJson = new JObject();
                            try
                            {
                                playbackJson = JObject.Parse(myContent);
                            }
                            catch (JsonReaderException e)
                            {
                                Console.WriteLine($"Spotify player not recognised: '{e}'\n");
                            }
                            dynamic playbackData = playbackJson;
                            serializer.Serialize(newFile, playbackJson);
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
            await currentPlayback();
            using (StreamReader playbackFile = File.OpenText(@"..\..\Resources\Playback.json"))
            {
                JObject playbackJson = (JObject)serializer.Deserialize(playbackFile, typeof(JObject));
                dynamic playbackData = playbackJson;
                switch (control)
                {
                    case "Resume/Pause":
                        try
                        {
                            bool resume = playbackData.is_playing;
                            if (resume)
                            {
                                await pauseTrack();
                            }
                            else
                            {
                                await resumeTrack();
                            }
                        }
                        catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException)
                        {

                        }
                        break;

                    case "Next":
                        await nextTrack();
                        break;

                    case "Previous":
                        await previousTrack();
                        break;
                }
            }

        }

        // this method runs all the above mentioned methods.
        public async Task spotifyAPIRequest(String filepath)
        {
            await getCurrentTrack();
            await getTrackFeatures(filepath);
            await getTrackAnalysis(filepath);
            //await getArtist(filepath);
        }

        // this method returns a value from jsons.
        public dynamic getData(String data)
        {
            dynamic returnable = null;
            using (StreamReader currentTrackFile = File.OpenText(@"..\..\Resources\CurrentTrack.json"))
            using (StreamReader trackFeaturesFile = File.OpenText(@"..\..\Resources\TrackFeatures.json"))
            {
                JObject currentTrackJson = (JObject)serializer.Deserialize(currentTrackFile, typeof(JObject));
                dynamic currentTrackData = currentTrackJson;
                JObject trackFeaturesJson = (JObject)serializer.Deserialize(trackFeaturesFile, typeof(JObject));
                dynamic trackFeaturesData = trackFeaturesJson;
                switch (data)
                {

                    case "artist":
                        returnable = currentTrackData.item.album.artists[0].name;
                        break;

                    case "track":
                        returnable = currentTrackData.item.name;
                        break;

                    case "danceability":
                        returnable = trackFeaturesData.danceability;
                        break;

                    case "energy":
                        returnable = trackFeaturesData.energy;
                        break;


                    case "bpm":
                        returnable = trackFeaturesData.tempo;
                        break;

                    case "valence":
                        returnable = trackFeaturesData.valence;
                        break;
                }
            }
            return returnable;
        }
    }
}
