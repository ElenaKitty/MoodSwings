using System;
using System.Text;
using SimpleTCP;

namespace MoodSwing
{
    class ArduinoComs
    {
        SimpleTcpClient client;
        SpotifyAPI spotify;
        private String playerCmd;

        public ArduinoComs(SpotifyAPI spotify)
        {
            this.spotify = spotify;
        }
        public void Serverclientside_Load()
        {
            client = new SimpleTcpClient();
            client.StringEncoder = Encoding.UTF8;
            client.DataReceived += Client_DataReceived;
        }

        public void Connect(String IpAddress, String port)
        {
            try
            {
                client.Connect(IpAddress, Convert.ToInt32(port));
            }
            catch (System.Net.Sockets.SocketException e)
            {
                Console.WriteLine($"Couldn't connect to current socket: '{e}'");
            }

        }

        private void Client_DataReceived(object sender, SimpleTCP.Message message)
        {
            message.ReplyLine(message.MessageString);
            playerCmd = message.MessageString;
            if (playerCmd != "")
            {

                spotify.playerControl(playerCmd);
                playerCmd = "";
            }
        }

        public void Send(String message)
        {
            client.WriteLineAndGetReply(message, TimeSpan.FromSeconds(3));
        }
    }
}
