using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame.Classes
{
    class PathNode
    {
        // Координаты точки на карте.
        public Point Position { get; set; }
        // Длина пути от старта (G).
        public int PathLengthFromStart { get; set; }
        // Точка, из которой пришли в эту точку.
        public PathNode CameFrom { get; set; }
        // Примерное расстояние до цели (H).
        public int HeuristicEstimatePathLength { get; set; }
        // Ожидаемое полное расстояние до цели (F).
        public int EstimateFullPathLength
        {
            get
            {
                return this.PathLengthFromStart + this.HeuristicEstimatePathLength;
            }
        }

        public Collection<PathNode> GetNeighbours(Point goal)
        {
            var result = new Collection<PathNode>();

            // Соседними точками являются соседние по стороне клетки.
            Point[] neighbourPoints = new Point[4];
            neighbourPoints[0] = new Point(this.Position.X + 10, this.Position.Y);
            neighbourPoints[1] = new Point(this.Position.X - 10, this.Position.Y);
            neighbourPoints[2] = new Point(this.Position.X, this.Position.Y + 10);
            neighbourPoints[3] = new Point(this.Position.X, this.Position.Y - 10);

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
                    CameFrom = this,
                    PathLengthFromStart = this.PathLengthFromStart +
                      GetDistanceBetweenNeighbours(),
                    HeuristicEstimatePathLength = SnakeAI.GetHeuristicPathLength(point, goal)
                };
                result.Add(neighbourNode);
            }

            return result;
        }

        private static int GetDistanceBetweenNeighbours()
        {
            return 1;
        }
    }
}
