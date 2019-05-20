using Robo.Common;
using Robo.src;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Robo.BL
{
    class bl_CleanHouse

    {
        private Cell[,] matrix;
        private Common.Common m_com;
        private List<RoboCleanHouse> m_listRobo;
        private static bl_CleanHouse s_clean;
        private List<Cell> m_dirtys;
        private List<Cell> m_obstacles;
        private Graphics m_grs;
        private SolidBrush m_color;
        private int totaFrame;

        private Thread thread1, thread2;

        public int Width { get; set; }
        public int Height { get; set; }

        public static bl_CleanHouse GetInstance()
        {
            if (s_clean == null)
            {
                s_clean = new bl_CleanHouse();
            }
            return s_clean;
        }
        //--------------------------------------------------------------
        private bl_CleanHouse()
        {
            m_com = Common.Common.GetInstance();
            m_com.InitMatrix();
            matrix = m_com.matrix;
            SetupMatrix();
            m_listRobo = new List<RoboCleanHouse>();
            m_dirtys = new List<Cell>();
            m_obstacles = new List<Cell>();
            // m_grs = m_com.GetGraphics();
            thread1 = new Thread(new ThreadStart(ScanMap));
            thread2 = new Thread(new ThreadStart(OnDrawDirty));
            thread1.Start();
            thread2.Start();
        }

        public void OnExit()
        {
            foreach (var item in m_listRobo)
            {
                item.OnExit();
            }
            thread1.Abort();
            thread2.Abort();
        }

        public void SetEnviroment(int w, int h, Color color, Graphics grs)
        {
            m_grs = grs;
            Width = w;
            Height = h;
            m_color = new SolidBrush(color);
        }

        private void OnDrawDirty()
        {
            
        }

        private void SetupMatrix()
        {
            for (int i = 0; i < m_com.COLUMNS; i++)
            {
                for (int j = 0; j < m_com.ROWS; j++)
                {
                    matrix[i, j] = new Cell(i, j, new Point(i * m_com.Width, j * m_com.Height), false, -1);
                }
            }
        }


        public void AddRobot(RoboCleanHouse rb)
        {
            rb.Tag = m_listRobo.Count;
            m_listRobo.Add(rb);
        }

        public List<RoboCleanHouse> GetListRobo()
        {
            return m_listRobo;
        }

        public void ResizeMap()
        {
            m_dirtys.Clear();
            m_obstacles.Clear();
            m_listRobo.Clear();
        }

        //public void CleanDirty(int x, int y)
        //{
        //    int tag = -1;
        //    foreach (var item in m_dirtys)
        //    {
        //        if (item.X == x && item.Y == y)
        //        {
        //            tag = item.RoboCleaning;
        //            m_dirtys.Remove(item);
        //            break;
        //        }
        //    }
        //    if (tag != -1)
        //    {
        //        StopRobo(tag);
        //    }
        //}

        private void StopRobo(int tag)
        {
            m_listRobo[tag].StopClean();

        }

        //public void CleanDirty(int Tag)
        //{
        //    var dir = m_dirtys[Tag];
        //    m_dirtys.Remove(dir);
        //}

        internal bool isVisible(int tag)
        {
            for (int i = 0; i < m_dirtys.Count; i++)
            {
                var dir = m_dirtys[i];
                if (dir.Tag == tag)
                {
                    return true;
                }
            }
            return false;
        }

        public Cell[,] MappingMap(Cell[,] cell, int roboX, int roboY)
        {
            //foreach (var item in m_listRobo)
            //{
            //    if (item.GetY() != roboY && item.GetX() != roboX)
            //    {
            //        cell[item.GetX(), item.GetY()].IsSoHuu = true;
            //        cell[item.GetX(), item.GetY()].Kind = Constants.ROBO;
            //    }
            //}

            // cell = MappingMapDirty(cell);
            cell = MappingMapObstacle(cell);
            return cell;
        }

        private Cell[,] MappingMapObstacle(Cell[,] cell)
        {
            foreach (var item in m_obstacles)
            {
                cell[item.X, item.Y].Kind = item.Kind;
                cell[item.X, item.Y].Cost = item.Cost;
            }
            return cell;
        }

        internal void RejectDirty(Cell cell)
        {
          //  m_dirtys.Remove(cell);
        }

        public void RePaintObject()
        {
            for (int i = 0; i < m_dirtys.Count; i++)
            {
                var dir = m_dirtys[i];
                DrawDirty(dir.P);
            }
        }

        private Cell[,] MappingMapDirty(Cell[,] cell)
        {
            foreach (var item in m_dirtys)
            {
                cell[item.X, item.Y].Kind = item.Kind;
            }

            return cell;
        }

        public void SetupCoordinates(int v, int Mx, int My)
        {
            var X = Mx / m_com.Width;
            var Y = My / m_com.Height;
            if (!IsNonOverrideRobo(X, Y) || !IsNonOverrideDirty(X, Y) || !IsNonOverrideObstacle(X, Y))
            {
                return;
            }
            var p = new Point(X * m_com.Width, Y * m_com.Height);
            var cell = new Cell(X, Y, p);

            switch (v)
            {
                case 3:// obstacle
                    cell.Kind = Constants.VAT_CAN;
                    cell.Cost = Constants.COST_VAT_CAN;
                    m_com.DrawObstacle(p);
                    m_obstacles.Add(cell);
                    return;
                case 4://dirty
                    cell.Kind = Constants.DIRTY;
                    m_com.DrawDirty(p);
                    cell.HasRoboCleaning = false;
                    cell.RoboCleaning = -1;
                    cell.Tag = m_dirtys.Count;
                    m_dirtys.Add(cell);

                    // CallRoboClean();
                    return;
            }
            //  OnResume();
        }

        private bool IsNonOverrideObstacle(int x, int y)
        {
            foreach (var item in m_obstacles)
            {
                if (item.X == x && item.Y == y)
                {
                    return false;
                }
            }
            return true;
        }

        private bool IsNonOverrideRobo(int x, int y)
        {
            foreach (var item in m_listRobo)
            {
                if (item.GetX() == x && item.GetY() == y)
                {
                    return false;
                }
            }
            return true;
        }

        private bool IsNonOverrideDirty(int x, int y)
        {
            foreach (var item in m_dirtys)
            {
                if (item.X == x && item.Y == y)
                {
                    return false;
                }
            }
            return true;
        }

        private void CallRoboClean()
        {
            Console.WriteLine("Clean : has dirtys: " + m_dirtys.Count);
            for (int i = 0; i < m_listRobo.Count; i++)
            {
                var robo = m_listRobo[i];
                if (!robo.IsBusy() && robo.GetDirty() == null)
                {
                    var dir = CheckNearDirty(robo);
                    if (dir != null)
                    {
                        var _robo = CheckNearRobo(dir);
                        if (_robo != null && _robo == robo)
                        {
                            dir.HasRoboCleaning = true;
                            dir.RoboCleaning = robo.Tag;
                            robo.SetDirty(dir);
                        }
                        else if (_robo != null)
                        {
                            dir.HasRoboCleaning = true;
                            dir.RoboCleaning = _robo.Tag;
                            _robo.SetDirty(dir);
                        }
                        Thread.Sleep(100);
                    }
                    //break;
                }
            }
        }

        private RoboCleanHouse CheckNearRobo(Cell dir)
        {
            double _tempDistance = 10000;
            RoboCleanHouse _tem = null;
            for (int i = 0; i < m_listRobo.Count; i++)
            {
                var robo = m_listRobo[i];
                if (!robo.IsBusy())
                {
                    var distance = DistanceTwoNodes(dir.X, dir.Y, robo.GetX(), robo.GetY());
                    if (distance < _tempDistance)
                    {
                        _tem = robo;
                        _tempDistance = distance;
                    }
                }
            }
            return _tem;
        }

        private Cell CheckNearDirty(RoboCleanHouse robo)
        {
            double _tempDistance = 10000;
            Cell _celTem = null;
            for (int i = 0; i < m_dirtys.Count; i++)
            {
                var dir = m_dirtys[i];
                if (!dir.HasRoboCleaning)
                {
                    var distance = DistanceTwoNodes(dir.X, dir.Y, robo.GetX(), robo.GetY());
                    if (distance < _tempDistance)
                    {
                        _celTem = dir;
                        _tempDistance = distance;
                    }
                }
                else
                {
                    //var _robo = m_listRobo[dir.RoboCleaning];
                    //if (_robo.GetDirty()==null)
                    //{
                    //    return dir;
                    //}
                }
            }
            return _celTem;
        }

        private double DistanceTwoNodes(int x1, int y1, int x2, int y2)
        {
            return Math.Sqrt((x1 - x2) * (x1 - x2) + (y1 - y2) * (y1 - y2));
        }

        public bool IsNonCollision(int x, int y, int tag)
        {
            foreach (var robo in m_listRobo)
            {
                if (robo.Tag != tag
                    && robo.GetX() == x
                    && robo.GetY() == y
                    )
                {
                    m_listRobo[tag].AddException(new Cell(x, y, new Point()));
                    return false;
                }
            }
            for (int i = 0; i < m_obstacles.Count; i++)
            {
                var ob = m_obstacles[i];
                if (ob.X == x && ob.Y == y)
                {
                    return false;
                }
            }
            return true;
        }

        public void OnResume()
        {
            foreach (var robo in m_listRobo)
            {
                robo.OnResume();
            }
        }

        private void OnRender()
        {
            for (int i = 0; i < m_dirtys.Count; i++)
            {
                var item = m_dirtys[i];
                // m_grs.FillRectangle(new SolidBrush(m_color), p.X + 1, p.Y + 1, Width - 1, Height - 1);
                DeleteIcon(item.P);
                DrawDirty(item.P);
            }
        }

        public void DeleteIcon(Point p)
        {
            m_grs.FillRectangle(m_color, p.X + 1, p.Y + 1, Width - 1, Height - 1);
        }

        public void DrawDirty(Point p)
        {
            String path = Application.StartupPath + "\\Resources\\dirty1.png";
            RenderIcon(path, p);
        }

        public void RenderIcon(string path, Point point)
        {
            WebRequest req = WebRequest.Create(path);
            WebResponse res = req.GetResponse();
            Stream imgStream = res.GetResponseStream();
            Image _img = Image.FromStream(imgStream);
            var p = point;
            m_grs.DrawImage(_img, p.X + 1, p.Y + 1, Width - 1, Height - 1);
        }

        private void ScanMap()
        {
            while (true)
            {
                CallRoboClean();
                OnUpdate();
                //  OnRender();
                Thread.Sleep(100);
            }
        }

        private void OnUpdate()
        {
            int totalRoboNonBusy = 0;
            CheckCollision();
            for (int i = 0; i < m_listRobo.Count; i++)
            {
                var robo = m_listRobo[i];
                if (robo.GetDirty() == null)
                {
                    robo.StopClean();
                    totalRoboNonBusy++;
                }
            }
            if (totalRoboNonBusy == m_listRobo.Count)
            {
                for (int i = 0; i < m_dirtys.Count; i++)
                {
                    var dir = m_dirtys[i];
                    dir.HasRoboCleaning = false;
                    dir.RoboCleaning = -1;
                }
            }
        }

        private void CheckCollision()
        {
            for (int i = 0; i < m_dirtys.Count; i++)
            {
                var dir = m_dirtys[i];
                for (int j = 0; j < m_listRobo.Count; j++)
                {
                    var robo = m_listRobo[j];
                    if (dir.X == robo.GetX() && dir.Y == robo.GetY())
                    {
                        robo.CleanSuccess();
                        DeleteIcon(dir.P);
                        m_dirtys.Remove(dir);
                        break;
                    }
                }
            }

        }

        public void ReScanDirty(Cell cel)
        {
            for (int i = 0; i < m_dirtys.Count; i++)
            {
                var dir = m_dirtys[i];
                if (dir.X == cel.X && dir.Y == cel.Y)
                {
                    return;
                }
            }

            cel.HasRoboCleaning = false;
            m_dirtys.Add(cel);
        }

    }
}
