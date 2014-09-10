using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.IO;
using System.Linq;
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
                // 1 - turn up
                case 1:
                    _snake.MoveUp();
                    break;
                // 2 - turn down
                case 2:
                    _snake.MoveDown();
                    break;
                // 3 - turn left
                case 3:
                    _snake.MoveLeft();
                    break;
                // 4 - turn right
                case 4:
                    _snake.MoveRight();
                    break;
                default:
                    break;
            }
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
                foreach (var neighbourNode in GetNeighbours(currentNode, goal))
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

        private static int GetDistanceBetweenNeighbours()
        {
            return 1;
        }

        private static Collection<PathNode> GetNeighbours(PathNode pathNode, Point goal)
        {
            var result = new Collection<PathNode>();

            // Соседними точками являются соседние по стороне клетки.
            Point[] neighbourPoints = new Point[4];
            neighbourPoints[0] = new Point(pathNode.Position.X + 10, pathNode.Position.Y);
            neighbourPoints[1] = new Point(pathNode.Position.X - 10, pathNode.Position.Y);
            neighbourPoints[2] = new Point(pathNode.Position.X, pathNode.Position.Y + 10);
            neighbourPoints[3] = new Point(pathNode.Position.X, pathNode.Position.Y - 10);

            foreach (var point in neighbourPoints)
            {
                //------------- Переделать ---------------------------------
                // Проверяем, что не вышли за границы карты.
                //if (point.X < 0 || point.X >= field.GetLength(0))
                //    continue;
                //if (point.Y < 0 || point.Y >= field.GetLength(1))
                //    continue;
                //// Проверяем, что по клетке можно ходить.
                //if ((field[point.X, point.Y] != 0) && (field[point.X, point.Y] != 1))
                //    continue;
                //----------------------------------------------------------
                // Заполняем данные для точки маршрута.
                var neighbourNode = new PathNode()
                {
                    Position = point,
                    CameFrom = pathNode,
                    PathLengthFromStart = pathNode.PathLengthFromStart +
                      GetDistanceBetweenNeighbours(),
                    HeuristicEstimatePathLength = GetHeuristicPathLength(point, goal)
                };
                result.Add(neighbourNode);
            }
            return result;
        }

        // Функция примерной оценка растояния до цели
        // Для оценки расстояния я использую длину пути без препятствий.
        private static int GetHeuristicPathLength(Point from, Point to)
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
