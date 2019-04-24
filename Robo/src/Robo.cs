using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net;
using System.Windows.Forms;

namespace Robo.src
{
    class Robo
    {
        private int m_x;
        private int m_y;
        private Point m_position;
        private Image m_imgRobot;

        //---------------------------------------------------------------
        public void OnUpdate()
        {

        }

        public void Swap(int x,int y)
        {
            m_position.X = x * Cell.Width;
            m_position.Y = y * Cell.Height;
        }

        public void OnInit()
        {
            string path = Application.StartupPath + "\\Resources\\robo.png";
            WebRequest req = WebRequest.Create(path);
            WebResponse res = req.GetResponse();
            Stream imgStream = res.GetResponseStream();
            m_imgRobot = Image.FromStream(imgStream);
        }

        public void OnRender(Graphics grs)
        {
            grs.DrawImage(m_imgRobot, m_position.X + 1, m_position.Y + 1, Cell.Width - 1, Cell.Height - 1);
        }

        //-------------------------------------------------------
        public void SetX(int x)
        {
            m_x = x;
        }

        public void SetY(int y)
        {
            m_y = y;
        }

        public int GetX()
        {
            return m_x;
        }

        public int GetY()
        {
            return m_y;
        }

        public void SetPoint(Point p)
        {
            m_position = p;
        }

        public Point GetPoint()
        {
            return m_position;
        }
    }
}
