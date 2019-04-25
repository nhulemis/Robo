using Robo.Common;
using Robo.src;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Robo.BL
{
    class bl_Robo
    {
        #region 
        RadioButton  b, c;
        private Cell cell;
        private MatBang mb;
        private Cell[,] matrix;
        List<Cell> listsRepaint;
        Graphics g;
        Robot[] robos;
        int soRobot;
        Mark mark;
        List<Obstacle> obstacles;
        Stack<Cell> oldLocalRobo1;
        Stack<Cell> oldLocalRobo2;
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
            oldLocalRobo1 = new Stack<Cell>();
            oldLocalRobo2 = new Stack<Cell>();
        }


        #region thiết lập và loang

        public void setTimeClear(RadioButton b, RadioButton c)
        {
            
            this.b = b;
            this.c = c;
        }
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
                    robo.DrawRobo();
                    robos[soRobot] = robo;
                    soRobot++;
                    matrix[X, Y].IsSoHuu = true;
                    matrix[X, Y].Kind = Constants.ROBO;
                    listsRepaint.Add(robo);
                    return true;
                case 2:// dich
                    if (mark != null)
                    {
                        Constants.xoaIcon(mark.P);
                    }
                    VeLaiRobo();
                    thietLapMatBang();
                    matrix[X, Y].Cost = 0;
                    mark = new Mark(X, Y, matrix[X, Y].P, false, 0);
                    mark.DrawMark(g);
                    Open = new Queue<Cell>();
                    Open.Enqueue(mark);
                    ResetCost();
                    // Loang();
                    return true;
                case 3:// obstacle                    
                    var obstacle = new Obstacle(X, Y, matrix[X, Y].P, true, -100);
                    obstacle.DrawObstacle(g);
                    obstacles.Add(obstacle);
                    matrix[X, Y].Cost = -100;
                    matrix[X, Y].IsSoHuu = true;
                    matrix[X, Y].Kind = Constants.VAT_CAN;
                    listsRepaint.Add(obstacle);
                    return true;
                case 4://dirty
                    var dirty = new Dirty(X, Y, matrix[X, Y].P, true);
                    dirty.DrawDirty(g);
                    matrix[X, Y].IsSoHuu = true;
                    matrix[X, Y].Kind = Constants.DIRTY;
                    listsRepaint.Add(dirty);
                    return true;
            }

            return false;
        }



        private void VeLaiRobo()
        {
            foreach (var item in robos)
            {
                if (item != null)
                {
                    item.DrawRobo();
                }
            }
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
                    matrix[item.X, item.Y].Kind = Constants.VAT_CAN;
                }
                if (item is Dirty)
                {
                    matrix[item.X, item.Y].Kind = Constants.DIRTY;
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
                       // mb.GiaTri(e.P, values + 1);

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
            if (matrix[x, y].Cost == -100 || matrix[x, y].Kind == Constants.VAT_CAN)
            {
                return false;
            }
            return true;
        }
        #endregion

        #region xử lý robo hoạt động
        /// <summary>
        /// yêu cầu robo hoạt động
        /// </summary>
        /// <param name="robo">robo thứ ?</param>
        /// <returns>-1 là dừng lại
        /// , 0 là tiếp tục</returns>
        public int RunNow(int robo)
        {
            if (Cost(robos[robo].X, robos[robo].Y) == -1)
            {
                return -1; // không thấy đường đi
            }
            else
            {
                if (robos[robo].X == mark.X && robos[robo].Y == mark.Y)
                    return -1;// dừng chạy khi robo tới đích
                Cell next = Next(robos[robo].X, robos[robo].Y);
                if (next != null)
                {
                    if (matrix[next.X, next.Y].IsSoHuu == false)
                    {
                        Swap(robo, next);
                        return 0; // tiếp tục di chuyển
                    }
                    else
                    {
                        if (matrix[next.X, next.Y].Kind == Constants.DIRTY)
                        {
                            Swap(robo, next);
                            //matrix[next.X, next.Y].IsSoHuu = false;
                            LauDon(robo);
                            return 0; // tiếp tục vận hành
                        }
                        else if (matrix[next.X, next.Y].Kind == Constants.VAT_CAN)
                        {
                            return -1;// kiểm tra lại vị trí vật cản này
                        }
                        else if (matrix[next.X, next.Y].Kind == Constants.ROBO)
                        {
                            int tinHieu;
                            if (robo == 0)
                                tinHieu = 1;
                            else
                                tinHieu = 0;
                            if (matrix[robos[tinHieu].X, robos[tinHieu].Y].Cost == 0)
                                return -1; // đã có robo khác chiếm vị trí đích

                            // loang lại
                            thietLapMatBang();
                            matrix[mark.X, mark.Y].Cost = 0;
                            matrix[robos[tinHieu].X, robos[tinHieu].Y].Cost = 10000; // không cho vệt loang qua đây
                            ResetCost();
                            Loang();
                            var _newCost = (matrix[robos[robo].X, robos[robo].Y].Cost - robos[robo].oldCost) * 500;
                            if (robos[tinHieu].ThoiGianNghi < _newCost)
                            {
                                Console.WriteLine("chờ robo " + tinHieu);
                                return robos[tinHieu].ThoiGianNghi + 100; // return chờ
                            }
                            else
                            {
                                Console.WriteLine("Đi đường mới");
                                return 0; // chạy đường mới
                            }
                        }
                    }
                }
                else
                {
                    // da bi chang duong boi 1 vat cang moi
                    thietLapMatBang();
                    matrix[mark.X, mark.Y].Cost = 0;
                    ResetCost();
                    Loang();
                    return 0;
                }
            }
            return -1;
        }

        private void LauDon(int robo)
        {            
            if (b.Checked == true)
            {
                robos[robo].ThoiGianNghi = 2500;
            }
            if (c.Checked == true)
            {
                robos[robo].ThoiGianNghi = 5000;
            }
            else
            {
                robos[robo].ThoiGianNghi = 2500;
            }
           
        }

        /// <summary>
        /// thay đổi vị trí 
        /// </summary>
        /// <param name="robo"></param>
        /// <param name="next"></param>
        private void Swap(int robo, Cell next)
        {
            matrix[robos[robo].X, robos[robo].Y].IsSoHuu = false;
            matrix[robos[robo].X, robos[robo].Y].Kind = null;
            if (matrix[robos[robo].X, robos[robo].Y].Cost == 10000)
            {
                matrix[robos[robo].X, robos[robo].Y].Cost = robos[robo].oldCost;
            }
            Constants.xoaIcon(robos[robo].P);
            robos[robo].X = next.X;
            robos[robo].Y = next.Y;
            robos[robo].P = next.P;
            robos[robo].oldCost = next.Cost;
            robos[robo].DrawRobo();
            switch (robo)
            { // lưu lại bước đi trước đó
                case 0:
                    oldLocalRobo1.Push(robos[robo]);
                    break;
                case 1:
                    oldLocalRobo2.Push(robos[robo]);
                    break;
            }
            matrix[robos[robo].X, robos[robo].Y].IsSoHuu = true;
            matrix[robos[robo].X, robos[robo].Y].Kind = Constants.ROBO;
        }

        /// <summary>
        /// bước tiếp theo của robo
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        private Cell Next(int x, int y)
        {
            var cost = matrix[x, y];
            var temp = cost;
            for (int i = 0; i < 4; i++)
            {
                var xx = x + Hx[i];
                var yy = y + Hy[i];
                if (Check2(xx, yy))
                {
                    if (matrix[xx, yy].Cost < cost.Cost)
                    {
                        cost = matrix[xx, yy];
                    }
                }
            }
            if (temp == cost)
            {
                return null;
            }
            return cost;
        }

        private bool Check2(int x, int y)
        {
            if (x < 0 || x > mb.Columns - 1 || y < 0 || y > mb.Rows - 1)
            {
                return false;
            }
            if (matrix[x, y].Cost == -1)
            {
                return false;
            }
            if (matrix[x, y].Cost == -100)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// hàm lấy chi phí hiện tại của robo
        /// </summary>
        /// <param name="x">tọa độ trục X</param>
        /// <param name="y">tọa độ trục Y</param>
        /// <returns></returns>
        private int Cost(int x, int y)
        {
            return matrix[x, y].Cost;
        }


        /// <summary>
        /// kiểm tra ưu tiên để chạy trước
        /// </summary>
        /// <returns>1 robo 1 gần hơn
        /// , 0 robo 0 gần hơn,
        /// -1 chỉ có 1 robo
        /// -2 không có robo nào</returns>
        public int UuTien()
        {
            if (robos[0] != null && robos[1] != null)
            {
                if (matrix[robos[0].X, robos[0].Y].Cost > matrix[robos[1].X, robos[1].Y].Cost)
                {
                    return 1;
                }
                else return 0;
            }
            else if (robos[0] != null)
            {
                return -1;
            }
            return -2;
        }


        public int GetTime(int robo)
        {
            return robos[robo].ThoiGianNghi;
        }

        public int GetTime(int robo, int time)
        {
            return robos[robo].ThoiGianNghi -= time;
        }

        public void SetTime(int robo)
        {
            robos[robo].ThoiGianNghi = 0;
        }

        public void SetTime(int robo, int time)
        {
            robos[robo].ThoiGianNghi = time;
        }
        #endregion
    }
}
