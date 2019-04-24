using System;
using System.Threading;
using System.Windows.Forms;
using Robo.BL;
using Robo.src;

namespace Robo.UI
{
 
    
    public partial class frm_CleanHouse : Form
    {
        private bl_CleanHouse bl;


        public frm_CleanHouse()
        {
            InitializeComponent();
           
        }

        private void frm_CleanHouse_Load(object sender, EventArgs e)
        {
            /// 925 x 650
            /// 
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            Common.Common _com = Common.Common.GetInstance();
            _com.SetGraphics(pnl_display.CreateGraphics(), pnl_display.BackColor);
            _com.COLUMNS = 20;
            _com.ROWS = 20;
            _com.Width = pnl_display.Width / Common.Common.GetInstance().COLUMNS;
            _com.Height = pnl_display.Height / Common.Common.GetInstance().ROWS;

            _com.DrawMatrix();
            bl = bl_CleanHouse.GetInstance() ;
        }

        private void pnl_display_Paint(object sender, PaintEventArgs e)
        {
            Common.Common.GetInstance().DrawMatrix();
        }

        private void btn_resize_Click(object sender, EventArgs e)
        {
            int rows = Int32.Parse(txt_rows.Text);
            int cols = Int32.Parse(txt_columns.Text);
            Common.Common.GetInstance().COLUMNS = cols;
            Common.Common.GetInstance().ROWS = rows;
            if (rows != cols)
            {
                if (rows > cols)
                {
                    Common.Common.GetInstance().Width = pnl_display.Height / Common.Common.GetInstance().ROWS;
                    Common.Common.GetInstance().Height = pnl_display.Height / Common.Common.GetInstance().ROWS;
                }
                else
                {
                    Common.Common.GetInstance().Width = pnl_display.Width / Common.Common.GetInstance().COLUMNS;
                    Common.Common.GetInstance().Height = pnl_display.Width / Common.Common.GetInstance().COLUMNS;
                }
            }
            else
            {
                Common.Common.GetInstance().Width = pnl_display.Width / Common.Common.GetInstance().COLUMNS;
                Common.Common.GetInstance().Height = pnl_display.Height / Common.Common.GetInstance().ROWS;
            }
            Common.Common.GetInstance().DrawMatrix();
        }



        private void pnl_display_MouseClick(object sender, MouseEventArgs e)
        {
            if (rbt_Robo.Checked)
            {
                
                RoboCleanHouse robo = new RoboCleanHouse(e.X / Common.Common.GetInstance().Width, e.Y / Common.Common.GetInstance().Height);
                bl.AddRobot(robo);
            }
            else if(rbt_VetBan.Checked)
            {
                bl.SetupCoordinates(4, e.X, e.Y);
            }
            else if (rbt_NgaiVat.Checked)
            {
                bl.SetupCoordinates(3, e.X, e.Y);
            }
        }
    }
}
