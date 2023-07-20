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
            long u_sec;
            double voltage;
            if(dataReceiver.GetVoltageData(out u_sec, out voltage))
            {
                this.plotter.registerData(u_sec, voltage);
            }
            formsPlot1.Plot.SetAxisLimitsX(xMin: 0, xMax: 0.01);
            //formsPlot1.Plot.AxisAutoX();

            formsPlot1.Plot.AxisAutoY();
            this.formsPlot1.Refresh();
        }
    }
}
