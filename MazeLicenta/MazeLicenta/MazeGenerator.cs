using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeLicenta
{
    public class MyPoint
    {
        public int X { get; set; }
        public int Y { get; set; }


        public MyPoint(int x, int y)
        {
            X = x;
            Y = y;
        }

        public MyPoint()
        {
            X = 0;
            Y = 0;
        }

        public MyPoint(MyPoint point)
        {
            X = point.X;
            Y = point.Y;
        }

        public static bool operator ==(MyPoint a, MyPoint b)
        {
            if (a.X == b.X && a.Y == b.Y)
            {
                return true;
            }
            return false;
        }

        public static bool operator !=(MyPoint a, MyPoint b)
        {
            if (!(a == b))
            {
                return true;
            }
            return false;
        }
    }


    public class MazeGenerator
    {
        #region Singleton
        private static MazeGenerator Instance { get; set; }

        private MazeGenerator() { }

        public static MazeGenerator GetInstance()
        {
            if (Instance == null)
            {
                Instance = new MazeGenerator();
            }
            return Instance;
        }
        #endregion

        Utils utils;

        public Maze GenerateMaze(int height, int width, int minWaypoints, int maxWaypoints)
        {
            utils = Utils.GetInstance();
            Tile[,] maze = new Tile[height, width];

            for (int i = 0; i < maze.GetLength(0); i++)
            {
                for (int j = 0; j < maze.GetLength(1); j++)
                {
                    maze[i, j] = new Tile();
                }
            }

            MyPoint startingPoint = GetRandomWall(maze);
            MyPoint endingPonint = GetRandomWall(maze);
            Maze mazeToReturn = new Maze();
            mazeToReturn.StartingPoint = new MyPoint(startingPoint);
            mazeToReturn.EndingPoint = new MyPoint(endingPonint);

            maze[startingPoint.Y, startingPoint.X].Walkable = true;

            endingPonint = GetRandomWall(maze);
            utils.CreatePath(startingPoint, endingPonint, maze);

            for (int i = 1; i < Engine.random.Next(minWaypoints, maxWaypoints); i++)
            {
                startingPoint = new MyPoint(endingPonint);
                endingPonint = GetRandomWall(maze);
                utils.CreatePath(startingPoint, endingPonint, maze);
            }

            utils.CreatePath(endingPonint, mazeToReturn.EndingPoint, maze);

            for (int i = 0; i < 3; i++)
            {
                MyPoint destPoint = GetRandomWall(maze);
                maze[destPoint.Y, destPoint.X].Walkable = true;

                startingPoint = GetRandomPath(maze);
                endingPonint = GetRandomWall(maze);
                utils.CreatePath(startingPoint, endingPonint, maze);
                for (int j = 1; j < Engine.random.Next(minWaypoints, maxWaypoints); j++)
                {
                    startingPoint = new MyPoint(endingPonint);
                    endingPonint = GetRandomWall(maze);
                    utils.CreatePath(startingPoint, endingPonint, maze);
                }
                utils.CreatePath(endingPonint, destPoint, maze);

            }

            mazeToReturn.maze = maze;

            return mazeToReturn;
        }

        private MyPoint GetRandomWall(Tile[,] maze)
        {
            MyPoint randomPoint = new MyPoint(Engine.random.Next(maze.GetLength(1)), Engine.random.Next(maze.GetLength(0)));

            while (maze[randomPoint.Y, randomPoint.X].Walkable)
            {
                randomPoint = new MyPoint(Engine.random.Next(maze.GetLength(1)), Engine.random.Next(maze.GetLength(0)));
            }

            return randomPoint;
        }

        private MyPoint GetRandomPath(Tile[,]maze)
        {
            MyPoint randomPoint = new MyPoint(Engine.random.Next(maze.GetLength(1)), Engine.random.Next(maze.GetLength(0)));

            while (!maze[randomPoint.Y, randomPoint.X].Walkable)
            {
                randomPoint = new MyPoint(Engine.random.Next(maze.GetLength(1)), Engine.random.Next(maze.GetLength(0)));
            }

            return randomPoint;
        }
    }
}
