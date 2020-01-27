using System;
using System.Threading;

namespace MoodSwing
{
    class Program
    {
        static void Main(string[] args)
        {
            SpotifyAPI spotify = new SpotifyAPI();
            ArduinoComs coms = new ArduinoComs(spotify);
            FilterClass filter = new FilterClass(spotify);
            MusicInfo muziek = new MusicInfo(spotify, filter);
            String filepath = @"..\..\Resources\CurrentTrack.json";
            String input = "";
            coms.Serverclientside_Load();
            coms.Connect("192.168.43.73", "80");
            spotify.startTimer();

            while (input != "stop")
            {
                //input = Console.ReadLine();
                if (spotify.NewSong)
                {
                    spotify.spotifyAPIRequest(filepath).Wait(); ;
                    muziek.getData();
                    Console.WriteLine(muziek.filterMusic());
                    coms.Send(muziek.filterMusic());
                    Console.WriteLine();
                }

                //if (input == "try")
                //{
                //    spotify.spotifyAPIRequest(filepath).Wait(); ;
                //    muziek.getData();
                //    Console.WriteLine(muziek.filterMusic());
                //    coms.Send(muziek.filterMusic());
                //    Console.WriteLine();
                //}

                //if (input == "connect")
                //{
                //    Console.Write("ipaddress: ");
                //    string ipaddress = Console.ReadLine();
                //    Console.Write("port: ");
                //    string port = Console.ReadLine();

                //    coms.Connect(ipaddress, port);
                //}

                //if (input == "send")
                //{
                //    Console.Write("send: ");
                //    coms.Send(Console.ReadLine());
                //}
                //spotify.playerControl(input).Wait();
            }
        }
    }
}
