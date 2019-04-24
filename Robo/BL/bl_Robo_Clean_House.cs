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
            m_listRobo.Add(rb);
            new Thread(new ThreadStart(rb.Operate)).Start();
        }

        public List<RoboCleanHouse> GetListRobo()
        {
            return m_listRobo;
        }

        public Cell[,] MappingMap(Cell[,] cell ,int x, int y)
        {
            foreach (var item in m_listRobo)
            {
                if (item.GetY() != y && item.GetX() != x)
                {
                    cell[item.GetX(), item.GetY()].IsSoHuu = true;
                    cell[item.GetX(), item.GetY()].Loai = Constants.ROBO;
                }
            }
            return cell;
        }

        public void SetupCoordinates(int v, int Mx, int My)
        {
            var X = Mx / m_com.Width;
            var Y = My / m_com.Height;
            if (matrix[X, Y].IsSoHuu)
            {
                return ; // dừng chương trình không thông báo
            }
            switch (v)
            {
                case 3:// obstacle                    
                    //var obstacle = new Obstacle(X, Y, matrix[X, Y].P, true, -100);
                    //obstacle.DrawObstacle(g);
                  //  obstacles.Add(obstacle);
                    matrix[X, Y].Cost = -100;
                    matrix[X, Y].IsSoHuu = true;
                    matrix[X, Y].Loai = Constants.VAT_CAN;
                    m_com.DrawObstacle(matrix[X, Y].P);
                   // listsRepaint.Add(obstacle);
                    return ;
                case 4://dirty
                   // var dirty = new Dirty(X, Y, matrix[X, Y].P, true);
                    //dirty.DrawDirty(g);
                   // matrix[X, Y].IsSoHuu = true;
                    matrix[X, Y].Loai = Constants.DIRTY;
                    m_com.DrawDirty(matrix[X, Y].P);
                   // listsRepaint.Add(dirty);
                    return ;
            }
        }



        
    }
}
