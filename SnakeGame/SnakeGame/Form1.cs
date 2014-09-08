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

        public Form1()
        {
            InitializeComponent();
            food = new Food(new Random());
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if(down)
                snake.MoveDown();
            if(up)
                snake.MoveUp();
            if(left)
                snake.MoveLeft();
            if(right)
                snake.MoveRight();

            this.Invalidate();
            Collision();

            for (int i = 0; i < snake.SnakeRectangles.Length; i++)
            {
                if (snake.SnakeRectangles[i].IntersectsWith(food.foodRectangle))
                {
                    snake.GrowSnake();
                    food.FoodLocation(new Random());
                }
            }

        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Space)
            {
                timer1.Enabled = true;
                down = false;
                up = false;
                left = false;
                right = true;
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
