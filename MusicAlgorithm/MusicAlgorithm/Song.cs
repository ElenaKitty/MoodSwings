using System;
using System.Collections.Generic;
using System.Text;

namespace MusicAlgorithm
{
    class Song
    {
        String songName;
        String artistName;
        float danceability;
        float energy;
        float tempo;

        public Song(String songName, String artistName, float danceability, float energy, float tempo)
        {
            this.songName = songName;
            this.artistName = artistName;
            this.danceability = danceability;
            this.energy = energy;
            this.tempo = tempo;
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
