using ScottPlot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Test
{
    public class Plotter
    {
        private FormsPlot formPlot;
        public Plotter(FormsPlot formPlot)
        {
            this.formPlot = formPlot;
        }
        private double[] data;
        public void registerData(long u_sec, double data) { }
    }
}