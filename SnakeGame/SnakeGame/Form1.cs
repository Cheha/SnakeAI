using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SnakeGame.Classes;

namespace SnakeGame
{
    public partial class Form1 : Form
    {
        private Graphics paper;
        Snake snake = new Snake();
        private Food food;
        private bool left = false;
        private bool right = false;
        private bool up = false;
        private bool down = false;
        int score = 0;
        private List<Point> path;

        private SnakeAI ai;

        public Form1()
        {
            InitializeComponent();
            food = new Food(new Random());
            ai = new SnakeAI(snake, food, paper);
            path = SnakeAI.FindPath(new Point(snake.SnakeRectangles[0].X, snake.SnakeRectangles[0].Y), new Point(food.X, food.Y));

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Width = 450;
            this.Height = 450;
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            paper = e.Graphics;
            food.DrawFood(paper);
            snake.DrawSnake(paper);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {      
            snake.MoveSnake(path);
            
            this.Invalidate();
            
            for (int i = 0; i < snake.SnakeRectangles.Length; i++)
            {
                if (snake.SnakeRectangles[i].IntersectsWith(food.foodRectangle))
                {
                    snake.GrowSnake();
                    food.FoodLocation(new Random());
                    path = SnakeAI.FindPath(new Point(snake.SnakeRectangles[0].X, snake.SnakeRectangles[0].Y), new Point(food.X, food.Y));
                }
            }
            
            
        }

        

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Space)
            {
                if (timer1.Enabled)
                    timer1.Enabled = false;
                else
                    timer1.Enabled = true;
            }
        }

        public void SnakeCollision()
        {
            for (int i = 1; i < snake.SnakeRectangles.Length; i++)
            {
                if (snake.SnakeRectangles[0].IntersectsWith(snake.SnakeRectangles[i]))
                    restart();
            }
        }


        //Game Logic
        public void Collision()
        {
            // Rectangle.IntersectsWith - метод
            // Определяет, пересекается ли данный прямоугольник с прямоугольником rect.
            for (int i = 1; i < snake.SnakeRectangles.Length; i++)
            {
                if (snake.SnakeRectangles[0].IntersectsWith(snake.SnakeRectangles[1])) 
                    restart();
                if (snake.SnakeRectangles[0].X < 0 || snake.SnakeRectangles[0].X > 290)
                    restart();
                if (snake.SnakeRectangles[0].Y < 0 || snake.SnakeRectangles[0].Y > 290)
                    restart();
            }
        }



        public void restart()
        {
            timer1.Enabled = false;
            MessageBox.Show("GAME OVER");
            //snakeScoreLabel.Text = "0";
            //score = 0;
            //spaceBarLabel.Text = "Press SPACECAR to begin";
            //codesmeshlabel.Text = "CODESMESH";
            snake = new Snake();
        }

    }
}
