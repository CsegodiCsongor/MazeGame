using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MazeLicenta
{
    public class DrawEngine
    {
        public Bitmap bitmap;
        public Graphics graphics;
        public PictureBox canvas;
        public Pen MyPen;

        public int TileSize=10;
        public MyPoint topLeft;


        #region Singleton
        public static DrawEngine Instance;

        private DrawEngine() { }

        public static DrawEngine GetInstace()
        {
            if (Instance == null)
            {
                Instance = new DrawEngine();
            }
            return Instance;
        }
        #endregion


        public void Init(PictureBox pictureBox)
        {
            canvas = pictureBox;
            bitmap = new Bitmap(canvas.Width, canvas.Height);
            graphics = Graphics.FromImage(bitmap);
            topLeft = new MyPoint(0, 0);

            MyPen = new Pen(Color.Black, 3);
        }

        public void DrawMaze(Maze maze)
        {
            for (int i = topLeft.Y / TileSize; i < bitmap.Width / TileSize + 1; i++)
            {
                for (int j = topLeft.X / TileSize; j < bitmap.Height / TileSize + 1; j++)
                {
                    if (j < maze.maze.GetLength(0) && i < maze.maze.GetLength(1))
                    {
                        graphics.DrawRectangle(MyPen, i * TileSize - topLeft.Y, j * TileSize - topLeft.X, TileSize, TileSize);
                        if (!maze.maze[j, i].Walkable)
                        {
                            graphics.FillRectangle(new SolidBrush(Color.Black), i * TileSize - topLeft.Y, j * TileSize - topLeft.X, TileSize, TileSize);
                        }
                        if (maze.StartingPoint.X == i && maze.StartingPoint.Y == j)
                        {
                            graphics.FillRectangle(new SolidBrush(Color.Red), i * TileSize - topLeft.Y, j * TileSize - topLeft.X, TileSize, TileSize);
                        }
                        else if (maze.EndingPoint.X == i && maze.EndingPoint.Y == j)
                        {
                            graphics.FillRectangle(new SolidBrush(Color.Green), i * TileSize - topLeft.Y, j * TileSize - topLeft.X, TileSize, TileSize);
                        }
                    }
                }
            }

            canvas.Image = bitmap;
        }
    }
}
