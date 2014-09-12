using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SnakeGame.Classes
{
    public class Snake
    {
        private Rectangle[] snakeRectangles;
        private SolidBrush brush;
        private int x, y, width, height;

        public Rectangle[] SnakeRectangles { get { return snakeRectangles; } }

        public int X { get { return SnakeRectangles[0].X; } }
        public int Y { get { return SnakeRectangles[0].Y; } }

        public Snake()
        {
            snakeRectangles = new Rectangle[3];
            brush = new SolidBrush(Color.Black);

            x = 20;
            y = 0;
            width = height = 10;

            for (int i = 0; i < snakeRectangles.Length; i++)
            {
                snakeRectangles[i] = new Rectangle(x , y, width, height);
                x -= 10;
            }
        }

        public void DrawSnake(Graphics paper)
        {
            foreach (Rectangle rectangle in snakeRectangles)
            {
                paper.FillRectangle(brush, rectangle);
            }
        }

        public void DrawSnake()
        {
            for (int i = snakeRectangles.Length - 1; i > 0; i--)
            {
                snakeRectangles[i] = snakeRectangles[i - 1];
            }
        }

        public void GrowSnake()
        {
            var rec = snakeRectangles.ToList();
            rec.Add(new Rectangle(snakeRectangles[snakeRectangles.Length - 1].X, snakeRectangles[snakeRectangles.Length - 1].Y, width, height));
            snakeRectangles = rec.ToArray();
        }

        public void MoveSnake(List<Point> path)
        {
            this.DrawSnake();
            var node = path.First();
            this.SnakeRectangles[0].Y = node.Y;
            this.SnakeRectangles[0].X = node.X;
            //for (int i = 1; i < this.SnakeRectangles.Length; i++)
            //{
            //    if (this.SnakeRectangles[0].IntersectsWith(this.SnakeRectangles[i]))
                    
            //}
            path.Remove(node);
        }
    }
}
