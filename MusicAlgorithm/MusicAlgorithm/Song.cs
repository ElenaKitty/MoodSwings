using System;
using System.Collections.Generic;
using System.Text;

namespace MusicAlgorithm
{
    class Song
    {
        int duration_ms;
        float danceability;
        float energy;
        float tempo;

        public Song(int duration, float danceability, float energy, float tempo)
        {
            this.duration_ms = duration;
            this.danceability = danceability;
            this.energy = energy;
            this.tempo = tempo;
        }

        public int Duration_ms 
        { 
            get { return duration_ms; }
            set { duration_ms = value; } 
        }

        public float Danceability
        {
            get { return danceability; }
            set { danceability = value; }
        }

        public float Energy
        {
            get { return energy; }
            set { energy = value; }
        }

        public float Tempo
        {
            get { return tempo; }
            set { tempo = value; }
        }
    }
}
