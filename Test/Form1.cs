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
        DataReceiver dataReceiver = new DataReceiver();
        
        public Form1()
        {
            InitializeComponent();
            this.button1.Click += (sender, e) => { this.label1.Text = "Clicked"; };
            if(dataReceiver.openSerial("COM9") == true)
            {
                writeLabel("Conneced!");
            }
        }
        
        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void formsPlot1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {

        }

        public void writeLabel(string str)
        {
            this.label1.Text = str;
        }
    }
}
