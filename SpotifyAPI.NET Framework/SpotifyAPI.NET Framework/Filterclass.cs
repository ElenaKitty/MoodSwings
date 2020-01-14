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
            return beatspeed;
        }

        public string Energiefilter()
        {
            double energy;
            energy = spotify.getData("energy");
            string Energiecategorie;
            if (energy >= lowEnergy && energy <= highEnergy)
            {
                Energiecategorie = "fade";
            }
            else if (energy > highEnergy)
            {
                Energiecategorie = "blink";
            }
            else
            {
                Energiecategorie = "slide";
            }

            return Energiecategorie;
        }

        public string Valencefilter()
        {
            double valence;
            valence = spotify.getData("valence");
            string Valencecategorie;
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