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
            MuziekInfo muziek = new MuziekInfo(spotify, filter);
            String filepath = @"..\..\Resources\CurrentTrack.json";
            String input = "";
            coms.Serverclientside_Load();
            //coms.Connect("172.20.10.2", "80");

            while (input != "stop")
            {

                input = Console.ReadLine();

                if (input == "try")
                {
                    spotify.spotifyAPIRequest(filepath).Wait();
                    muziek.getData();
                    Console.WriteLine(muziek.filterMusic());
                    //coms.Send(muziek.filterMusic());
                    Console.WriteLine();
                }

                if (input == "connect")
                {
                    Console.Write("IpAddress: ");
                    String ipAddress = Console.ReadLine();
                    Console.Write("Port: ");
                    String port = Console.ReadLine();

                    coms.Connect(ipAddress, port);
                }

                if (input == "send")
                {
                    Console.Write("send: ");
                    coms.Send(Console.ReadLine());
                }

                spotify.playerControl(input);
            }
        }
    }
}
