using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeLicenta
{
    public class Utils
    {
        #region Singleton
        private static Utils Instace;

        private Utils() { }

        public static Utils GetInstance()
        {
            if(Instace == null)
            {
                Instace = new Utils();
            }
            return Instace;
        }
        #endregion

        public void CreatePath(MyPoint startingPoint, MyPoint endingPoint, Tile[,] maze)
        {
            int[,] waveMat = CreateWaveMat(startingPoint, endingPoint, maze);
            List<MyPoint> path = GetPath(waveMat, endingPoint);

            maze[startingPoint.Y, startingPoint.X].Walkable = true;
            maze[endingPoint.Y, endingPoint.X].Walkable = true;
            for(int i = 0;i<path.Count;i++)
            {
                maze[path[i].Y, path[i].X].Walkable = true;
            }
        }

        #region BackTrackPath
        private List<MyPoint> GetPath(int[,] waveMat, MyPoint endingPoint)
        {
            List<MyPoint> path = new List<MyPoint>();

            MyPoint currentPoint = new MyPoint(endingPoint);

            while(waveMat[currentPoint.Y, currentPoint.X] != 1)
            {
                List<MyPoint> possiblePoints = GetPointsAround(currentPoint, waveMat);
                currentPoint = possiblePoints[Engine.random.Next(possiblePoints.Count)];
                path.Add(currentPoint);
            }

            return path;
        }

        private List<MyPoint> GetPointsAround(MyPoint point, int[,] waveMat)
        {
            List<MyPoint> points = new List<MyPoint>();
            int valueToLookFor = waveMat[point.Y, point.X] - 1;

            if (Ok(point, -1, 0, valueToLookFor, waveMat))
            {
                points.Add(new MyPoint(point.X - 1, point.Y));
            }
            if (Ok(point, 1, 0, valueToLookFor, waveMat))
            {
                points.Add(new MyPoint(point.X + 1, point.Y));
            }
            if (Ok(point, 0, 1, valueToLookFor, waveMat))
            {
                points.Add(new MyPoint(point.X, point.Y + 1));
            }
            if (Ok(point, 0, -1, valueToLookFor, waveMat))
            {
                points.Add(new MyPoint(point.X, point.Y - 1));
            }

            return points;
        }

        private bool Ok(MyPoint point, int xOffset, int yOffset, int valueToLookFor, int[,] waveMat)
        {
            if (CanGo(point, xOffset, yOffset, waveMat) && ShouldGo(point, xOffset, yOffset, valueToLookFor, waveMat))
            {
                return true;
            }
            return false;
        }

        private bool ShouldGo(MyPoint point, int xOffset, int yOffset, int valueToLookFor, int[,] waveMat)
        {
            if(waveMat[point.Y + yOffset, point.X + xOffset] == valueToLookFor)
            {
                return true;
            }
            return false;
        }
        #endregion

        #region WaveRegion
        private int[,] CreateWaveMat(MyPoint startingPoint, MyPoint endingPoint, Tile[,] maze)
        {
            List<MyPoint> points = new List<MyPoint>();
            points.Add(new MyPoint(startingPoint));

            MyPoint currentPoint;
            bool found = false;
            int[,] waveMat = new int[maze.GetLength(0), maze.GetLength(1)];

            for (int i = 0; i < waveMat.GetLength(0); i++)
            {
                for (int j = 0; j < waveMat.GetLength(1); j++)
                {
                    waveMat[i, j] = -1;
                }
            }

            waveMat[startingPoint.Y, startingPoint.X] = 0;

            while (points.Count != 0 && !found)
            {
                currentPoint = points[0];
                points.RemoveAt(0);

                List<MyPoint> possiblePoints = GetPointsAround(currentPoint, false, maze, waveMat);
                if (possiblePoints.Count == 0)
                {
                    possiblePoints = GetPointsAround(currentPoint, true, maze, waveMat);
                }

                for (int i = 0; i < possiblePoints.Count; i++)
                {
                    waveMat[possiblePoints[i].Y, possiblePoints[i].X] = waveMat[currentPoint.Y, currentPoint.X] + 1;
                    if (possiblePoints[i] == endingPoint)
                    {
                        found = true;
                    }
                    points.Add(new MyPoint(possiblePoints[i]));
                }
            }

            return waveMat;
        }

        private List<MyPoint> GetPointsAround(MyPoint point, bool walkable, Tile[,] maze, int[,] waveMat)
        {
            List<MyPoint> points = new List<MyPoint>();

            if (Ok(point, walkable, -1, 0, maze, waveMat))
            {
                points.Add(new MyPoint(point.X - 1, point.Y));
            }
            if (Ok(point, walkable, 1, 0, maze, waveMat))
            {
                points.Add(new MyPoint(point.X + 1, point.Y));
            }
            if (Ok(point, walkable, 0, -1, maze, waveMat))
            {
                points.Add(new MyPoint(point.X, point.Y - 1));
            }
            if (Ok(point, walkable, 0, 1, maze, waveMat))
            {
                points.Add(new MyPoint(point.X, point.Y + 1));
            }

            return points;
        }

        private bool Ok(MyPoint point, bool walkable, int xOffset, int yOffset, Tile[,] maze, int[,] waveMat)
        {
            if (CanGo(point, xOffset, yOffset, waveMat) && ShouldGo(point, walkable, xOffset, yOffset, maze, waveMat))
            {
                return true;
            }
            return false;
        }

        private bool ShouldGo(MyPoint point, bool walkable, int xOffset, int yOffset, Tile[,] maze, int[,] waveMat)
        {
            if (maze[point.Y + yOffset, point.X + xOffset].Walkable == walkable &&
                waveMat[point.Y + yOffset, point.X + xOffset] == -1)
            {
                return true;
            }
            return false;
        }

        private bool CanGo(MyPoint point, int xOffset, int yOffset, int[,] maze)
        {
            if (point.X + xOffset >= 0 && point.X + xOffset < maze.GetLength(1) &&
               point.Y + yOffset >= 0 && point.Y + yOffset < maze.GetLength(0))
            {
                return true;
            }
            return false;
        }
        #endregion
    }
}
