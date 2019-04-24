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
    public partial class frm_CleanHouse : Form
    {
        public frm_CleanHouse()
        {
            InitializeComponent();
        }

        private void frm_CleanHouse_Load(object sender, EventArgs e)
        {
            Common.Constants.c = pnl_display.BackColor;
            Common.Constants.grs = pnl_display.CreateGraphics();
        }


    }
}
