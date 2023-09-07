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
using ScottPlot.Renderable;

namespace Test
{
    public partial class Form1 : Form
    {
        Plotter plotter;
        DataReceiver dataReceiver;

        public Form1()
        {
            InitializeComponent();
            this.button1.Click += (sender, e) => { this.label1.Text = "Clicked"; };
            this.plotter = new(formsPlot1);
            dataReceiver = new();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void formsPlot1_Load(object sender, EventArgs e)
        {

        }


        private void button1_Click_1(object sender, EventArgs e)
        {
            /* ボタンを押すと、データを受け取ったと仮定して、プロッタにデータを渡す */
            if (dataReceiver.GetVoltageData(out long u_sec, out double voltage))
            {
                this.plotter.registerData(u_sec, voltage);
            }

            /* 横幅、縦幅は、今のところ自動で変わる */
            formsPlot1.Plot.SetAxisLimitsX(xMin: 0, xMax: 0.01);
            formsPlot1.Plot.AxisAutoY();
            formsPlot1.Plot.AxisAutoX();

            //formsPlot1.Plot.AxisPan(10, 10);
            this.formsPlot1.Refresh();
        }
    }
}
