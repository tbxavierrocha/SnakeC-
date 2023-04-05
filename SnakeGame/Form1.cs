using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Schema;

namespace SnakeGame
{
    public partial class Form1 : Form
    {
        private List<circle> Snake = new List<circle>();
        private circle food = new circle();

        int maxWidth;
        int maxHeight;

        int score;
        int highscore;

        Random rand=new Random();

        bool goLeft, goRight, goDown, goUp;
        public Form1()
        {
            InitializeComponent();

            new settings();
        }

        private void keyIsDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left && settings.directions != "right")
            {
                goLeft= true;
            }
            if (e.KeyCode == Keys.Right && settings.directions != "left")
            {
                goRight = true;
            }
            if (e.KeyCode == Keys.Up && settings.directions != "down")
            {
                goUp= true;
            }
            if (e.KeyCode == Keys.Down && settings.directions != "up")
            {
                goDown= true;
            }
        }

        private void keyIsUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                goLeft = false;
            }
            if (e.KeyCode == Keys.Right)
            {
                goRight = false;
            }
            if (e.KeyCode == Keys.Up)
            {
                goUp = false;
            }
            if (e.KeyCode == Keys.Down)
            {
                goDown = false;
            }
        }

        private void startgame(object sender, EventArgs e)
        {
            RestartGame();
        }

        private void GameTimerEvent(object sender, EventArgs e)
        {
            //set directions
            if(goLeft)
            {
                settings.directions= "left";
            }
            if(goRight)
            {
                settings.directions= "right";
            }
            if(goDown)
            {
                settings.directions = "down";
            }
            if(goUp)
            {
                settings.directions = "up";
            }
            //end directions

            for (int i = Snake.Count - 1; i >= 0; i--)
            {
                if(i == 0)
                {
                    switch(settings.directions) 
                    { 
                        case "left":
                            Snake[i].x--;
                            break;
                        case "right":
                            Snake[i].x++;
                            break;
                        case "down":
                            Snake[i].y++;
                            break;
                        case "up":
                            Snake[i].y--;
                            break;
                    }

                    if(Snake[i].x < 0)
                    {
                        Snake[i].x = maxWidth;
                    }
                    if (Snake[i].x > maxWidth)
                    {
                        Snake[i].x = 0;
                    }
                    if (Snake[i].y < 0)
                    {
                        Snake[i].y = maxHeight;
                    }
                    if (Snake[i].y > maxHeight)
                    {
                        Snake[i].y = 0;
                    }

                    if (Snake[i].x ==food.x && Snake[i].y == food.y)
                    {
                        EatFood();
                    }

                    for (int j = 1; j < Snake.Count; j++)
                    {
                        if (Snake[i].x == Snake[j].x && Snake[i].y == Snake[j].y)
                        {
                            GameOver();
                        }
                    }

                }
                else
                {
                    Snake[i].x = Snake[i - 1].x;
                    Snake[i].y = Snake[i - 1].y;
                }
            }

            picCanvas.Invalidate();
        }

        private void UpdatePictureBox(object sender, PaintEventArgs e)
        {
            Graphics canvas= e.Graphics;

            Brush snakeColour;

            for (int i = 0; i < Snake.Count; i++)
            { 
                if(i ==0)
                {
                    snakeColour = Brushes.Black;
                }
                else
                {
                    snakeColour = Brushes.DarkGreen;
                }

                canvas.FillEllipse(snakeColour, new Rectangle
                    (
                    Snake[i].x * settings.Width,
                    Snake[i].y * settings.Height,
                    settings.Width, settings.Height
                    ));
            }

            canvas.FillEllipse(Brushes.DarkRed, new Rectangle
            (
            food.x * settings.Width,
            food.y * settings.Height,
            settings.Width, settings.Height
            ));
        }

        private void RestartGame()
        {
            maxWidth = picCanvas.Width / settings.Width - 1;
            maxHeight = picCanvas.Height / settings.Height - 1;

            Snake.Clear();

            btn_start.Enabled = false;
            score = 0;
            txtScore.Text = "Score: " + score;

            circle head = new circle { x = 10, y = 5 };
            Snake.Add(head);

            for (int i = 0; i < 10; i++) 
            {
                circle body = new circle();
                Snake.Add(body);
            }

            food = new circle { x = rand.Next(2, maxWidth), y = rand.Next(2, maxHeight) };

            gameTimer.Start();
        }

        private void EatFood()
        {
            score += 1;

            txtScore.Text = "Score: " + score;

            circle body = new circle
            {
                x = Snake[Snake.Count - 1].x,
                y = Snake[Snake.Count - 1].y
            };

            Snake.Add(body);

            food = new circle { x = rand.Next(2, maxWidth), y = rand.Next(2, maxHeight) };

        }

        private void GameOver()
        {
            gameTimer.Stop();
            btn_start.Enabled = true;

            if (score > highscore)
            {
                highscore= score;

                txthighscore.Text = "High Score: " + Environment.NewLine + highscore;
                txthighscore.ForeColor = Color.Maroon;
                txthighscore.TextAlign = ContentAlignment.MiddleCenter;
            }

        }
    }
}
