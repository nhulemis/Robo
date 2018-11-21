using Robo.Common;
using Robo.src;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Robo.BL
{
    class bl_Robo
    {
        #region 
        private Cell cell;
        private MatBang mb;
        private Cell[,] matrix;
        List<Cell> listsRepaint;
        Graphics g;
        Robot[] robos;
        int soRobot;
        Mark mark;
        List<Obstacle> obstacles;

        private int[] Hx = { 1, 0, -1, 0 };
        private int[] Hy = { 0, 1, 0, -1 };

        public Queue<Cell> Open { get; private set; }
        #endregion
        public bl_Robo(Graphics g)
        {
            cell = new Cell();
            mb = new MatBang(10, 10);
            matrix = new Cell[mb.Columns, mb.Rows];
            thietLapMatBang();
            listsRepaint = new List<Cell>();
            this.g = g;
            soRobot = 0;
            robos = new Robot[2];
            mark = new Mark();
            obstacles = new List<Obstacle>();
        }


        #region thiết lập và loang
        private void thietLapMatBang()
        {
            for (int i = 0; i < mb.Columns; i++)
            {
                for (int j = 0; j < mb.Rows; j++)
                {
                    matrix[i, j] = new Cell(i, j, new Point(i * Cell.Width, j * Cell.Height), false, -1);
                }
            }
        }

        internal bool caiDatViTri(int v, int Mx, int My)
        {
            var X = Mx / Cell.Width;
            var Y = My / Cell.Height;
            if (matrix[X, Y].IsSoHuu == true)
                return true; // dừng chương trình không thông báo
            switch (v)
            {
                case 1: // robo
                    if (soRobot == 2) return false; // dừng chương trình có thông báo                  
                    var robo = new Robot(0, X, Y, matrix[X, Y].P, true);
                    robo.DrawRobo(g);
                    robos[soRobot] = robo;
                    soRobot++;
                    matrix[X, Y].IsSoHuu = true;
                    listsRepaint.Add(robo);
                    return true;
                case 2:// dich
                    if (mark != null)
                    {
                        Constants.xoaIcon(g, mark.P);
                    }
                    thietLapMatBang();
                    matrix[X, Y].Cost = 0;
                    mark = new Mark(X, Y, matrix[X, Y].P, false, 0);
                    mark.DrawMark(g);
                    Open = new Queue<Cell>();
                    Open.Enqueue(mark);
                    ResetCost();
                    Loang();
                    return true;
                case 3:// obstacle                    
                    var obstacle = new Obstacle(X, Y, matrix[X, Y].P, true, -100);
                    obstacle.DrawObstacle(g);
                    obstacles.Add(obstacle);
                    matrix[X, Y].Cost = -100;
                    matrix[X, Y].IsSoHuu = true;
                    listsRepaint.Add(obstacle);
                    return true;
                case 4://dirty
                    var dirty = new Dirty(X, Y, matrix[X, Y].P, true);
                    dirty.DrawDirty(g);
                    matrix[X, Y].IsSoHuu = true;
                    listsRepaint.Add(dirty);
                    return true;
            }

            return false;
        }

        public void DrawMatrix(Graphics g)
        {
            mb.DrawMatrix(g);
        }

        private void ResetCost()
        {
            foreach (var item in listsRepaint)
            {
                matrix[item.X, item.Y].IsSoHuu = true;
                if (item is Obstacle)
                {
                    matrix[item.X, item.Y].Cost = -100;
                }
            }
        }

        public void Loang()
        {
            if (mark == null)
            {
                return;
            }
            //matrix[endPoint.X, endPoint.Y].giaTri = 0;
            while (Open.Count != 0)
            {
                var _fist = Open.Peek();
                for (int ii = 0; ii < 4; ii++)
                {
                    var X = _fist.X + Hx[ii];
                    var Y = _fist.Y + Hy[ii];

                    var values = _fist.Cost;
                    if (Check(X, Y))
                    {
                        var po = matrix[X, Y].P;
                        matrix[X, Y].Cost = values + 1;
                        var e = new Cell(X, Y, po, false, values + 1);
                        Open.Enqueue(e);
                        mb.GiaTri(e.P, values + 1);

                    }
                }
                Open.Dequeue();

            }// ket thúc vòng while
            Open.Enqueue(mark);
        }

        private bool Check(int x, int y)
        {
            if (x < 0 || x > mb.Columns - 1 || y < 0 || y > mb.Rows - 1)
            {
                return false;
            }
            if (matrix[x, y].Cost != -1)
            {
                return false;
            }
            if (matrix[x, y].Cost == -100 || matrix[x, y].Loai == Constants.VAT_CAN)
            {
                return false;
            }
            return true;
        }
        #endregion

        #region xử lý robo hoạt động


        #endregion
    }
}
