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
    public partial class Menu : Form
    {
        int screenw, screenh, boxw, boxh;
        Rectangle exitgame, restart, controls, quittomainmain;
        SoundPlayer click = new SoundPlayer("Audios/Click.wav");
        public Menu()
        {
            InitializeComponent();
            initGraphics();
        }

        private void initGraphics()
        {
            screenw = this.Width;
            screenh = this.Height;
            boxw = screenw / 4;
            boxh = screenh / 8;
        }

        private void Menu_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
   
        private void Menu_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            exitgame = new Rectangle(screenw / 4 + (boxw * 3) / 2, screenh / 4 + 2 * boxh, boxw, boxh);
            controls = new Rectangle(screenw / 4, screenh / 4 + 2 * boxh, boxw, boxh);
            restart = new Rectangle(screenw / 4, screenh / 4, boxw, boxh);
            quittomainmain = new Rectangle(screenw / 4 + (boxw * 3) / 2, screenh / 4, boxw, boxh);
            g.DrawImage(Image.FromFile("Images/Background.jpg"), 0, 0, screenw, screenh);
            g.DrawImage(Image.FromFile("Images/TSRmenu.png"), 0, 0, screenw / 3, screenh / 5);
            g.DrawImage(Image.FromFile("Images/Exitgame.png"), exitgame);
            g.DrawImage(Image.FromFile("Images/Restart.png"), restart);
            g.DrawImage(Image.FromFile("Images/Controls.png"), controls);
            g.DrawImage(Image.FromFile("Images/QuitToMain.png"), quittomainmain);
        }

        private void Menu_Resize(object sender, EventArgs e)
        {
            initGraphics();
            this.Invalidate();
        }

        private void Menu_MouseDown(object sender, MouseEventArgs e)
        {
            if (exitgame.Contains(e.Location))
            {
               Application.Exit(); 
            }

            if (restart.Contains(e.Location))
            {
                if (Var.classicgame == true)
                {
                    click.Play();
                    this.Hide();
                    ClassicGame cgame = new ClassicGame();
                    cgame.Show();
                }
                else{
                    click.Play();
                    this.Hide();
                    Game game = new Game();
                    game.Show();
                }
            }

            if (controls.Contains(e.Location))
            {
                click.Play();
                this.Hide();
                About con = new About();
                con.Show();
            }
            if (quittomainmain.Contains(e.Location))
            {
                click.Play();
                this.Hide();
                LoadingScreen load = new LoadingScreen();
                load.Show();
            }
        }

        private void Menu_Load(object sender, EventArgs e)
        {

        }
    }
}