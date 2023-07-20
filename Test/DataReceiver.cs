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
            // �V���A���|�[�g�̐ݒ�
            SerialPort sport = new SerialPort
            {
                PortName = comPort,                    //�|�[�g�ԍ�
                BaudRate = 9600,                   //�{�[���[�g
                DataBits = 8,                      //�f�[�^�r�b�g
                Parity = Parity.None,              //�p���e�B
                StopBits = StopBits.One,           //�X�g�b�v�r�b�g
                Handshake = Handshake.None,  //�n���h�V�F�C�N
                Encoding = Encoding.UTF8,          //�G���R�[�h
                WriteTimeout = 100000,             //�������݃^�C���A�E�g
                ReadTimeout = 100000,              //�ǂݎ��^�C���A�E�g
                NewLine = "\r\n"                   //���s�R�[�h�w��
            };
            if (!sport.IsOpen)
            {
                try
                {
                    sport.Open();  // �|�[�g�I�[�v��
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