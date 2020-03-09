using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MazeLicenta
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public Engine engine;

        private void Form1_Load(object sender, EventArgs e)
        {
            engine = Engine.GetInstance();
            engine.Init(pictureBox1);
            engine.CreateMaze();
        }
    }
}
