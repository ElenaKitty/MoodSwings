using System;
using System.Threading;

namespace MoodSwing
{
    class Program
    {
        static void Main(string[] args)
        {
            ArduinoComs coms = new ArduinoComs();
            SpotifyAuth auth = new SpotifyAuth();
            String filepath = @"..\..\Resources\CurrentTrack.json";
            String input = "";
            Console.WriteLine(auth.getAuth().Result);
            SpotifyAPI spotify = new SpotifyAPI(auth.getAuth().Result);
            coms.Serverclientside_Load();
            coms.Connect("172.20.10.2", "80");
            while (input != "stop")
            {
                //input = Console.ReadLine();
                if (input == "try")
                {
                    spotify.spotifyAPIRequest(filepath);
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

                if (coms.PlayerCmd != "")
                {
                    String command = coms.PlayerCmd;
                    coms.PlayerCmd = "";
                    spotify.playerControl(command);
                }
            }
        }
    }
}
