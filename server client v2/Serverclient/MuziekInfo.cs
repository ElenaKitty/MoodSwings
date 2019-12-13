using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serverclient
{
    class MuziekInfo
    {
        SpotifyAPI api = new SpotifyAPI("BQBRrkMHyQtrbdSzsiNbcE6v9gxJOW7gNdzQkwajTvQJcp-KXF-Q4YN1qrtj3EfThbtmIbS85gvbxlFZVcJUdLvEFiNbXozcDZNjeltPVBRq1m4MiElqW_MGovQTAHH7JUf2dB5wkA");
        public MuziekInfo()
        {

        }
        public float BPM { get; set; }

        public string Artiest { get; set; }

        public string Track { get; set; }

        public float Energie { get; set; }

        public float DanceAbility { get; set; }
        public void Muziekdata()
        {
            BPM = api.getData("bpm");
            Artiest = api.getData("artist");
            Track = api.getData("track");
            Energie = api.getData("energy");
            DanceAbility = api.getData("danceability");
        }

        public string Lichteffecten()
        {
            string effect = "";
            if (BPM <= 170 && Energie <= 0.8)
            {
                effect = "fade";
            }
            else if (BPM >= 170 && Energie >= 0.8)
            {
                effect = "fade and blink";
            }
            else if (BPM <= 190 && Energie >= 0.9)
            {
                effect = "blink";
            }
            else if (BPM <= 100 && Energie >= 0.8)
            {
                effect = "fade";
            }
            else if (BPM <= 80 && Energie <= 0.6)
            {
                effect = "blink";
            }
            else
            {
                effect = "blink";
            }
            return effect;
        }
    }
}
