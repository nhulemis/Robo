using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Robo.src
{
    class Obstacle : Cell
    {
       
        public Obstacle() { }

        public Obstacle(int X, int Y, Point p, bool isSoHuu, int cost) : base(X, Y, p, isSoHuu, cost)
        {
        }

        

        public void DrawObstacle(Graphics g)
        {
            g.FillRectangle(new SolidBrush(Color.BlueViolet), this.P.X + 1, this.P.Y + 1, Cell.Width - 1, Cell.Height - 1);
        }
    }
}
