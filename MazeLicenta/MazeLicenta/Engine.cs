using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MazeLicenta
{
    public class Engine
    {
        public static Random random = new Random();


        #region MazeStats
        public Maze maze;
        public MazeGenerator mazeGenerator;
        public int MazeWidth = 70;
        public int MazeHeight = 70;
        public int MaxWaypoints = 5;
        public int MinWaypoints = 1;
        #endregion


        #region DrawStats
        public DrawEngine drawEninge;
        #endregion


        #region Singleton
        public static Engine Instance { get; set; }

        private Engine() { }

        public static Engine GetInstance()
        {
            if (Instance == null)
            {
                Instance = new Engine();
            }
            return Instance;
        }
        #endregion


        public void Init(PictureBox pictureBox)
        {
            mazeGenerator = MazeGenerator.GetInstance();
            drawEninge = DrawEngine.GetInstace();
            drawEninge.Init(pictureBox);
        }

        public void CreateMaze()
        {
            maze = mazeGenerator.GenerateMaze(MazeHeight, MazeWidth, MinWaypoints, MaxWaypoints);
            drawEninge.DrawMaze(maze);
        }
    }
}
