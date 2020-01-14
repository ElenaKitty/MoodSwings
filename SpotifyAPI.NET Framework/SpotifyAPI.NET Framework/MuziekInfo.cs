using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoodSwing
{
    class MuziekInfo
    {
        SpotifyAPI spotify;
        FilterClass filter;

        public MuziekInfo(SpotifyAPI spotify, FilterClass filter)
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
            double bpmData = spotify.getData("bpm");
            BPM = (int)Math.Round(bpmData);
            Artiest = spotify.getData("artist");
            Track = spotify.getData("track");
            Energie = spotify.getData("energy");
            DanceAbility = spotify.getData("danceability");
            Valence = spotify.getData("valence");
        }

        public String filterMusic()
        {
            String sendString = "#" + Artiest + "," + Track + "," + filter.BPMformula() + "," + filter.Energiefilter() + "," + filter.Valencefilter() + ",$";
            return sendString;
        }
    }
}
