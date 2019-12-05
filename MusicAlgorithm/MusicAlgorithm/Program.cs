using System;
using System.IO;
using SpotifyAPI.Web.Auth;
using SpotifyAPI.Web.Enums;
using SpotifyAPI.Web.Models;
using SpotifyAPI.Web;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace MusicAlgorithm
{
    class Program
    {
        static void Main(string[] args)
        {
            AuthorizationCodeAuth auth = new AuthorizationCodeAuth(
            "5dc276b9432a4b55b0e1070fa5569441",
            "4b3ee52fd2ef44d4a6ae7a51520d8170",
            "https://mysite.com/callback/",
            "https://mysite.com/callback/",
            Scope.PlaylistReadPrivate | Scope.UserReadCurrentlyPlaying
            );

            auth.AuthReceived += async (sender, payload) =>
            {
                auth.Stop();
                Token token = await auth.ExchangeCode(payload.Code);
                SpotifyWebAPI api = new SpotifyWebAPI()
                {
                    TokenType = token.TokenType,
                    AccessToken = token.AccessToken
                };
                // Do requests with API client
            };
            auth.Start(); // Starts an internal HTTP Server
            //auth.OpenBrowser();

            String filepath = @"..\..\..\Resources\CurrentTrack.json";
            String input = "";
            SpotifyAPI spotify = new SpotifyAPI("BQCDeeQ8S-h3nL8anfCvPkfuLwAfI8HonORoHWUXn2eNPQripA1X8GBxKnzWdZRPQhZuVt0OHrJ5vyGi_ZSkWC7H1PMP-GkLU2aAHl0WkP0p4JPVeGl_MMBHE289ZuVuiKMRdB8FId2iL4UCQzO04ayAqQKXh9aaK5U7hWOvHRC18A");
            while (input != "stop")
            {
                input = Console.ReadLine();
                if (input == "try")
                {
                    spotify.spotifyAPIRequest(filepath);
                    Console.WriteLine();

                }
                spotify.playerControl(input);
            }
        }
    }
}
