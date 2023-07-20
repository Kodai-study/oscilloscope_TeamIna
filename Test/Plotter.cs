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

        private List<double> voltageDataList;
        private List<double> timeDataList;
        public Plotter(FormsPlot formPlot)
        {
            this.formPlot = formPlot;
            voltageDataList = new List<double>();
            timeDataList = new List<double>();
        }
        public void registerData(long u_sec, double voltageData)
        {
            voltageDataList.Add(voltageData);
            timeDataList.Add(u_sec * 10e-6);
            formPlot.Plot.AddScatter(timeDataList.ToArray(), voltageDataList.ToArray());
        }
    }
}