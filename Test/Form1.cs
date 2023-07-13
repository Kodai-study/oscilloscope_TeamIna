using ScottPlot;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Test
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        //    this.button1.Click += (sender, e) => { this.label1.Text = "Clicked"; };
        }
        
        private void formsPlot1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            double[] xs = DataGen.Consecutive(51);
            double[] sin = DataGen.Sin(51);
            formsPlot1.Plot.AddScatter(xs, sin);
            formsPlot1.Refresh();
        }
    }
}
