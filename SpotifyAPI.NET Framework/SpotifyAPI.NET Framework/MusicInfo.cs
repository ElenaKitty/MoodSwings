using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoodSwing
{
    class MusicInfo
    {
        SpotifyAPI spotify;
        FilterClass filter;
        bool succeeded = false;

        public MusicInfo(SpotifyAPI spotify, FilterClass filter)
        {
            this.spotify = spotify;
            this.filter = filter;
        }

        public int BPM { get; set; }

        public string Artiest { get; set; }

        public string Track { get; set; }

        public float Energie { get; set; }

        public float DanceAbility { get; set; }

        public float Valence { get; set; }

        public void getData()
        {
            try
            {
                double bpmData = spotify.getData("bpm");
                BPM = (int)Math.Round(bpmData);
                Artiest = spotify.getData("artist");
                Track = spotify.getData("track");
                Energie = spotify.getData("energy");
                DanceAbility = spotify.getData("danceability");
                Valence = spotify.getData("valence");
                succeeded = true;
            }
            catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException e)
            {
                Console.WriteLine($"No data available for this song '{e}'\n");
                succeeded = false;
            }

        }

        public String filterMusic()
        {
            String sendString = "";
            if (succeeded)
            {
                sendString = "#" + Artiest + "^" + Track + "^" + filter.energyFilter() + "^" + filter.BPMformula() + "^" + filter.valencefilter() + "^$";
            }
            return sendString;
        }
    }
}
