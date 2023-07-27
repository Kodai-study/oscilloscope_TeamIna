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
        public event EventHandler<string> DataReceived;
        private long cnt;
        private string bufstr;

        /// <summary>
        /// 指定されたCOMポートでシリアル通信を開始します。
        /// シリアルデータを受け取った時のイベントハンドラも開始します。
        /// </summary>
        /// <param name="comPort">シリアル通信のポート番号</param>
        /// <returns>シリアル通信の開始に成功したかどうかを示す値</returns>
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

        public bool GetVoltageData(out long u_sec, out double voltage)
        {
            u_sec = cnt += 100;
            voltage = 0.0;
            return true;
        }

        /// <summary>
        /// シリアルポートから受信したデータを処理します。
        /// </summary>
        /// <param name="sender">イベントの送信元オブジェクト</param>
        /// <param name="e">イベントのデータ</param>
        public void DataReceivedHandler(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort sp = (SerialPort)sender;
            string dataReceived = sp.ReadExisting();
            bufstr = dataReceived;
            OnDataReceived(dataReceived);
            Console.WriteLine(dataReceived);
        }

        /// <summary>
        /// DataReceivedイベントを発生させるメソッド
        /// </summary>
        /// <param name="data">受信したデータ</param>
        protected virtual void OnDataReceived(string data)
        {
            DataReceived?.Invoke(this, data);
        }

    }
}