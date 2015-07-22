using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using global::YoutubeExtractor;
using Newtonsoft.Json;

namespace Video_Player
{
    public partial class Form2 : Form
    {
        bool process = false;
        TrackBar trackBar1;
        Point loca;
        Form1 f1;
        WMPLib.IWMPPlaylist pl;
        WMPLib.IWMPPlaylistArray plItems;
        List<WMPLib.IWMPMedia> itemm;
        public Form2()
        {
            InitializeComponent();
            f1 = new Form1();
            trackBar1 = new TrackBar();
            Controls.Add(trackBar1);
            groupBox3.Controls.Add(trackBar1);
            loca = new Point(comboBox1.Location.X, comboBox1.Location.Y);
            trackBar1.Location = loca;
            trackBar1.Size = new Size(trackBar1.Size.Width + 50, trackBar1.Size.Height - 50);
            trackBar1.TickFrequency = 10;
            trackBar1.LargeChange = 3;
            trackBar1.SmallChange = 2;
            trackBar1.Maximum = 100;
            trackBar1.Minimum = 0;
            trackBar1.TickStyle = TickStyle.BottomRight;
            this.trackBar1.Scroll += new System.EventHandler(this.trackBar1_Scroll);

            string tr = comboBox1.Text.Trim('%');
            f1.Opacity = double.Parse(tr) / 100.0;

                pl = f1.axWindowsMediaPlayer1.playlistCollection.newPlaylist("list1");//axwindowsMediaPlayer.playlistCollection.newPlaylist(myPlaylist);
            f1.axWindowsMediaPlayer1.currentPlaylist = pl;
            itemm = new List<WMPLib.IWMPMedia>();
            f1.Show();

            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult dr = openFileDialog1.ShowDialog();
            if (dr.ToString() == "OK")
            {
                process = true;
                richTextBox1.Text = openFileDialog1.FileName;
                WMPLib.IWMPMedia temp = f1.axWindowsMediaPlayer1.newMedia(openFileDialog1.FileName);
                itemm.Add(temp);
                pl.appendItem(temp);
                pl.insertItem(0, temp);
                listBox1.Items.Add(temp.sourceURL);
                //f1.axWindowsMediaPlayer1.URL = openFileDialog1.FileName;
                trackBar2.Minimum = 0;
                //trackBar2.Maximum = Convert.ToInt32(f1.axWindowsMediaPlayer1.currentMedia.duration);
                label5.Text = f1.axWindowsMediaPlayer1.currentMedia.duration.ToString();
            }
        }



        private void trackBar1_Scroll(object sender, System.EventArgs e)
        {
            if (process)
            {
                f1.axWindowsMediaPlayer1.settings.volume = trackBar1.Value;
            }
        }

        private void MainMenu_FormClosing(object sender, FormClosingEventArgs e)
        {
            f1.Close();
        }

        private void comboBox1_TextChanged(object sender, EventArgs e)
        {
            string tr = comboBox1.Text.Trim('%');
            f1.Opacity = double.Parse(tr) / 100.0;
            f1.Update();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (process)
            {
                f1.axWindowsMediaPlayer1.Ctlcontrols.stop();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (process)
            {
                
                f1.axWindowsMediaPlayer1.Ctlcontrols.play();
            }
            if (f1.axWindowsMediaPlayer1.playState == WMPLib.WMPPlayState.wmppsPlaying)
            {
                label5.Text = f1.axWindowsMediaPlayer1.currentMedia.durationString;
                trackBar2.Maximum = Convert.ToInt32(f1.axWindowsMediaPlayer1.currentMedia.duration);
                timer1.Start();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (process)
            {
                f1.axWindowsMediaPlayer1.Ctlcontrols.pause();
            }
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            //string link = richTextBox1.Text;
            //IEnumerable<VideoInfo> videoInfos = DownloadUrlResolver.GetDownloadUrls(link);
            //IEnumerable<VideoInfo> vv = DownloadUrlResolver.GetDownloadUrls(link);

            ////VideoInfo video = videoInfos.First(info => info.VideoFormat == VideoFormat.Standard360);
            ////f1.axWindowsMediaPlayer1.URL = video.DownloadUrl;

            //process = true;
        }

        private void trackBar2_MouseUp(object sender, MouseEventArgs e)
        {
            f1.axWindowsMediaPlayer1.Ctlcontrols.currentPosition = trackBar2.Value + 0.0;
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (process && f1.axWindowsMediaPlayer1.playState==WMPLib.WMPPlayState.wmppsPlaying)
            {
                trackBar2.Value = Convert.ToInt32(f1.axWindowsMediaPlayer1.Ctlcontrols.currentPosition);
                trackBar2.Update();
                Application.DoEvents();
            }
            
        }

        private void trackBar2_MouseDown(object sender, MouseEventArgs e)
        {
            timer1.Stop();
        }


    }
}
