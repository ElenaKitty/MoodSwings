using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Serverclient
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            string filepath = @"Resources\CurrentTrack.json";
            SpotifyAPI spotify = new SpotifyAPI("BQBRrkMHyQtrbdSzsiNbcE6v9gxJOW7gNdzQkwajTvQJcp-KXF-Q4YN1qrtj3EfThbtmIbS85gvbxlFZVcJUdLvEFiNbXozcDZNjeltPVBRq1m4MiElqW_MGovQTAHH7JUf2dB5wkA");
            spotify.getCurrentTrack();
            spotify.getTrackFeatures(filepath);
            spotify.getTrackAnalysis(filepath);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Serverclientside());
        }
    }
}
