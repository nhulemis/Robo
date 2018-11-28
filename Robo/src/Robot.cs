using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Robo.src
{
    class Robot : Cell
    {
        public int ThoiGianNghi { get; set; }

        public Robot()
        {

        }
        public Robot(int thoiGianNghi, int X, int Y, Point p, bool isSoHuu) : base(X, Y, p, isSoHuu)
        {
            this.ThoiGianNghi = thoiGianNghi;
        }

        public void DrawRobo()
        {
            String path = Application.StartupPath + "\\Resources\\robo.png";
            Common.Constants.VeIcon(path, this.P);
        }

    }
}
