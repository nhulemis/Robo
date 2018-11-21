using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Robo.src
{
    class Mark :Cell
    {
        public Mark()
        {
        }

        public Mark(int X, int Y, Point p, bool isSoHuu, int cost) : base(X, Y, p, isSoHuu, cost)
        {
        }

        // public Mark(int X, int Y, Point p, bool isSoHuu, int cost) : base(X, Y, p, isSoHuu, cost) { }

        public void DrawMark(Graphics g)
        {
            String path = Application.StartupPath + "\\Resources\\empty-flag.png";
            Common.Constants.VeIcon(g, path, this.P);
        }
    }
}
