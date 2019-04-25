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
        private int m_x;
        private int m_Witdh;
        private int m_Height;
        private int m_y;
        private Point m_position;
        private Image m_imgRobot;
        //private   Thread m_thread;
        private Stack<Cell> m_pathPlanning;
        private Queue<Cell> m_Open;
        private Queue<Cell> m_dirtys;
        public int Tag { get; set; }
        private Cell[,] m_map;
        private bool m_isModified;
        private bool m_isBusy;
        private Common.Common m_com;
        private int m_timesSwap;
        private int m_totalTimes;
        private int[] Hx = { 1, 0, -1, 0 };
        private int[] Hy = { 0, 1, 0, -1 };
        private bool m_isStop;

        private Thread m_thread;

        private Color m_color;
        private Graphics m_grs;
        private Brush m_soliColor;




        //--------------------------------------------------------------
        public RoboCleanHouse()
        {
            // m_thread = new Thread(Operate);
        }

        public RoboCleanHouse(int x, int y, Graphics grs, Color color)
        {
            // m_thread = new Thread(Operate);
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
            m_timesSwap = 3;
            m_isBusy = false;
            m_isStop = false;
            m_color = color;
            m_grs = grs;
            m_soliColor = new SolidBrush(m_color);
            m_thread = new Thread(new ThreadStart(Operate));
            m_pathPlanning = new Stack<Cell>();
            OnInit();
            m_thread.Start();
        }

        //---------------------------------------------------------------
        private void OnUpdate()
        {
            if (m_dirtys.Count != 0)
            {
                m_totalTimes++;
                if (!m_isBusy)
                {
                    m_isBusy = true;
                    //MappingMap();
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
        }

        private void RescanMap()
        {
            // m_com.DrawMatrix();
            // m_grs.DrawImage(m_imgRobot, m_position.X + 1, m_position.Y + 1, m_com.Width - 1, m_com.Height - 1);
            SetupMatrix();
            MappingMap();
            m_map[m_x, m_y].Cost = 0;
            // m_isBusy = false;
        }

        private void SchedulePathPlanning()
        {
            if (m_dirtys.Count == 0)
            {
                return;
            }
            var tem = m_dirtys.Peek();
            while (true)
            {
                if (m_map[tem.X, tem.Y].Cost == 0)
                {
                    break;
                }
                for (int i = 0; i < 4; i++)
                {
                    var X = tem.X + Hx[i];
                    var Y = tem.Y + Hy[i];
                    if (Check(1, X, Y))
                    {
                        if (m_map[X, Y].Cost < m_map[tem.X, tem.Y].Cost)
                        {
                            m_pathPlanning.Push(tem);
                            tem = m_map[X, Y];
                        }
                    }
                }
            }
        }

        private void MappingMap()
        {
            m_map = BL.bl_CleanHouse.GetInstance().MappingMap(m_map, m_x, m_y);
        }

        private void Spill()
        {

            RescanMap();
            m_Open.Enqueue(m_map[m_x, m_y]);

            while (m_Open.Count != 0)
            {
                var _fist = m_Open.Peek();
                for (int ii = 0; ii < 4; ii++)
                {
                    var X = _fist.X + Hx[ii];
                    var Y = _fist.Y + Hy[ii];

                    var values = _fist.Cost;
                    if (Check(0, X, Y))
                    {

                        var po = m_map[X, Y].P;
                        m_map[X, Y].Cost = values + 1;
                        var e = new Cell(X, Y, po, false, values + 1);
                        m_Open.Enqueue(e);
                        //DrawValue(e.P, values + 1);

                        if (m_map[X, Y].Kind == Constants.DIRTY)
                        {
                            m_dirtys.Enqueue(m_map[X, Y]);
                            break;
                        }
                    }
                }
                m_Open.Dequeue();
            }
            //
        }

        private bool Check(int type, int x, int y)
        {
            if (x < 0 || x > m_com.COLUMNS - 1 || y < 0 || y > m_com.ROWS - 1)
            {
                return false;
            }
            if (m_map[x, y].Cost != -1 && type == 0)
            {
                return false;
            }
            if (m_map[x, y].Cost == -100 || m_map[x, y].Kind == Constants.VAT_CAN)
            {
                return false;
            }
            if (m_map[x, y].IsSoHuu && type == 1)
            {
                return false;
            }
            return true;
        }

        public void Swap()
        {
            if (m_dirtys.Count == 0)
            {
                return;
            }

            var dirty = m_dirtys.Peek();
            if (dirty.X == m_x && dirty.Y == m_y)
            {
                m_dirtys.Dequeue();
                m_isBusy = false;
                return;
            }

            if (m_pathPlanning.Count == 0)
            {
                return;
            }
            var next = m_pathPlanning.Pop();
            if (!BL.bl_CleanHouse.GetInstance().IsNonCollision(next.X, next.Y, Tag))
            {
                BL.bl_CleanHouse.GetInstance().ReScanDirty(m_dirtys.Peek());
                Thread.Sleep(500);
                m_dirtys.Clear();
                m_isBusy = false;
                return;
            }
            m_grs.FillRectangle(m_soliColor, m_position.X + 1, m_position.Y + 1, m_Witdh - 1, m_Height - 1);
            m_position = next.P;
            m_x = next.X;
            m_y = next.Y;
            m_isModified = true;
        }

        private void DrawValue(Point p, int val)
        {
            String text = "" + val;
            Font drawFornt = new Font("Arial", 14);
            SolidBrush drawSb = new SolidBrush(Color.Red);
            StringFormat stringFormat = new StringFormat();
            m_grs.DrawString(text, drawFornt, drawSb, p);
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
            m_Open = new Queue<Cell>();
            m_dirtys = new Queue<Cell>();
            SetupMatrix();
            //m_isModified = true;
            m_map[m_x, m_y].Cost = 0;
            m_Open.Enqueue(m_map[m_x, m_y]);
            m_grs.DrawImage(m_imgRobot, m_position.X + 1, m_position.Y + 1, m_com.Width - 1, m_com.Height - 1);
            m_Witdh = m_com.Width;
            m_Height = m_com.Height;

        }

        public void OnRender(Graphics grs)
        {
            if (m_isModified)
            {
                grs.DrawImage(m_imgRobot, m_position.X + 1, m_position.Y + 1, m_com.Width - 1, m_com.Height - 1);
                //  DrawValue(m_position, Tag + 1);
                m_isModified = false;
            }

        }

        public void OnResume()
        {
            //m_thread.Abort();
            if (!m_thread.IsAlive)
            {
                m_thread.Start();
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

        public bool IsBusy()
        {
            return m_isBusy;
        }

        public void SetDirty(Cell dir)
        {
            m_dirtys.Enqueue(dir);
            m_totalTimes = 0;
            BL.bl_CleanHouse.GetInstance().CleanDirty();
        }

        public Point GetPoint()
        {
            return m_position;
        }

        public void Operate()
        {
            while (true)
            {
                OnUpdate();
                OnRender(m_grs);
                // Console.WriteLine("robo Dang chay");
                Thread.Sleep(70);
            }

        }

    }
}
