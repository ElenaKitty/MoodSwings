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
            SpotifyAPI spotify = new SpotifyAPI("BQByIoz7VyOwosoRtO3Si3Y5cERSjQ5xs8N8bUWoweoNJKlSsAgTkpEMSybiOIehT6GZOLDMZq1lAU-qAlfZqXya0NBRJAFPPnmSDL2AiLsbLywmUMVJmsxDrhSJjEqxUtcyuH199aHHjAia0d3jm4iojQ4qQlOF7w");
            String filepath = @"C:\Users\bodhi\source\repos\MusicAlgorithm\MusicAlgorithm\Resources\CurrentTrack.json";
            String input = "";
            while (input != "stop")
            {
                input = Console.ReadLine();
                if (input == "try")
                {
                    spotify.getCurrentTrack();
                    Console.ReadLine();
                    spotify.getTrackFeatures(filepath);
                    Console.WriteLine();
                    spotify.getTrackAnalysis(filepath);
                }
            }
            Song song = new Song(spotify.getTrackName(), spotify.getArtistName(), spotify.getDanceAbility(), spotify.getEnergy(), spotify.getBPM());
            Console.ReadLine();
        }
    }
}
