using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleTCP;

namespace MoodSwing
{
    class ArduinoComs
    {
        SimpleTcpClient client;
        private String playerCmd;
        public void Serverclientside_Load()
        {
            client = new SimpleTcpClient();
            client.StringEncoder = Encoding.UTF8;
            client.DataReceived += Client_DataReceived;
        }

        public void Connect(String IpAddress, String port)
        {
            Console.WriteLine(IpAddress);
            client.Connect(IpAddress, Convert.ToInt32(port));
        }

        private void Client_DataReceived(object sender, SimpleTCP.Message message)
        {
            message.ReplyLine(message.MessageString);
            playerCmd = message.MessageString;
        }

        public void Send(String message)
        {
            client.WriteLineAndGetReply(message, TimeSpan.FromSeconds(3));
        }

        public String getPlayerCmd()
        {
            return playerCmd;
        }

        public String PlayerCmd { get { return playerCmd; } set { playerCmd = value; } }
    }
}
