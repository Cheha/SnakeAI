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
        private List<Point> gate;

        private SnakeAI ai;

        public Form1()
        {
            InitializeComponent();
            food = new Food(new Random());
            ai = new SnakeAI(snake, food, paper);
            gate = SnakeAI.FindPath(new Point(snake.SnakeRectangles[0].X, snake.SnakeRectangles[0].Y), new Point(food.X, food.Y));

        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            var form = new Form1();
            paper = e.Graphics;
            food.DrawFood(paper);
            snake.DrawSnake(paper);


        }

        private void timer1_Tick(object sender, EventArgs e)
        {      
            //if(down)
            //    snake.MoveDown();
            //if(up)
            //    snake.MoveUp();
            //if(left)
            //    snake.MoveLeft();
            //if(right)
            //    snake.MoveRight();
            MoveSnake(gate);

            
            this.Invalidate();
           // Collision();
            

            for (int i = 0; i < snake.SnakeRectangles.Length; i++)
            {
                if (snake.SnakeRectangles[i].IntersectsWith(food.foodRectangle))
                {
                    snake.GrowSnake();
                    food.FoodLocation(new Random());
                    gate = SnakeAI.FindPath(new Point(snake.SnakeRectangles[0].X, snake.SnakeRectangles[0].Y), new Point(food.X, food.Y));
                }
            }
            //SnakeCollision();
        }

        private void MoveSnake(List<Point> path)
        {
            var node = path.First();
            snake.DrawSnake();
            snake.SnakeRectangles[0].Y = node.Y;
            snake.SnakeRectangles[0].X = node.X;
            path.Remove(node);
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Space)
            {
                timer1.Enabled = true;
                down = false;
                up = false;
                left = false;
                right = false;
            }
            if (e.KeyData == Keys.Down && up == false)
            {
                down = true;
                up = false;
                right = false;
                left = false;
            }
            if (e.KeyData == Keys.Up && down == false)
            {
                down = false;
                up = true;
                right = false;
                left = false;
            }
            if (e.KeyData == Keys.Left && right == false)
            {
                down = false;
                up = false;
                right = false;
                left = true;
            }
            if (e.KeyData == Keys.Right && left == false)
            {
                down = false;
                up = false;
                right = true;
                left = false;
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

        // Checks if snake intersects with itself
        public void SnakeCollision()
        {
            for (int i = 1; i < snake.SnakeRectangles.Length - 1; i++)
            {
                if (snake.SnakeRectangles[0].IntersectsWith(snake.SnakeRectangles[i])) 
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
