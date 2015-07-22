using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Video_Player
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            axWindowsMediaPlayer1.SendToBack();
            TopMost = true;
            panel1.BackColor = BackColor;
            TransparencyKey = BackColor;
            this.WindowState = FormWindowState.Normal;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Bounds = Screen.PrimaryScreen.Bounds;
            //Rectangle bb = new Rectangle(Bounds.X, Bounds.Y, Bounds.Width, Bounds.Height + 45);
            axWindowsMediaPlayer1.Bounds = Bounds;
            panel1.Bounds = Bounds;
            axWindowsMediaPlayer1.uiMode = "none";
            //axWindowsMediaPlayer1.Size = new Size(bb.Height, bb.Width);

        }
        protected override void WndProc(ref Message m)
        {
            const int WM_NCHITTEST = 0x0084;
            const int HTTRANSPARENT = (-1);

            if (m.Msg == WM_NCHITTEST)
            {
                m.Result = (IntPtr)HTTRANSPARENT;
            }
            else
            {
                base.WndProc(ref m);
            }
        }
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x00000020; //WS_EX_TRANSPARENT
                return cp;
            }
        }

        private void axWindowsMediaPlayer1_Enter(object sender, EventArgs e)
        {

        }

        private void axWindowsMediaPlayer1_MediaError(object sender, AxWMPLib._WMPOCXEvents_MediaErrorEvent e)
        {
            try
            // If the Player encounters a corrupt or missing file, 
            // show the hexadecimal error code and URL.
            {
            }
            catch (InvalidCastException)
            // In case pMediaObject is not an IWMPMedia item.
            {
                MessageBox.Show("Error in file.");
            } 
        }
    }
}
