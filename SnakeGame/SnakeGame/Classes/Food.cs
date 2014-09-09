using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame.Classes
{
    public class Food
    {
        private int x, y, width, height;
        private SolidBrush brush;

        public Rectangle foodRectangle;

        public int X { get { return x; } set { x = value; } }
        public int Y { get { return y; } set { y = value; } }

        public Food(Random random)
        {
            x = random.Next(0, 29)*10;
            y = random.Next(0, 29)*10;
            brush = new SolidBrush(Color.Black);
            width = height = 10;
            foodRectangle = new Rectangle(x , y, width, height );
        }

        public void FoodLocation(Random random)
        {
            x = random.Next(0, 29) * 10;
            y = random.Next(0, 29) * 10;
        }

        public void DrawFood(Graphics paper)
        {
            foodRectangle.X = x;
            foodRectangle.Y = y;
            paper.FillRectangle(brush, foodRectangle);
        }
    }
}
