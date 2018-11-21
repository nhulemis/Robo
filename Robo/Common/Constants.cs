using Robo.src;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Robo.Common
{
    class Constants
    {
        public static Color c;
        public static String VAT_CAN = "VAT_CAN";
        public static String DIRTY = "DIRTY";
        public static String ROBO = "ROBO";
        public static Graphics grs;

        public static void VeIcon(Graphics grs,string path, Point point)
        {
            WebRequest req = WebRequest.Create(path);

            WebResponse res = req.GetResponse();

            Stream imgStream = res.GetResponseStream();

            Image img1 = Image.FromStream(imgStream);
            var p = point;
            grs.DrawImage(img1, p.X + 1, p.Y + 1, Cell.Width - 1, Cell.Height - 1);
        }

        public static void xoaIcon(Graphics g,Point p)
        {
            g.FillRectangle(new SolidBrush(c), p.X + 1, p.Y + 1, Cell.Width - 1, Cell.Height - 1);
        }
    }
}
