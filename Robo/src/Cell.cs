using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Robo.src
{
    class Cell
    {
        public static int Height = 40;
        public static int Width = 40;

        public int X { get; set; }
        public int Y { get; set; }
        public bool HasRoboCleaning { get; set; }

        public Point P { get; set; }

        public int Cost { get; set; }
        public bool IsSoHuu { get; set; }
        public String Kind { get; set; }
        public Cell() { }


        public Cell(int X, int Y, Point p, bool isSoHuu,int cost)
        {
            this.X = X;
            this.Y = Y;
            this.P = p;
            this.IsSoHuu = isSoHuu;
            this.Cost = cost;
        }

        public Cell(int X, int Y, Point p, bool isSoHuu)
        {
            this.X = X;
            this.Y = Y;
            this.P = p;
            this.IsSoHuu = isSoHuu;            
        }

        public Cell(int X, int Y, Point p)
        {
            this.X = X;
            this.Y = Y;
            this.P = p;
            
        }
        public Cell(int X, int Y, Point p, bool isSoHuu,String loai)
        {
            this.X = X;
            this.Y = Y;
            this.P = p;
            this.IsSoHuu = isSoHuu;
            this.Kind = loai;
        }

        public Cell(int X, int Y, Point p, bool isSoHuu, String loai,int cost)
        {
            this.X = X;
            this.Y = Y;
            this.P = p;
            this.IsSoHuu = isSoHuu;
            this.Kind = loai;
            this.Cost = cost;
        }

    }
}
