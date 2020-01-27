using System;


namespace MoodSwing
{

    class FilterClass
    {
        SpotifyAPI spotify;
        private double lowEnergy;
        private double highEnergy;
        private double lowValence;
        private double highValence;

        public FilterClass(SpotifyAPI spotify)
        {
            this.spotify = spotify;
            lowEnergy = 0.4;
            highEnergy = 0.8;
            lowValence = 0.3;
            highValence = 0.75;
        }

        public double BPMformula()
        {
            double BPM;
            double bpmData = spotify.getData("bpm");
            BPM = (int)Math.Round(bpmData);
            double beatspeed;
            beatspeed = 1 / BPM * 60;
            beatspeed = Math.Round(beatspeed * 100.0);
            return beatspeed;
        }

        public string energyFilter()
        {
            double energy;
            double loudness;
            energy = spotify.getData("energy");
            loudness = spotify.getData("loudness");
            string Energiecategorie;
            if (energy >= lowEnergy && energy <= highEnergy && loudness >= -10)
            {
                Energiecategorie = "random";
            }
            else if (energy >= lowEnergy && energy <= highEnergy && loudness < -10)
            {
                Energiecategorie = "wave";
            }
            else if (energy > highEnergy)
            {
                Energiecategorie = "blink";
            }
            else
            {
                Energiecategorie = "fade";
            }

            return Energiecategorie;
        }

        public string valencefilter()
        {
            double valence;
            valence = spotify.getData("valence");
            string Valencecategorie;
            if (energyFilter() == "fade")
            {
                if (valence >= lowValence && valence <= highValence)
                {
                    Valencecategorie = "teal/orange";
                }
                else if (valence > highValence)
                {
                    Valencecategorie = "red/yellow";
                }
                else
                {
                    Valencecategorie = "blue/purple";
                }
                return Valencecategorie;
            }

            if (valence >= lowValence && valence <= highValence)
            {
                Valencecategorie = "blue";
            }
            else if (valence > highValence)
            {
                Valencecategorie = "red";
            }
            else
            {
                Valencecategorie = "green";
            }

            return Valencecategorie;
        }

    }
}