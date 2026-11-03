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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace TSR
{
    public partial class Game : Form
    {
        private int screenw, screenh, pausew, pauseh, road1y, road2y, roadspeed = 15;
        private int opponentx, opponenty, carw, carh, trafficspeed = 10, carx, carmovement;
        private bool goleft, goright, carcrash = true, highscoreoccured = false, isGadgetActive = false;
        private static Rectangle car, opponent1, opponent2, opponent3, gadget, staterec, menurec;
        private int gadgettick = 0, speedMultiplier = 1, score, previous_score = 0, menuw, menuh;
        bool isColliding = false, ishshieldactive = false, isweaponactive = false, isspeedactive = false;
        SoundPlayer HighScoree = new SoundPlayer("Audios/HighScore.wav"), crash = new SoundPlayer("Audios/Crash.wav"), Horn = new SoundPlayer("Audios/Horn.wav");
        SoundPlayer Click = new SoundPlayer("Audios/Click.wav");
        SoundPlayer Nitros = new SoundPlayer("Audios/Nitros.wav");
        SoundPlayer Shield = new SoundPlayer("Audios/Shield.wav");
        SoundPlayer Machinegun = new SoundPlayer("Audios/MachineGun.wav");
        Image g1 = Var.roadimg;
        Image g2 = Var.roadimg;
        Image opp1, opp2, gadgetimg, playerimg, stateimage, pauseimg, resumeimg;


        private int bulletspeed = 50;
        List<Rectangle> bullets = new List<Rectangle>();
        Image bulletimg;
        bool bullethit1 = false, bullethit2 = false;
        public Game()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            initGraphics();
            Road_timer.Start();
        }

        private void initGraphics()
        {
            screenw = this.Width;
            screenh = this.Height;
            road1y = 0;
            road2y = -screenh;
            roadspeed = 10;
            trafficspeed = 5;
            carw = screenw / 10;
            carh = screenh / 4;
            carx = screenw / 4 - 20;
            carmovement = 60;
            score = 0;
            opponent1.Y = changeobjposy();
            opponent1.X = changeobjposx();
            opponent2.Y = changeobjposy();
            opponent2.X = changeobjposx();
            opp1 = Changecarimg();
            opp2 = Changecarimg();
            gadget.X = screenw / 4;
            gadget.Y = changeobjposy();
            gadgetimg = Changegadgetimg();
            playerimg = Image.FromFile("Images/Red.png");
            pausew = screenw / 32;
            pauseh = screenh / 28;
            menuw = screenw / 30;
            menuh = screenh / 20;
            pauseimg = Image.FromFile("Images/Pauseicon.png");
            stateimage = pauseimg;
            resumeimg = Image.FromFile("Images/Resumeicon.png");
            bulletimg = Image.FromFile("Images/bullet.png");
        }

        private void Game_Paint(object sender, PaintEventArgs e)
        {
            Font HighScore = new Font("Impact", 45, FontStyle.Italic);
            Font Score = new Font("Impact", (screenw * 8) / 500, FontStyle.Italic);
            car = new Rectangle(carx, (int)(screenh / 20 + 2 * carh) + 80, carw, carh);

            opponent2 = new Rectangle(opponent2.X, opponent2.Y, carw, carh);
            opponent1 = new Rectangle(opponent1.X, opponent1.Y, carw, carh);
            gadget = new Rectangle(gadget.X, gadget.Y, carw, carh / 2);
            staterec = new Rectangle((screenw - screenw / 25), screenh / 10, pausew, pauseh);
            menurec = new Rectangle((screenw - screenw / 24), screenh / 28, menuw, menuh);
            Graphics g = e.Graphics;
            g.DrawImage(g1, 0, road1y, screenw, screenh);
            g.DrawImage(g2, 0, road2y, screenw, screenh);
            g.DrawImage(playerimg, car);
            g.DrawImage(opp1, opponent1);
            g.DrawImage(opp2, opponent2);
            g.DrawImage(gadgetimg, gadget);
            g.DrawImage(stateimage, staterec);
            g.DrawImage(Image.FromFile("Images/Menuicon.png"), menurec);
            g.DrawString("Score: " + score.ToString("D6"), Score, Brushes.Black, 10, 10);


            if (isweaponactive)
            {
                foreach (Rectangle bullet in bullets)
                {
                    g.DrawImage(bulletimg, bullet);
                }
            }


            if (collision() == true)
            {
                if ((Highscorechecker(score)) == false)
                {
                    g.DrawString("SCORE: " + score, HighScore, Brushes.Red, screenw / 4 + 200, screenh / 4 + carh / 2);
                }
                else
                {
                    HighScoree.Play();
                    g.DrawString("NEW HIGH SCORE: " + score, HighScore, Brushes.Red, screenw / 4 + 100, screenh / 4 + carh / 2);
                }
                g.DrawImage(Image.FromFile("Images/Explosion.png"), car.X - 50, car.Y - 50, 2 * screenw / 12, screenh / 5);
                g.DrawImage(Image.FromFile("Images/Gameover.png"), screenw / 4 - 20, screenh / 15, screenw / 2, screenh / 4);
            }
        }

        public void Road_timer_Tick(object sender, EventArgs e)
        {
            road1y += roadspeed * speedMultiplier;
            road2y += roadspeed * speedMultiplier;
            string scoreString = score.ToString("D6");

            if (opponent1.IntersectsWith(gadget))
            {
                gadget.Y = opponent1.Bottom + 1;
            }
            if (opponent2.IntersectsWith(gadget))
            {
                gadget.Y = opponent2.Bottom + 1;
            }

            if (CheckObjectCollision(opponent1, gadget) || CheckObjectCollision(opponent2, gadget))
            {
                gadget.Y = changeobjposy();
                gadgetimg = Changegadgetimg();
            }

            if (CheckObjectCollision(opponent1, opponent2))
            {
                opponent1.X = changeobjposx();
                opponent1.Y = changeobjposy();
                opp1 = Changecarimg();
            }

            if (road1y >= screenh)
            {
                road1y = road2y - (screenh - 4);
            }
            if (road2y >= screenh)
            {
                road2y = road1y - (screenh - 4);
            }
            if (goright == true && carx < (screenw * 3) / 4 - (carw / 4) - 50)
            {
                carx += carmovement;
                this.Invalidate();
            }
            if (goleft == true && carx > (screenw / 4) - 30)
            {
                carx -= carmovement;
                this.Invalidate();
            }
            if (score - previous_score == 40)
            {
                roadspeed += 8;
                trafficspeed += 8;
                previous_score = score;
            }

            if (gadget.Y >= screenh)
            {
                gadgetimg = Changegadgetimg();
                gadget.Y = changeobjposy();
                gadget.X = changeobjposx();
            }
            else
            {
                gadget.Y += trafficspeed;
            }

            if (opponent1.Y >= screenh || bullethit1 == true)
            {
                bullethit1 = false;
                opp1 = Changecarimg();
                opponent1.Y = changeobjposy();
                opponent1.X = changeobjposx();
            }
            else
            {
                opponent1.Y += trafficspeed;
            }

            if (opponent2.Y >= screenh || bullethit2 == true)
            {
                bullethit2 = false;
                opp2 = Changecarimg();
                opponent2.Y = changeobjposy();
                opponent2.X = changeobjposx();
            }
            else
            {
                opponent2.Y += trafficspeed;
            }

            if (isweaponactive)
            {
                Rectangle bullet = new Rectangle(car.X + 40, car.Y, 20, 40);
                bullets.Add(bullet);
                for (int i = bullets.Count - 1; i >= 0; i--)
                {
                    bullet = bullets[i];
                    bullet.Y -= bulletspeed;
                    if (bullet.Y < 0)
                    {
                        bullets.RemoveAt(i);
                    }
                    else
                    {
                        bullets[i] = bullet;
                    }
                }
            }
            collision();
            Shieldgadgetcollision();
            Speedgadgetcollision();
            Weapondgadgetcollision();
            Hitbullet();
            score++;
            this.Invalidate();
        }

        private void Weapondgadgetcollision()
        {
            if (car.IntersectsWith(gadget) && gadgetimg == GadgetImages[1] && !isGadgetActive)
            {
                isGadgetActive = true;
                Machinegun.Play();
                playerimg = Image.FromFile("Images/Tank.png");
                isweaponactive = true;
            }

            if (isweaponactive)
            {
                if (gadgettick == 80)
                {
                    playerimg = Image.FromFile("Images/Red.png");
                    isweaponactive = false;
                    gadgettick = 0;
                    isGadgetActive = false;
                    Machinegun.Stop();
                }
                else
                {
                    gadgettick++;
                }
            }
        }

        private void Hitbullet()
        {
            for(int h =0; h <bullets.Count; h++)
            {
                Rectangle bullet = bullets[h];
                if (bullet.IntersectsWith(opponent1) && isweaponactive){
                    bullets.RemoveAt(h);
                    bullethit1 = true;
                    break;
                }
            }

            for (int h = 0; h < bullets.Count; h++)
            {
                Rectangle bullet = bullets[h];
                if (bullet.IntersectsWith(opponent2) && isweaponactive)
                {
                    bullets.RemoveAt(h);
                    bullethit2 = true;
                    break;
                }
            }
        }

        private bool CheckObjectCollision(Rectangle r1, Rectangle r2)
        {
            return r1.IntersectsWith(r2);
        }

        private void Speedgadgetcollision()
        {
            if (car.IntersectsWith(gadget) && gadgetimg == GadgetImages[0] && !isGadgetActive)
            {
                isGadgetActive = true;
                playerimg = Image.FromFile("Images/fastcar.png");
                isspeedactive = true;
                Nitros.Play();
                speedMultiplier = 3;
            }

            if (isspeedactive)
            {
                if (gadgettick == 100)
                {
                    playerimg = Image.FromFile("Images/Red.png");
                    isspeedactive = false;
                    gadgettick = 0;
                    isGadgetActive = false;
                    speedMultiplier = 1;
                }
                else
                {
                    gadgettick++;
                }
            }
        }

        private bool collision()
        {
            if (carcrash == true && (car.IntersectsWith(opponent1) || car.IntersectsWith(opponent2) || car.IntersectsWith(opponent3)))
            {
                carcrash = true;
                Road_timer.Stop();
                crash.Play();
                return true;
            }
            return false;
        }

        private void Shieldgadgetcollision()
        {
            if (car.IntersectsWith(gadget) && gadgetimg == GadgetImages[2] && !isGadgetActive)
            {
                isGadgetActive = true;
                playerimg = Image.FromFile("Images/RedOpacity.png");
                carcrash = false;
                ishshieldactive = true;
                Shield.Play();
            }

            if (ishshieldactive)
            {
                if (gadgettick == 100)
                {
                    playerimg = Image.FromFile("Images/Red.png");
                    carcrash = true;
                    ishshieldactive = false;
                    gadgettick = 0;
                    isGadgetActive = false;
                }
                else
                {
                    gadgettick++;
                }
            }
        }

        private int changeobjposx()
        {
            Random randx = new Random();
            return randx.Next((screenw / 4) - 30, (screenw * 3) / 4 - (carw / 4) - 50);
        }

        private int changeobjposy()
        {
            Random randy = new Random();


            return randy.Next(-3 * screenh / 2, -screenh / 2);
        }

        private Image[] OpponentCarImages = new Image[] {
             Image.FromFile("Images/Blue.png"),
             Image.FromFile("Images/Green.png"),
             Image.FromFile("Images/moto.png"),
             Image.FromFile("Images/Police.png"),
             Image.FromFile("Images/Purple.png"),
             Image.FromFile("Images/Brown.png"),
             Image.FromFile("Images/Black.png"),
             Image.FromFile("Images/LightBlue.png"),
             Image.FromFile("Images/LightGreen.png"),
             Image.FromFile("Images/Yellow.png")};

        private Image Changecarimg()
        {
            Random changecar = new Random();
            int carImageIndex = changecar.Next(OpponentCarImages.Length);
            return OpponentCarImages[carImageIndex];
        }

        private Image[] GadgetImages = new Image[] {
             Image.FromFile("Images/SpeedBooster.png"),
             Image.FromFile("Images/Weapon.png"),
             Image.FromFile("Images/Shield.png")};

        private Image Changegadgetimg()
        {
            Random changegad = new Random();
            int gadgetImageIndex = changegad.Next(GadgetImages.Length);
            return GadgetImages[gadgetImageIndex];
        }

        private void SaveHighScore(int score)
        {
            StreamWriter write = new StreamWriter("HighScore.txt");
            write.WriteLine(score);
            write.Close();
        }

        private static int GetHighScore()
        {
            StreamReader reader = new StreamReader("HighScore.txt");
            int readedscore = Convert.ToInt32(reader.ReadLine());
            reader.Close();
            int highscore = readedscore;
            return highscore;
        }

        private bool Highscorechecker(int score)
        {
            int highscore = GetHighScore();
            if (score > highscore)
            {
                SaveHighScore(score);
                return true;
            }
            else
            {
                return false;
            }
        }

        private void Game_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Right)
            {
                goright = true;
            }
            if (e.KeyCode == Keys.Left)
            {
                goleft = true;
            }
            if (e.KeyCode == Keys.H)
            {
                Horn.Play();
            }
        }

        private void Game_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Right)
            {
                goright = false;
            }
            if (e.KeyCode == Keys.Left)
            {
                goleft = false;
            }
        }

        private void Game_Load(object sender, EventArgs e)
        {

        }

        private void Game_Resize(object sender, EventArgs e)
        {
            initGraphics();
            this.Invalidate();
        }

        private void Game_FormClosed(object sender, FormClosedEventArgs e)
        {
            StreamWriter writescoree = new StreamWriter("HighScore.txt");
            writescoree.WriteLine("0");
            writescoree.Close();
            Application.Exit();
        }

        private void Game_MouseDown(object sender, MouseEventArgs e)
        {
            if (menurec.Contains(e.Location))
            {
                Click.Play();
                this.Hide();
                Menu menu = new Menu();
                menu.Show();
            }
            if (staterec.Contains(e.Location))
            {
                Click.Play();
                if (stateimage == pauseimg)
                {
                    Road_timer.Stop();
                    stateimage = resumeimg;
                    this.Invalidate();
                }
                else
                {
                    Road_timer.Start();
                    stateimage = pauseimg;
                    this.Invalidate();
                }
            }
        }
    }
}