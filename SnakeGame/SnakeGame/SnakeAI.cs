using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using SnakeGame.Classes;

namespace SnakeGame
{
    public class SnakeAI
    {
        #region Fields

        private Snake _snake;
        private Food _food;

        #endregion

        public SnakeAI(Snake snake, Food food, Graphics paper)
        {
            _snake = snake;
            _food = food;
        }

        public static List<Point> FindPath(Point start, Point goal)
        {
            // Создается 2 списка вершин – ожидающие рассмотрения и уже рассмотренные. 
            // В ожидающие добавляется точка старта, список рассмотренных пока пуст.
            var closedSet = new Collection<PathNode>();
            var openSet = new Collection<PathNode>();
            // Для каждой точки рассчитывается F = G + H.
            //G – расстояние от старта до точки, H – примерное расстояние от точки до цели. 
            // Так же каждая точка хранит ссылку на точку, из которой в нее пришли.
            PathNode startNode = new PathNode()
            {
                Position = start,
                CameFrom = null,
                PathLengthFromStart = 0,
                HeuristicEstimatePathLength = GetHeuristicPathLength(start, goal)
            };
            openSet.Add(startNode);
            //BlackSnake(snake, ref closedSet);
            while (openSet.Count > 0)
            {
                // Из списка точек на рассмотрение выбирается точка с наименьшим F. Обозначим ее X.
                var currentNode = openSet.OrderBy(node => node.EstimateFullPathLength).First();
                // Если X – цель, то мы нашли маршрут.
                if (currentNode.Position == goal)
                    return GetPathForNode(currentNode);
                // Переносим X из списка ожидающих рассмотрения в список уже рассмотренных.
                openSet.Remove(currentNode);
                closedSet.Add(currentNode);
                // Для каждой из точек, соседних для X (обозначим эту соседнюю точку Y), делаем следующее:.
                foreach (var neighbourNode in currentNode.GetNeighbours(goal))
                {
                    // Если Y уже находится в рассмотренных – пропускаем ее.
                    if (closedSet.Count(node => node.Position == neighbourNode.Position) > 0)
                        continue;
                    var openNode = openSet.FirstOrDefault(node => node.Position == neighbourNode.Position);
                    // Если Y еще нет в списке на ожидание – добавляем ее туда, запомнив ссылку на X и рассчитав Y.G (это X.G + расстояние от X до Y) и Y.H.
                    if (openNode == null)
                        openSet.Add(neighbourNode);
                    else
                        if (openNode.PathLengthFromStart > neighbourNode.PathLengthFromStart)
                        {
                            // Если же Y в списке на рассмотрение – проверяем, если X.G + расстояние от X до Y < Y.G, 
                            // значит мы пришли в точку Y более коротким путем, заменяем Y.G на X.G + расстояние от X до Y, а точку, из которой пришли в Y на X.
                            openNode.CameFrom = currentNode;
                            openNode.PathLengthFromStart = neighbourNode.PathLengthFromStart;
                        }
                }
                

            }
            // Если список точек на рассмотрение пуст, а до цели мы так и не дошли – значит маршрут не существует.
            return null;
        }

        public static void EatFood(Snake snake, Food food, List<Point> path)
        {
            for (int i = 0; i < snake.SnakeRectangles.Length; i++)
            {
                if (snake.SnakeRectangles[i].IntersectsWith(food.foodRectangle))
                {
                    snake.GrowSnake();
                    food.FoodLocation(new Random());
                    path = FindPath(new Point(snake.SnakeRectangles[0].X, snake.SnakeRectangles[0].Y), new Point(food.X, food.Y));
                }
            }
        }

        //public static bool CheckNode(Snake snake, Point node)
        //{
        //    if (snake.SnakeRectangles[0].X == node.X && snake.SnakeRectangles[0].X == node.X)
        //    {
        //        MessageBox.Show("Авария");
        //        return true;
        //    }
        //    return false;
        //}

        

        // Функция примерной оценка растояния до цели
        // Для оценки расстояния я использую длину пути без препятствий.
        internal static int GetHeuristicPathLength(Point from, Point to)
        {
            return Math.Abs(from.X - to.X) + Math.Abs(from.Y - to.Y);
        }

        private static List<Point> GetPathForNode(PathNode pathNode)
        {
            //TO DO: Add logic for moving snake ?????
            var result = new List<Point>();
            var currentNode = pathNode;
            while (currentNode != null)
            {
                result.Add(currentNode.Position);
                currentNode = currentNode.CameFrom;
            }
            result.Reverse();
            return result;
        }

    }
}
