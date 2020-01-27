using System;
using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Threading;
using System.Timers;

namespace MoodSwing
{
    /*
     * This class contains all the spotify api request that we need to make.
     */
    class SpotifyAPI
    {
        JsonSerializer serializer = new JsonSerializer();
        SpotifyAuth auth = new SpotifyAuth();
        System.Timers.Timer timer = new System.Timers.Timer();
        Mutex mutex = new Mutex();
        Mutex mutexTest = new Mutex();

        private String OAuth;
        private String songID = "";
        public bool NewSong { get; set; }
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
                            JObject currentTrackJson = JObject.Parse(myContent);
                            dynamic currentTrackData = currentTrackJson;
                            Console.WriteLine("Artist name: " + currentTrackData.item.artists[0].name);
                            Console.WriteLine("Track name: " + currentTrackData.item.name);

                            if (myContent != "")
                            {
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
            dynamic currentTrackData;
            String id = "";
            using (StreamReader file = File.OpenText(filepath))
            {
                JObject currentTrackJson = (JObject)serializer.Deserialize(file, typeof(JObject));
                currentTrackData = currentTrackJson;
                try
                {
                    id = currentTrackData.item.id;
                }
                catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException e)
                {
                    Console.WriteLine($"No current track to analyse: '{e}'\n");
                }
            }
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", OAuth);
                using (HttpResponseMessage response = await client.GetAsync(urlTrack + id))
                {
                    using (HttpContent content = response.Content)
                    {
                        String myContent = await content.ReadAsStringAsync();
                        JObject trackFeaturesJson = JObject.Parse(myContent);
                        dynamic trackFeaturesData = trackFeaturesJson;
                        Console.WriteLine("Danceability: " + trackFeaturesData.danceability);
                        Console.WriteLine("Energy: " + trackFeaturesData.energy);
                        Console.WriteLine("BPM: " + trackFeaturesData.tempo);
                        Console.WriteLine("Loudness: " + trackFeaturesData.loudness);

                        using (StreamWriter newFile = File.CreateText(@"..\..\Resources\TrackFeatures.json"))
                        {
                            serializer.Serialize(newFile, trackFeaturesJson);
                        }
                    }
                }
            }
        }

        // This method makes a http request to get the track analysis.
        public async Task getTrackAnalysis(String filepath)
        {
            dynamic currentTrackData;
            String id = "";
            using (StreamReader file = File.OpenText(filepath))
            {
                JObject currentTrackJson = (JObject)serializer.Deserialize(file, typeof(JObject));
                currentTrackData = currentTrackJson;
                try
                {
                    id = currentTrackData.item.id;
                }
                catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException e)
                {
                    Console.WriteLine($"No current track to analyse: '{e}'\n");
                }
            }
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", OAuth);
                using (HttpResponseMessage response = await client.GetAsync(urlAnalysis + id))
                {
                    using (HttpContent content = response.Content)
                    {
                        String myContent = await content.ReadAsStringAsync();
                        JObject trackAnalysisJson = new JObject();
                        try
                        {
                            trackAnalysisJson = JObject.Parse(myContent);
                        }
                        catch (JsonReaderException e)
                        {
                            Console.WriteLine($"No content to convert to json: '{e}'\n");
                        }
                        Console.WriteLine("heyo");
                        using (StreamWriter newFile = File.CreateText(@"..\..\Resources\TrackAnalysis.json"))
                        {
                            serializer.Serialize(newFile, trackAnalysisJson);
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
                            JObject artistJson = JObject.Parse(myContent);
                            dynamic artistData = artistJson;
                            using (StreamWriter newFile = File.CreateText(@"..\..\Resources\Artist.json"))
                            {
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
                        mutex.WaitOne();
                        using (StreamWriter newFile = File.CreateText(@"..\..\Resources\Playback.json"))
                        {
                            serializer.Serialize(newFile, playbackJson);
                        }
                        mutex.ReleaseMutex();
                    }
                }
            }
        }

        public String currentID()
        {
            String id = "";
            JObject playbackJson;
            mutex.WaitOne();
            using (StreamReader file = File.OpenText(@"..\..\Resources\Playback.json"))
            {
                playbackJson = (JObject)serializer.Deserialize(file, typeof(JObject));
            }
            mutex.ReleaseMutex();
            dynamic playbackData = playbackJson;
            try
            {
                id = playbackData.item.id;
            }
            catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException e)
            {
                Console.WriteLine($"Json file is empty: '{e}'\n");
            }
            return id;
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

        public void playerControl(String control)
        {
            JObject playbackJson;
            mutex.WaitOne();
            using (StreamReader playbackFile = File.OpenText(@"..\..\Resources\Playback.json"))
            {
                playbackJson = (JObject)serializer.Deserialize(playbackFile, typeof(JObject));
            }
            mutex.ReleaseMutex();
            dynamic playbackData = playbackJson;
            switch (control)
            {
                case "Resume/Pause":
                    try
                    {
                        bool resume = playbackData.is_playing;
                        if (resume)
                        {
                            pauseTrack().Wait();
                        }
                        else
                        {
                            resumeTrack().Wait();
                        }
                    }
                    catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException)
                    {
                        int i = 0;
                        i++;
                    }
                    break;

                case "Next":
                    nextTrack().Wait();
                    break;

                case "Previous":
                    previousTrack().Wait();
                    break;
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
                    case "loudness":
                        returnable = trackFeaturesData.loudness;
                        break;
                }
            }
            return returnable;
        }

        public void startTimer()
        {
            timer.Elapsed += new ElapsedEventHandler(timeEvent);
            timer.Interval = 1000;
            timer.Start();
        }

        public void stopTimer()
        {
            timer.Elapsed += null;
            timer.Stop();
        }

        public void timeEvent(object source, ElapsedEventArgs e)
        {
            timer.Stop();
            try
            {
                currentPlayback().Wait();
            }
            catch (Exception error)
            {
                Console.WriteLine($"Can't open two instances of a file: '{error}'\n");
            }
            string currentId = currentID();
            if (currentId != songID)
            {
                songID = currentId;
                NewSong = true;
                Thread.Sleep(50);
            }
            NewSong = false;
            timer.Start();
        }
    }
}
