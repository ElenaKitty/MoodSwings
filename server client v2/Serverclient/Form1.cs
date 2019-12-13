using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;
using SimpleTCP;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;

namespace Serverclient
{
    public partial class Serverclientside : Form
    {
        SpotifyAPI api = new SpotifyAPI("BQBRrkMHyQtrbdSzsiNbcE6v9gxJOW7gNdzQkwajTvQJcp-KXF-Q4YN1qrtj3EfThbtmIbS85gvbxlFZVcJUdLvEFiNbXozcDZNjeltPVBRq1m4MiElqW_MGovQTAHH7JUf2dB5wkA");
        public Serverclientside()
        {
            InitializeComponent();
        }

        SimpleTcpClient client;

        private void Serverclientside_Load(object sender, EventArgs e)
        {
            // simpletcp setup
            client = new SimpleTcpClient();
            client.StringEncoder = Encoding.UTF8;
            client.DataReceived += Client_DataReceived;
        }

        //   private void Jsonloading()
        //   {
        //       using (System.IO.StreamReader r = new System.IO.StreamReader(src))
        //       {
        //           string json = r.ReadToEnd();
        //           Muziekinfo item = Newtonsoft.Json.JsonConvert.DeserializeObject<Muziekinfo>(json);
        //       }
        //   }
        private void TbHost_TextChanged(object sender, EventArgs e)
        {

        }

        private void BtConnect_Click(object sender, EventArgs e)
        {
            btConnect.Enabled = false;
            //Connect to server
            client.Connect(tbHost.Text, Convert.ToInt32(tbPort.Text));
        }

        private void Client_DataReceived(object sender, SimpleTCP.Message e)
        {
            //Update message to tbStatus
            tbStatus.Invoke((MethodInvoker)delegate ()
            {
                tbStatus.Text += e.MessageString;
            });
        }
        private void BtSend_Click(object sender, EventArgs e)
        {
            // message check when clicking the button
            client.WriteLineAndGetReply(tbMessage.Text, TimeSpan.FromSeconds(3));
        }

        private void TbStatus_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            MuziekInfo info = new MuziekInfo();
            info.Muziekdata();

            lbBPM.Text = Convert.ToString(info.BPM);
            lbArtist.Text = Convert.ToString(info.Artiest);
            lbTrack.Text = Convert.ToString(info.Track);
            lbEnergy.Text = Convert.ToString(info.Energie);
            lbDance.Text = Convert.ToString(info.DanceAbility);

            client.WriteLineAndGetReply("#" + lbArtist.Text + "," + lbTrack.Text + "," + info.Lichteffecten() + "," + lbBPM.Text + "$", TimeSpan.FromSeconds(3));
        }
    }
}
