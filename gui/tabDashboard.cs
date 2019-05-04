using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Bunifu.DataViz.WinForms;
namespace PriClinic
{
    public partial class tabDashboard : UserControl
    {
        public tabDashboard()
        {
            if(Program.IsInDesignMode())
            {
                return;
            }
            InitializeComponent();

        }




        private void progressBarUpdate_Tick(object sender, EventArgs e)
        {
            Random r = new Random();
            bunifuProgressBar1.Value = r.Next(0, 100);
            bunifuProgressBar2.Value = r.Next(bunifuProgressBar1.Value, 100);
            bunifuCircleProgressbar1.Value = bunifuProgressBar1.Value;
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void Label2_Click(object sender, EventArgs e)
        {

        }

        private void PictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
