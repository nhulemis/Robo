using Robo.BL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Robo.UI
{
    public partial class frm_Robo : Form
    {
        public frm_Robo()
        {
            InitializeComponent();
        }
        bl_Robo bl;
        private void frm_Robo_Load(object sender, EventArgs e)
        {
            bl = new bl_Robo(pnlMatBang.CreateGraphics());
            Common.Constants.c = pnlMatBang.BackColor;
            Common.Constants.grs = pnlMatBang.CreateGraphics();
            bl.setTimeClear( radioButton2, radioButton3);
        }

        private void pnlMatBang_Paint(object sender, PaintEventArgs e)
        {
            bl.DrawMatrix(pnlMatBang.CreateGraphics());
        }

        private void pnlMatBang_MouseClick(object sender, MouseEventArgs e)
        {
            if (rbt_Robo.Checked == true)
            {
                if (!bl.caiDatViTri(1, e.X, e.Y))
                {
                    MessageBox.Show("không thể thêm nhiều hơn 2 robo trên bản đồ");
                }
            }
            if (rbt_Dich.Checked == true)
            {
                if (!bl.caiDatViTri(2, e.X, e.Y))
                {
                    MessageBox.Show("không thể đặt vị trí đích tại nơi có chướng ngại vật");
                }
            }
            if (rbt_NgaiVat.Checked == true)
            {
                if (!bl.caiDatViTri(3, e.X, e.Y))
                {
                    MessageBox.Show("cẩn thận với những con robo ở dưới ");
                }
            }
            if (rbt_VetBan.Checked == true)
            {
                bl.caiDatViTri(4, e.X, e.Y);
            }
        }

        private void btn_Run_Click(object sender, EventArgs e)
        {
            bl.Loang();
            switch (bl.UuTien())
            {
                case 0:
                    timer_Robo_1.Enabled = true;
                    timer_Robo_1.Start();
                    Thread.Sleep(50);
                    timer_Robo_2.Enabled = true;
                    timer_Robo_2.Start();
                    break;
                case 1:
                    timer_Robo_2.Enabled = true;
                    timer_Robo_2.Start();
                    Thread.Sleep(50);
                    timer_Robo_1.Enabled = true;
                    timer_Robo_1.Start();
                    break;
                case -1:
                    timer_Robo_1.Enabled = true;
                    timer_Robo_1.Start();
                    break;
                default:

                    break;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            pnlMatBang.CreateGraphics().Clear(pnlMatBang.BackColor);
            bl = new bl_Robo(pnlMatBang.CreateGraphics());
            bl.DrawMatrix(pnlMatBang.CreateGraphics());
            timer_Robo_1.Stop();
            timer_Robo_2.Stop();
            timer1_LauDon.Stop();
            timer2_LauDon.Stop();
           
            bl.setTimeClear(radioButton2, radioButton3);
        }

        private void timer_Robo_1_Tick(object sender, EventArgs e)
        {
            Console.WriteLine("dang chay 1");
            var result = bl.RunNow(0);
            if (result== -1)
            {
                timer_Robo_1.Stop();
            }
            else if (result == 0)
            {
                var time = bl.GetTime(0);
                if (time!=0)
                {
                    timer_Robo_1.Stop();
                    timer1_LauDon.Enabled = true;
                    timer1_LauDon.Start();
                }
            }
            else if (result >=100)
            {

                timer_Robo_1.Stop();
                bl.SetTime(0, result);
                timer1_LauDon.Enabled = true;
                timer1_LauDon.Start();
            }
        }

        private void timer_Robo_2_Tick(object sender, EventArgs e)
        {
            Console.WriteLine("dang chay 2");
            var result = bl.RunNow(1);
            if (result ==-1)
            {
                timer_Robo_2.Stop();
            }
            else if (result == 0)
            {
                var time = bl.GetTime(1);
                if (time != 0)
                {
                    timer_Robo_2.Stop();
                    timer2_LauDon.Enabled = true;
                    timer2_LauDon.Start();
                }
            }
            else if (result >= 100)
            {

                timer_Robo_2.Stop();
                bl.SetTime(1, result);
                timer2_LauDon.Enabled = true;
                timer2_LauDon.Start();
            }
        }

       

        private void timer1_LauDon_Tick(object sender, EventArgs e)
        {
            Console.WriteLine("dang chay lau don 1");
            var result = bl.GetTime(0, 100);
            if (result == 0)
            {
                bl.SetTime(0);
                timer_Robo_1.Start();
                timer1_LauDon.Stop();
                timer1_LauDon.Enabled = false;
            }
        }

        private void timer2_LauDon_Tick(object sender, EventArgs e)
        {
            Console.WriteLine("dang chay lau don 2");
            var result = bl.GetTime(1, 100);
            if (result == 0)
            {
                bl.SetTime(1);
                timer_Robo_2.Start();
                timer2_LauDon.Stop();
                timer2_LauDon.Enabled = false;
            }
        }

        private void radioButton1_Click(object sender, EventArgs e)
        {

        }

        private void radioButton2_Click(object sender, EventArgs e)
        {

        }

        private void radioButton3_Click(object sender, EventArgs e)
        {

        }
    }
}
