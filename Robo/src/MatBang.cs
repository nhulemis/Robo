using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Robo.src
{
    class MatBang
    {
        public int Rows { get; set; }
        public int Columns { get; set; }
        public MatBang()
        {

        }
        public MatBang(int rows, int columns)
        {
            this.Rows = rows;
            this.Columns = columns;
        }

        public void DrawMatrix(Graphics grs)
        {
            float x = 0.000001f;
            Pen pen = new Pen(new SolidBrush(Color.Black), x);

            for (int i = 0; i <= Columns; i++)
            {
                grs.DrawLine(pen, i * Cell.Width, 0, i * Cell.Width, Rows * Cell.Height);
            }
            for (int i = 0; i <= Rows; i++)
            {
                grs.DrawLine(pen, 0, i * Cell.Height, Columns * Cell.Width, i * Cell.Height);
            }
        }

        public void GiaTri(Point p, int giatri)
        {
            String text = "" + giatri;
            Font drawFornt = new Font("Arial", 14);
            SolidBrush drawSb = new SolidBrush(Color.Red);
            StringFormat stringFormat = new StringFormat();
            Common.Constants.grs.DrawString(text, drawFornt, drawSb, p);
        }

    }
}
