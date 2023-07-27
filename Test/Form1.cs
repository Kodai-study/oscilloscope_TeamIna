using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Test
{
    public partial class Form1 : Form
    {
        DataReceiver dataReceiver = new DataReceiver();
        /// <summary>
        /// フォームのクラス
        /// </summary>
        public Form1()
        {
            InitializeComponent();
            //this.button1.Click += (sender, e) => { this.label1.Text = "Clicked"; };
            if(dataReceiver.openSerial("COM9") == true)
            {
                writeLabel("Conneced!");
            }
            dataReceiver.DataReceived += DataReceiver_DataReceived;
        }

        /// <summary>
        /// DataReceivedイベントのハンドラーメソッド
        /// </summary>
        /// <param name="sender">イベントの送信元オブジェクト</param>
        /// <param name="data">受信したデータ</param>
        private void DataReceiver_DataReceived(object sender, string data)
        {
            // DataReceivedイベントが発生したときに、writeLabelメソッドを呼び出す
            writeLabel(data);
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


        /// <summary>
        /// ラベルにテキストを表示するメソッド
        /// </summary>
        /// <param name="str">表示するテキスト</param>
        public void writeLabel(string str)
        {
            this.label1.Text = str;
        }

    }
}
