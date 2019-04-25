using Robo.Common;
using Robo.src;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
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
        private Queue<Cell> m_dirtys;
        private List<Cell> m_obstacles;


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
            m_dirtys = new Queue<Cell>();
            m_obstacles = new List<Cell>();

            new Thread(new ThreadStart(ScanMap)).Start();
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
        int i = 0;
        public void AddRobot(RoboCleanHouse rb)
        {
            rb.Tag = i;
            i++;
            m_listRobo.Add(rb);
            // new Thread(new ThreadStart(rb.Operate)).Start();
        }

        public List<RoboCleanHouse> GetListRobo()
        {
            return m_listRobo;
        }

        public void ResizeMap()
        {
            m_listRobo.Clear();
        }

        public void CleanDirty(int x, int y)
        {
            foreach (var item in m_dirtys)
            {
                if (item.X == x && item.Y == y)
                {
                    //m_dirtys.Dequeue()
                    break;
                }
            }
        }

        public void CleanDirty()
        {
            m_dirtys.Dequeue();
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
            if (!IsNonOverrideRobo(X, Y) || !IsNonOverrideDirty(X,Y) || !IsNonOverrideObstacle(X,Y))
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
                    cell.HasRoboCleaning= false;
                    m_dirtys.Enqueue(cell);

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
            foreach (var dir in m_dirtys)
            {
                if (!dir.HasRoboCleaning)
                {
                   
                    foreach (var robo in m_listRobo)
                    {
                        if (!robo.IsBusy())
                        {
                            dir.HasRoboCleaning = true;

                            robo.SetDirty(dir);
                            break;
                        }
                    }
                    break;
                }
            }
        }

        public bool IsNonCollision(int x,int y, int tag)
        {
            foreach (var item in m_listRobo)
            {
                if (item.Tag != tag
                    && item.GetX() == x
                    && item.GetY() == y
                    )
                {
                    return false;
                }
            }
            return true;
        }

        public void OnResume()
        {
            foreach (var item in m_listRobo)
            {
                item.OnResume();
            }
        }

        private void ScanMap()
        {
            while (true)
            {
                CallRoboClean();
                Thread.Sleep(100);
            }
        }

        public void ReScanDirty(Cell cel)
        {
            cel.HasRoboCleaning = false;
            m_dirtys.Enqueue(cel);
        }

    }
}
