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
        public bool GetVoltageData(out long u_sec, out double voltage)
        {
            u_sec = cnt += 100;
            voltage = 0.0;
            return true;
        }
    }
}