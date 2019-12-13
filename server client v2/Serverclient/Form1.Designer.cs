namespace Serverclient
{
    partial class Serverclientside
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tbStatus = new System.Windows.Forms.TextBox();
            this.btSend = new System.Windows.Forms.Button();
            this.btConnect = new System.Windows.Forms.Button();
            this.tbPort = new System.Windows.Forms.TextBox();
            this.tbHost = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tbMessage = new System.Windows.Forms.TextBox();
            this.btnSongscanner = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lbArtist = new System.Windows.Forms.Label();
            this.lbTrack = new System.Windows.Forms.Label();
            this.lbBPM = new System.Windows.Forms.Label();
            this.lbEnergy = new System.Windows.Forms.Label();
            this.lbDance = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // tbStatus
            // 
            this.tbStatus.Location = new System.Drawing.Point(425, 313);
            this.tbStatus.Multiline = true;
            this.tbStatus.Name = "tbStatus";
            this.tbStatus.Size = new System.Drawing.Size(353, 125);
            this.tbStatus.TabIndex = 24;
            this.tbStatus.TextChanged += new System.EventHandler(this.TbStatus_TextChanged);
            // 
            // btSend
            // 
            this.btSend.Location = new System.Drawing.Point(810, 270);
            this.btSend.Name = "btSend";
            this.btSend.Size = new System.Drawing.Size(75, 44);
            this.btSend.TabIndex = 23;
            this.btSend.Text = "Send";
            this.btSend.UseVisualStyleBackColor = true;
            this.btSend.Click += new System.EventHandler(this.BtSend_Click);
            // 
            // btConnect
            // 
            this.btConnect.Location = new System.Drawing.Point(810, 75);
            this.btConnect.Name = "btConnect";
            this.btConnect.Size = new System.Drawing.Size(75, 44);
            this.btConnect.TabIndex = 22;
            this.btConnect.Text = "Connect";
            this.btConnect.UseVisualStyleBackColor = true;
            this.btConnect.Click += new System.EventHandler(this.BtConnect_Click);
            // 
            // tbPort
            // 
            this.tbPort.Location = new System.Drawing.Point(678, 95);
            this.tbPort.Name = "tbPort";
            this.tbPort.Size = new System.Drawing.Size(100, 22);
            this.tbPort.TabIndex = 21;
            this.tbPort.Text = "80";
            // 
            // tbHost
            // 
            this.tbHost.Location = new System.Drawing.Point(678, 57);
            this.tbHost.Name = "tbHost";
            this.tbHost.Size = new System.Drawing.Size(100, 22);
            this.tbHost.TabIndex = 20;
            this.tbHost.Text = "192.168.200.56";
            this.tbHost.TextChanged += new System.EventHandler(this.TbHost_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(634, 95);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(38, 17);
            this.label5.TabIndex = 19;
            this.label5.Text = "Port:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(631, 57);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 17);
            this.label4.TabIndex = 18;
            this.label4.Text = "Host:";
            // 
            // tbMessage
            // 
            this.tbMessage.Location = new System.Drawing.Point(425, 158);
            this.tbMessage.Multiline = true;
            this.tbMessage.Name = "tbMessage";
            this.tbMessage.Size = new System.Drawing.Size(353, 113);
            this.tbMessage.TabIndex = 25;
            // 
            // btnSongscanner
            // 
            this.btnSongscanner.Location = new System.Drawing.Point(93, 291);
            this.btnSongscanner.Name = "btnSongscanner";
            this.btnSongscanner.Size = new System.Drawing.Size(75, 23);
            this.btnSongscanner.TabIndex = 26;
            this.btnSongscanner.Text = "GO";
            this.btnSongscanner.UseVisualStyleBackColor = true;
            this.btnSongscanner.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(40, 152);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 17);
            this.label1.TabIndex = 27;
            this.label1.Text = "Artist:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(36, 177);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 17);
            this.label2.TabIndex = 28;
            this.label2.Text = "Track:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(36, 202);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 17);
            this.label3.TabIndex = 29;
            this.label3.Text = "BPM:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(36, 226);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(57, 17);
            this.label6.TabIndex = 30;
            this.label6.Text = "Energy:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(4, 251);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(89, 17);
            this.label7.TabIndex = 31;
            this.label7.Text = "Danceability:";
            // 
            // lbArtist
            // 
            this.lbArtist.AutoSize = true;
            this.lbArtist.Location = new System.Drawing.Point(91, 152);
            this.lbArtist.Name = "lbArtist";
            this.lbArtist.Size = new System.Drawing.Size(46, 17);
            this.lbArtist.TabIndex = 32;
            this.lbArtist.Text = "label8";
            // 
            // lbTrack
            // 
            this.lbTrack.AutoSize = true;
            this.lbTrack.Location = new System.Drawing.Point(90, 177);
            this.lbTrack.Name = "lbTrack";
            this.lbTrack.Size = new System.Drawing.Size(46, 17);
            this.lbTrack.TabIndex = 33;
            this.lbTrack.Text = "label9";
            // 
            // lbBPM
            // 
            this.lbBPM.AutoSize = true;
            this.lbBPM.Location = new System.Drawing.Point(91, 202);
            this.lbBPM.Name = "lbBPM";
            this.lbBPM.Size = new System.Drawing.Size(54, 17);
            this.lbBPM.TabIndex = 34;
            this.lbBPM.Text = "label10";
            // 
            // lbEnergy
            // 
            this.lbEnergy.AutoSize = true;
            this.lbEnergy.Location = new System.Drawing.Point(91, 226);
            this.lbEnergy.Name = "lbEnergy";
            this.lbEnergy.Size = new System.Drawing.Size(54, 17);
            this.lbEnergy.TabIndex = 35;
            this.lbEnergy.Text = "label11";
            // 
            // lbDance
            // 
            this.lbDance.AutoSize = true;
            this.lbDance.Location = new System.Drawing.Point(91, 251);
            this.lbDance.Name = "lbDance";
            this.lbDance.Size = new System.Drawing.Size(54, 17);
            this.lbDance.TabIndex = 36;
            this.lbDance.Text = "label12";
            // 
            // Serverclientside
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(973, 450);
            this.Controls.Add(this.lbDance);
            this.Controls.Add(this.lbEnergy);
            this.Controls.Add(this.lbBPM);
            this.Controls.Add(this.lbTrack);
            this.Controls.Add(this.lbArtist);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnSongscanner);
            this.Controls.Add(this.tbMessage);
            this.Controls.Add(this.tbStatus);
            this.Controls.Add(this.btSend);
            this.Controls.Add(this.btConnect);
            this.Controls.Add(this.tbPort);
            this.Controls.Add(this.tbHost);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Name = "Serverclientside";
            this.Text = "Serverclient";
            this.Load += new System.EventHandler(this.Serverclientside_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbStatus;
        private System.Windows.Forms.Button btSend;
        private System.Windows.Forms.Button btConnect;
        private System.Windows.Forms.TextBox tbPort;
        private System.Windows.Forms.TextBox tbHost;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbMessage;
        private System.Windows.Forms.Button btnSongscanner;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lbArtist;
        private System.Windows.Forms.Label lbTrack;
        private System.Windows.Forms.Label lbBPM;
        private System.Windows.Forms.Label lbEnergy;
        private System.Windows.Forms.Label lbDance;
    }
}

