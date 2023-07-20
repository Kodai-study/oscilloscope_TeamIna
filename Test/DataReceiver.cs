using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Test
{
    public class DataReceiver
    {
        private long cnt;
        private double sample_volt = 0.0;
        public bool GetVoltageData(out long u_sec, out double voltage)
        {
            u_sec = cnt += 100;
            voltage = sample_volt += 0.1;
            return true;
        }
    }
}