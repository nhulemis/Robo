using Robo.Common;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net;
using System.Threading;
using System.Windows.Forms;

namespace Robo.src

{
    class RoboCleanHouse
    {
        private static int m_x;
        private static int m_y;
        private static Point m_position;
        private static Image m_imgRobot;
        private static Thread m_thread;
        private static Stack<Cell> m_pathPlanning;
        private static Queue<Cell> m_Open;
        private static Queue<Cell> m_dirtys;

        private static Cell[,] m_map;
        private static bool m_isModified;
        private static bool m_isBusy;
        private static Common.Common m_com;
        private static int m_timesSwap;
        private static int m_totalTimes;
        private static int[] Hx = { 1, 0, -1, 0 };
        private static int[] Hy = { 0, 1, 0, -1 };
        private static bool m_isStop;


        

        //--------------------------------------------------------------
        public RoboCleanHouse()
        {
            m_thread = new Thread(Operate);
        }

        public RoboCleanHouse(int x, int y)
        {
            m_thread = new Thread(Operate);
            m_com = Common.Common.GetInstance();
            m_x = x;
            m_y = y;
            m_position.X = m_x * m_com.Width;
            m_position.Y = m_y * m_com.Height;
            m_isModified = false;
            string path = Application.StartupPath + "\\Resources\\robo.png";
            WebRequest req = WebRequest.Create(path);
            WebResponse res = req.GetResponse();
            Stream imgStream = res.GetResponseStream();
            m_imgRobot = Image.FromStream(imgStream);
            m_timesSwap = 5;
            m_isBusy = false;
            m_isStop = false;
            OnInit();
        }

        //---------------------------------------------------------------
        private static void OnUpdate()
        {
            m_totalTimes++;
            if (!m_isBusy)
            {
                m_isBusy = true;
                MappingMap();
                Spill();
                SchedulePathPlanning();
            }
            else
            {
                if (m_totalTimes % m_timesSwap == 0)
                {
                    Swap();
                }
            }
        }

        private static void SchedulePathPlanning()
        {
            var tem = m_dirtys.Peek();
            while (true)
            {
                if (tem.Cost == 0)
                {
                    break;
                }
                for (int i = 0; i < 4; i++)
                {
                    var X = tem.X + Hx[i];
                    var Y = tem.Y + Hy[i];
                    if (Check(1,X, Y))
                    {
                        if (m_map[X, Y].Cost < tem.Cost)
                        {
                            m_pathPlanning.Push(tem);
                            tem = m_map[X, Y];
                        }
                    }
                }
            }
           
        }

        private static void MappingMap()
        {
            var matrix = m_com.matrix;
            foreach (var item in matrix)
            {
                if (item.Loai == Constants.ROBO || item.Loai == Constants.VAT_CAN ||item.Loai == Constants.DIRTY )
                {
                    m_map[item.X, item.Y].IsSoHuu = item.IsSoHuu;
                    m_map[item.X, item.Y].Loai = item.Loai;
                   // m_map[item.X, item.Y].Cost = item.Cost;
                }
            }

            m_com.matrix[m_x, m_y].Loai = Constants.ROBO;
            m_com.matrix[m_x, m_y].IsSoHuu = true;
        }

        private static void Spill()
        {
            while (m_Open.Count != 0)
            {
                var _fist = m_Open.Peek();
                for (int ii = 0; ii < 4; ii++)
                {
                    var X = _fist.X + Hx[ii];
                    var Y = _fist.Y + Hy[ii];

                    var values = _fist.Cost;
                    if (Check(0,X, Y))
                    {
                        if (m_map[X, Y].Loai == Constants.DIRTY)
                        {
                            m_dirtys.Enqueue(m_map[X, Y]);
                        }
                        var po = m_map[X, Y].P;
                        m_map[X, Y].Cost = values + 1;
                        var e = new Cell(X, Y, po, false, values + 1);
                        m_Open.Enqueue(e);
                         m_com.GiaTri(e.P, values + 1);

                    }
                }
                m_Open.Dequeue();
            }
            m_Open.Enqueue(m_map[m_x, m_y]);
        }

        private static bool Check(int type, int x, int y)
        {
            if (x < 0 || x > m_com.COLUMNS - 1 || y < 0 || y > m_com.ROWS - 1)
            {
                return false;
            }
            if (m_map[x, y].Cost != -1 && type == 0)
            {
                return false;
            }
            if (m_map[x, y].Cost == -100 || m_map[x, y].Loai == Constants.VAT_CAN)
            {
                return false;
            }
            if (m_map[x,y].IsSoHuu)
            {
                return false;
            }
            return true;
        }

        public static void Swap()
        {
            var dirty = m_dirtys.Peek();
            if (dirty.X == m_x && dirty.Y == m_y)
            {
                m_dirtys.Dequeue();
                m_isStop = true;
                return;
            }
            var next = m_pathPlanning.Pop();
            m_com.DeleteIcon(m_position);
            m_position = next.P;
            m_x = next.X;
            m_y = next.Y;
            m_isModified = true;
        }

        private void SetupMatrix()
        {
            for (int i = 0; i < m_com.COLUMNS; i++)
            {
                for (int j = 0; j < m_com.ROWS; j++)
                {
                    m_map[i, j] = new Cell(i, j, new Point(i * m_com.Width, j * m_com.Height), false, -1);
                }
            }
        }

        public void OnInit()
        {

            m_map = new Cell[m_com.COLUMNS, m_com.ROWS];
            m_pathPlanning = new Stack<Cell>();
            m_Open = new Queue<Cell>();
            m_dirtys = new Queue<Cell>();
            SetupMatrix();
            m_map[m_x, m_y].Cost = 0;
            m_Open.Enqueue(m_map[m_x, m_y]);

            m_com.DeleteIcon(m_position);
            m_com.GetGraphics().DrawImage(m_imgRobot, m_position.X + 1, m_position.Y + 1, m_com.Width - 1, m_com.Height - 1);

            m_thread.Start();

        }

        public static void OnRender(Graphics grs)
        {
            if (m_isModified)
            {
                grs.DrawImage(m_imgRobot, m_position.X + 1, m_position.Y + 1, m_com.Width - 1, m_com.Height - 1);
                m_isModified = false;
            }

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

        private static void Operate()
        {
            while (!m_isStop)
            {
                OnUpdate();
                OnRender(m_com.GetGraphics());
                Thread.Sleep(100);
            }
        }

    }
}
