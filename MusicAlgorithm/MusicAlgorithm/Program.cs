using System;
using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Collections.Generic;

namespace MusicAlgorithm
{
    class Program
    {
        static void Main(string[] args)
        {
            SpotifyAPI spotify = new SpotifyAPI("BQDA7RGEeZBnOUeJt53mdKX44P0VUuLPh8vhMKTJgkjKV74lyI29VqRmHu9HjSrRpMTZA2Xdn9JXn4_R2dv8awCkTdpGpl4Ty-AuxL70WyVfY0bUv1a1FDzfSasG78puCGitO-q2xiDF2JJ7eY5XgOHb8K0Ogj7SwA");

            String input = "";
            while (input != "stop")
            {
                input = Console.ReadLine();
                if (input == "try")
                {
                    spotify.getRequest("https://api.spotify.com/v1/me/player/currently-playing?market=NL");
                }
            }
        }
    }
}
