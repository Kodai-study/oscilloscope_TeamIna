using ScottPlot;
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
using ScottPlot.Renderable;
using System.Text.RegularExpressions;

namespace Test
{
    public partial class Form1 : Form
    {
        Plotter plotter;
        DataReceiver dataReceiver;
        /// <summary>
        /// フォームのクラス
        /// </summary>
        public Form1()
        {
            InitializeComponent();

            plotter = new(formsPlot1);
            this.dataReceiver = new(plotter);

            if (dataReceiver.openSerial("COM8") == true)
            {
                writeLabel("Conneced!");
            }
            //this.button1.Click += (sender, e) => { this.label1.Text = "Clicked"; };
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
            //writeLabel(data);
            writeLabel(data);
            var Lines = data.Split("\r\n");
            try
            {
                //     if (Lines.Length >= 2)
                //         datas = Lines[Lines.Length - 2].Split(',');
                //     else
                //         datas = Lines[0].Split(',');
                //
                //     if (datas[0] == "")
                //         return;


                // 正規表現を使用してデータを解析する
                Regex regex = new Regex(@"^(\d+),(\d+)$");
                
                foreach (var e in Lines)
                {
                    Match match = regex.Match(e);
                    if (match.Success)
                    {
                        // グループ1は秒数、グループ2は電圧値
                        string seconds = match.Groups[1].Value;
                        string voltage = match.Groups[2].Value;
                        if (string.IsNullOrEmpty(seconds) || Int64.Parse(seconds) <= 100000)
                        {
                            continue;
                        }
                        plotter.registerData(Int64.Parse(seconds), Int64.Parse(voltage));


                        if (Int64.Parse(seconds) <= 10000)
                        {
                            Console.WriteLine("#");
                        }
                        formsPlot1.Refresh();
                    }
                }


            }
            catch (Exception e)
            {

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
            formsPlot1.Refresh();
        }


        /// <summary>
        /// ラベルにテキストを表示するメソッド
        /// </summary>
        /// <param name="str">表示するテキスト</param>
        public void writeLabel(string str)
        {
            try
            {
                //  this.label1.Text = str;
                this.Invoke((MethodInvoker)delegate
                {
                    // label1を更新するコード
                    label1.Text = str;
                });
            }
            catch (Exception e)
            {

            }
        }

    }
}