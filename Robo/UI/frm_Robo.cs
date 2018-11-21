using Robo.BL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
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

        }

        private void button1_Click(object sender, EventArgs e)
        {
            pnlMatBang.CreateGraphics().Clear(pnlMatBang.BackColor);
            bl = new bl_Robo(pnlMatBang.CreateGraphics());
            bl.DrawMatrix(pnlMatBang.CreateGraphics());
        }
    }
}
