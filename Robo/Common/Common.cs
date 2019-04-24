using Robo.src;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Robo.Common
{
    class Common
    {
        private static Common s_common;
        private Graphics m_grs;
        private Color m_color;
        public Cell[,] matrix;

        public int COLUMNS;
        public int ROWS;

        public int Width;
        public int Height;


        private Common()
        {
          
        }

        public static Common GetInstance()
        {
            if (s_common == null)
            {
                s_common = new Common();
                return s_common;
            }
            return s_common;
        }

        public void InitMatrix()
        {
            matrix = new Cell[COLUMNS, ROWS];
        }

        public void DrawMatrix()
        {
            m_grs.Clear(m_color);
            float x = 0.000001f;
            Pen pen = new Pen(new SolidBrush(Color.Black), x);

            for (int i = 0; i <= COLUMNS; i++)
            {
                m_grs.DrawLine(pen, i * Width, 0, i * Width, ROWS * Height);
            }
            for (int i = 0; i <= ROWS; i++)
            {
                m_grs.DrawLine(pen, 0, i * Height, COLUMNS * Width, i * Height);
            }
        }

        public void SetGraphics(Graphics grs, Color color)
        {
            m_color = color;
            m_grs = grs;
        }

        public Graphics GetGraphics()
        {
            return m_grs;
        }

        public Color GetColor()
        {
            return m_color;
        }

        public void DeleteIcon(Point p)
        {
            m_grs.FillRectangle(new SolidBrush(m_color), p.X + 1, p.Y + 1, Width - 1, Height - 1);
            
        }

        public void DrawDirty(Point p)
        {
            String path = Application.StartupPath + "\\Resources\\dirty1.png";
            RenderIcon(path, p);
        }

        public void DrawObstacle(Point P)
        {
            m_grs.FillRectangle(new SolidBrush(Color.BlueViolet), P.X + 1, P.Y + 1, Width - 1, Height - 1);
        }

        public void GiaTri(Point p, int giatri)
        {
            String text = "" + giatri;
            Font drawFornt = new Font("Arial", 14);
            SolidBrush drawSb = new SolidBrush(Color.Red);
            StringFormat stringFormat = new StringFormat();
            m_grs.DrawString(text, drawFornt, drawSb, p);
        }

        public void RenderIcon(string path, Point point)
        {
            WebRequest req = WebRequest.Create(path);

            WebResponse res = req.GetResponse();

            Stream imgStream = res.GetResponseStream();

            Image img1 = Image.FromStream(imgStream);
            var p = point;
            m_grs.DrawImage(img1, p.X + 1, p.Y + 1, Width - 1, Height - 1);
        }

    }
}
