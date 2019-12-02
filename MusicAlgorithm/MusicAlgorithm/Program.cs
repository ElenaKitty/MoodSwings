using System;
using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Collections.Generic;
<<<<<<< HEAD
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http.Extensions;
using SpotifyAPI.Web.Auth;
using SpotifyAPI.Web.Enums;
using SpotifyAPI.Web.Models;
using SpotifyAPI.Web;
=======
>>>>>>> Development

namespace MusicAlgorithm
{
    class Program
    {
        static void Main(string[] args)
        {
<<<<<<< HEAD
            //AuthorizationCodeAuth auth = new AuthorizationCodeAuth(
            //"5dc276b9432a4b55b0e1070fa5569441",
            //"4b3ee52fd2ef44d4a6ae7a51520d8170",
            //"https://mysite.com/callback/",
            //"https://mysite.com/callback/",
            //Scope.PlaylistReadPrivate | Scope.UserReadCurrentlyPlaying
            //);

            //auth.AuthReceived += async (sender, payload) =>
            //{
            //    auth.Stop();
            //    Token token = await auth.ExchangeCode(payload.Code);
            //    SpotifyWebAPI api = new SpotifyWebAPI()
            //    {
            //        TokenType = token.TokenType,
            //        AccessToken = token.AccessToken,
            //    };
            //    // Do requests with API client

            //};
            //auth.Start(); // Starts an internal HTTP Server
            //auth.OpenBrowser();


            SpotifyAPI spotify = new SpotifyAPI("BQAEk1iDDYaaM08-Wxkr9ByQdyUWb609bGSfLtNXjbpajYnOZKkDGG7qG6n-lReYdLOZhujoOL-djyJ76pEQ7YmGwj8T5fK3So-mYGjVFy5pM1x-MpiOU3_D62duvHIfpCWaOihHTgV9t_a7RovQaMb8anlsUtIgKg");
=======
            SpotifyAPI spotify = new SpotifyAPI("BQByIoz7VyOwosoRtO3Si3Y5cERSjQ5xs8N8bUWoweoNJKlSsAgTkpEMSybiOIehT6GZOLDMZq1lAU-qAlfZqXya0NBRJAFPPnmSDL2AiLsbLywmUMVJmsxDrhSJjEqxUtcyuH199aHHjAia0d3jm4iojQ4qQlOF7w");
>>>>>>> Development
            String filepath = @"C:\Users\bodhi\source\repos\MusicAlgorithm\MusicAlgorithm\Resources\CurrentTrack.json";
            String input = "";
            while (input != "stop")
            {
                input = Console.ReadLine();
                if (input == "try")
                {
<<<<<<< HEAD

                    spotify.getCurrentTrack();
                    Console.ReadLine();
                    spotify.getTrackFeatures(filepath);
                    Console.ReadLine();
                    spotify.getTrackAnalysis(filepath);
                    Console.ReadLine();

                }
            }
=======
                    spotify.getCurrentTrack();
                    Console.ReadLine();
                    spotify.getTrackFeatures(filepath);
                    Console.WriteLine();
                    spotify.getTrackAnalysis(filepath);
                }
            }
            Song song = new Song(spotify.getTrackName(), spotify.getArtistName(), spotify.getDanceAbility(), spotify.getEnergy(), spotify.getBPM());
>>>>>>> Development
            Console.ReadLine();
        }
    }
}
