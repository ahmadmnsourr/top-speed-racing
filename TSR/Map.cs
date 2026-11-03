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
    public partial class Map : Form
    {
        private int screenw, screenh, escw, esch, alpha = 0, imgw, imgh;
        Rectangle field, desert, highway, snow;
        SoundPlayer click = new SoundPlayer("Audios/Click.wav");

        public Map()
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
            imgw = screenw / 4;
            imgh = screenh / 4;
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

        private void Map_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void Map_MouseDown(object sender, MouseEventArgs e)
        {

            if (field.Contains(e.Location))
            {
                Var.roadimg = Image.FromFile("Images/Field.png");
                click.Play();
                this.Hide();
                Mode m = new Mode();
                m.Show();
            }
            if (desert.Contains(e.Location))
            {
                Var.roadimg = Image.FromFile("Images/Desert.png");
                click.Play();
                this.Hide();
                Mode m = new Mode();
                m.Show();
            }
            if (snow.Contains(e.Location))
            {
                Var.roadimg = Image.FromFile("Images/Snow.jpg");
                click.Play();
                this.Hide();
                Mode m = new Mode();
                m.Show();
            }
            if (highway.Contains(e.Location))
            {
                Var.classicgame = true;
                Var.roadimg = Image.FromFile("Images/Highway.png");
                click.Play();
                this.Hide();
                Mode m = new Mode();
                m.Show();
            }
        }

        private void Map_Paint(object sender, PaintEventArgs e)
        {
            field = new Rectangle( screenw/4, screenh/4, imgw, imgh);
            desert = new Rectangle(screenw/4 + imgw + 30, screenh / 4, imgw, imgh);
            highway = new Rectangle(screenw/4, screenh/4 + imgh + 30, imgw, imgh);
            snow = new Rectangle (screenw / 4 +imgw + 30, screenh / 4 + imgh + 30, imgw, imgh);

            Font F1 = new Font("Impact", (screenw * 12) / 700, FontStyle.Italic);
            Graphics g = e.Graphics;
            g.DrawImage(Image.FromFile("Images/Background.jpg"), 0, 0, screenw, screenh);
            g.DrawImage(Image.FromFile("Images/TSR.png"), 0, 0, screenw / 3, screenh / 5);
            g.DrawString("Please select a map", F1, new SolidBrush(Color.FromArgb(alpha, 255, 255, 255)), screenw / 2 - escw - 30, screenh - esch);
            g.DrawImage(Image.FromFile("Images/Field.png"), field);
            g.DrawImage(Image.FromFile("Images/Desert.png"), desert);
            g.DrawImage(Image.FromFile("Images/Highway.png"), highway);
            g.DrawImage(Image.FromFile("Images/Snow.jpg"), snow);
        }

        private void Map_Resize(object sender, EventArgs e)
        {
            initGraphics();
            this.Invalidate();
        }

        private void Map_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Escape)
            {
                Application.Exit();
            }
        }
    }
}
