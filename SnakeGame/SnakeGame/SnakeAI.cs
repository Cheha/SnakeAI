using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SnakeGame.Classes;

namespace SnakeGame
{
    public class SnakeAI
    {
        #region Fields

        private Snake _snake;
        private Food _food;
        private Graphics _paper;

        #endregion

        public SnakeAI(Snake snake, Food food, Graphics paper)
        {
            _snake = snake;
            _food = food;
            _paper = paper;
        }

        // return values:
        // 1 - turn up
        // 2 - turn down
        // 3 - turn left
        // 4 - turn right
        private int FindPath()
        {
            if (_snake.X < _food.X)
                return 4;
            if (_snake.Y < _food.Y)
                return 2;
            if (_snake.X > _food.X)
                return 3;
            if (_snake.Y > _food.Y)
                return 1;
            return 0;
        }

        public void MoveAI()
        {
            var result = FindPath();
            switch (result)
            {
                case 1:
                    _snake.MoveUp();
                    break;
                case 2:
                    _snake.MoveDown();
                    break;
                case 3:
                    _snake.MoveLeft();
                    break;
                case 4:
                    _snake.MoveRight();
                    break;
                default:
                    break;
            }

        }
    }
}
