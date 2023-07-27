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
        /// �w�肳�ꂽCOM�|�[�g�ŃV���A���ʐM���J�n���܂��B
        /// �V���A���f�[�^���󂯎�������̃C�x���g�n���h�����J�n���܂��B
        /// </summary>
        /// <param name="comPort">�V���A���ʐM�̃|�[�g�ԍ�</param>
        /// <returns>�V���A���ʐM�̊J�n�ɐ����������ǂ����������l</returns>
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

        public bool GetVoltageData(out long u_sec, out double voltage)
        {
            u_sec = cnt += 100;
            voltage = 0.0;
            return true;
        }

        /// <summary>
        /// �V���A���|�[�g�����M�����f�[�^���������܂��B
        /// </summary>
        /// <param name="sender">�C�x���g�̑��M���I�u�W�F�N�g</param>
        /// <param name="e">�C�x���g�̃f�[�^</param>
        public void DataReceivedHandler(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort sp = (SerialPort)sender;
            string dataReceived = sp.ReadExisting();
            bufstr = dataReceived;
            OnDataReceived(dataReceived);
            Console.WriteLine(dataReceived);
        }

        /// <summary>
        /// DataReceived�C�x���g�𔭐������郁�\�b�h
        /// </summary>
        /// <param name="data">��M�����f�[�^</param>
        protected virtual void OnDataReceived(string data)
        {
            DataReceived?.Invoke(this, data);
        }

    }
}