using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TSR
{
    public partial class LoadingScreen : Form
    {
        private int screenw, screenh, escw, esch, alpha = 0, butw, buth;
        bool song = true;

        public LoadingScreen()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            fade.Start();
            fadeout.Start();
            initGraphics();
            playintro(song);
        }

        private void initGraphics()
        {
            screenw = this.Width;
            screenh = this.Height;
            escw = screenw / 13;
            esch = screenh / 10;
            butw = screenw / 3;
            buth = screenh / 10;
        }

        private void LoadingScreen_Resize(object sender, EventArgs e)
        {
            initGraphics();
            this.Invalidate();
        }

        private void LoadingScreen_Paint(object sender, PaintEventArgs e)
        {

            Font F1 = new Font("Impact", (screenw * 12) / 700, FontStyle.Italic);
            Graphics g = e.Graphics;
            g.DrawImage(Image.FromFile("Images/Background.jpg"), 0, 0, screenw, screenh);
            g.DrawImage(Image.FromFile("Images/TSR.png"), 0, 0, screenw / 3, screenh / 5);
            g.DrawString("Press enter to continue", F1, new SolidBrush(Color.FromArgb(alpha, 255, 255, 255)), screenw / 2 - escw - 30, screenh - esch);
        }

        private void LoadingScreen_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter)
            {
                this.Hide();
                Map m1 = new Map();
                m1.Show();
                song = false;
                playintro(song);
            }
        }

        private void LoadingScreen_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
        private void playintro(bool play)
        {
            SoundPlayer player2 = new SoundPlayer("Audios/Intro.wav");
            if (play == true)
            {
                player2.PlayLooping();
            }
            if (play == false)
            {
                player2.Stop();
            }
        }

        private void fade_Tick(object sender, EventArgs e)
        {

            if (alpha + 10 > 255)
            {
                alpha = 255;
                fade.Stop();
                fadeout.Start();
            }
            else if (alpha <= 255)
            {
                alpha += 10;
            }
            this.Invalidate();
        }

        private void fadeout_Tick(object sender, EventArgs e)
        {
            if (alpha - 10 < 0)
            {
                alpha = 0;
                int tmp = fadeout.Interval;
                fadeout.Interval = fade.Interval;
                fade.Interval = tmp;
                fade.Start();
                fadeout.Stop();
            }
            else if (alpha >= 0)
            {
                alpha -= 10;
            }
            this.Invalidate();
        }

        private void LoadingScreen_Load(object sender, EventArgs e)
        {

        }
    }
}
