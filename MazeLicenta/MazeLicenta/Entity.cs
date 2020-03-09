using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeLicenta
{
    public abstract class Entity
    {
        public Color Color { get; set; }
        public int Health { get; set; }
        public int Damage { get; set; }
        public MyPoint Location { get; set; }
    }

    public class Enemy:Entity
    {
        public Enemy()
        {

        }

        public void Do(){ }
    }

    public class Player:Entity
    {

    }
}
