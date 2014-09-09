using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SnakeGame;
using SnakeGame.Classes;
using System.Drawing;

namespace SnakeAU
{
    public class AI
    {
        #region Fields

        private Snake _snake;
        private Food _snake_food;
        private Graphics _paper;

        #endregion

        #region ctor

        public AI(Snake snake, Food food, Graphics paper)
        {
            _snake = snake;
            _snake_food = food;
            _paper = paper;
        }
        #endregion

        public void FindPath()
        {
            _snake.MoveDown();
        }

    }
}
