using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using System.Text;
using System.Diagnostics;

namespace Test
{
    public class DataReceiver
    {
        private long cnt;
        private string bufstr;
        public bool openSerial(string comPort)
        {
            // シリアルポートの設定
            SerialPort sport = new SerialPort
            {
                PortName = comPort,                    //ポート番号
                BaudRate = 9600,                   //ボーレート
                DataBits = 8,                      //データビット
                Parity = Parity.None,              //パリティ
                StopBits = StopBits.One,           //ストップビット
                Handshake = Handshake.None,  //ハンドシェイク
                Encoding = Encoding.UTF8,          //エンコード
                WriteTimeout = 100000,             //書き込みタイムアウト
                ReadTimeout = 100000,              //読み取りタイムアウト
                NewLine = "\r\n"                   //改行コード指定
            };
            if (!sport.IsOpen)
            {
                try
                {
                    sport.Open();  // ポートオープン
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                    return false;
                }
            }
            sport.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);
            return true;
        }
        public string getSerialLine()
        {
            string str = null;
            if (bufstr != null)
            {
                str = bufstr;
                bufstr = null;
            }
            return str;
        }
        public bool GetVoltageData(out long u_sec, out double voltage)
        {
            u_sec = cnt += 100;
            voltage = 0.0;
            return true;
        }

        private void DataReceivedHandler(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort sp = (SerialPort)sender;
            string dataReceived = sp.ReadExisting();
            bufstr = dataReceived;
            Console.WriteLine(dataReceived);
        }

    }
}