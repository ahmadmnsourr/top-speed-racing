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
    public partial class Mode : Form
    {
        private Var varr = new Var();
        private int screenw, screenh, escw, esch, alpha = 0, butw, buth, boxw, boxh;
        bool song = true;
        Rectangle classicrec, powerrec;
        SoundPlayer click = new SoundPlayer("Audios/Click.wav");

        public Mode()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            fade.Start();
            fadeout.Start();
            initGraphics();
        }

        private void initGraphics()
        {
            screenw = this.Width;
            screenh = this.Height;
            escw = screenw / 13;
            esch = screenh / 10;
            butw = screenw / 3;
            buth = screenh / 10;
            boxw = screenw / 4;
            boxh = screenh / 8;
        }

        private void Mode_Resize(object sender, EventArgs e)
        {

            initGraphics();
            this.Invalidate();
        }

        private void Mode_KeyDown(object sender, KeyEventArgs e)
        {
           
        }

        private void Mode_Paint(object sender, PaintEventArgs e)
        {
            powerrec = new Rectangle(screenw / 4 + (boxw * 3) / 2, screenh / 4 + 2 * boxh, boxw, boxh);
            classicrec = new Rectangle(screenw / 4, screenh / 4 + 2 * boxh, boxw, boxh);
            Font F1 = new Font("Impact", (screenw * 12) / 700, FontStyle.Italic);
            Graphics g = e.Graphics;
            g.DrawImage(Image.FromFile("Images/Background.jpg"), 0, 0, screenw, screenh);
            g.DrawImage(Image.FromFile("Images/TSR.png"), 0, 0, screenw / 3, screenh / 5);
            g.DrawImage(Image.FromFile("Images/Classic.png"), classicrec);
            g.DrawImage(Image.FromFile("Images/Power.png"), powerrec);
            g.DrawString("Please select a game mode", F1, new SolidBrush(Color.FromArgb(alpha, 255, 255, 255)), screenw / 2 - escw - 30, screenh - esch);
        }

        private void Mode_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void Mode_MouseDown(object sender, MouseEventArgs e)
        {
            if (classicrec.Contains(e.Location))
            {
                Var.classicgame = true;
                click.Play();
                this.Hide();
                ClassicGame con = new ClassicGame();
                con.Show();
            }
            if (powerrec.Contains(e.Location))
            {
                Var.classicgame = false;
                click.Play();
                this.Hide();
                Game gg = new Game();
                gg.Show();
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

        private void Mode_Load(object sender, EventArgs e)
        {

        }
    }
}
