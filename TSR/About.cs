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
    public partial class About : Form
    {
        int screenw, screenh, backw, backh, butw, buth;
        Rectangle back, about, text;
        SoundPlayer click = new SoundPlayer("Audios/Click.wav");

        public About()
        {
            InitializeComponent();
            initGraphics();
        }

        private void initGraphics()
        {
            screenw = this.Width;
            screenh = this.Height;
            backw = screenw / 10;
            backh = screenh / 10;
            butw = screenw / 14;
            buth = screenh / 14;

        }

        private void About_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            string s = "  Welcome to Top Speed Racing! The main objective of the game is to pass by cars and use gadgets while trying to achieve a high score. The game is designed to test your driving skills, reflexes, and strategy. As you progress through the game, you’ll encounter various obstacles and challenges that will test your abilities even further. You’ll need to use the right key button to go right and the left key button to make right so sure you have good control over your vehicle.";
            Font F = new Font("HIGH SPEED", screenw / 80);
            back = new Rectangle(screenw - (11 * backw) / 10, screenh - (11 * backh) / 10, backw, backh);
            text = new Rectangle(screenw / 4 - butw * 2, screenh / 4, (screenw * 3) / 4, (screenh * 3) / 4);
            g.DrawImage(Image.FromFile("Images/Background.jpg"), 0, 0, screenw, screenh);
            g.DrawImage(Image.FromFile("Images/TSRControls.png"), 0, 0, screenw / 3, screenh / 5);
            g.DrawImage(Image.FromFile("Images/Back.png"), back);
            g.DrawString(s, F, new SolidBrush(Color.FromArgb(203, 102, 15)), text);
            g.DrawImage(Image.FromFile("Images/Left.png"), screenw / 4, (3 * screenh) / 4, butw, buth);
            g.DrawImage(Image.FromFile("Images/Right.png"), screenw / 4 + butw, (3 * screenh) / 4, butw, buth);
            g.DrawString("Controls: ", F, new SolidBrush(Color.FromArgb(203, 102, 15)), screenw / 4 - butw * 2, (3 * screenh) / 4);
            g.DrawString("CAR HORN:", F, new SolidBrush(Color.FromArgb(203, 102, 15)), screenw / 2, (3 * screenh) / 4);
            g.DrawImage(Image.FromFile("Images/H.png"), screenw / 2 + 2 * butw, (3 * screenh) / 4, butw, buth);
        }



        private void About_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void About_MouseDown(object sender, MouseEventArgs e)
        {
            if (back.Contains(e.Location))
            {
                click.Play();
                this.Hide();
                Menu m1 = new Menu();
                m1.Show();
            }
        }

        private void About_Resize(object sender, EventArgs e)
        {
            initGraphics();
            this.Invalidate();
        }

        private void About_Load(object sender, EventArgs e)
        {

        }
    }
}