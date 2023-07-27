using ScottPlot;
using ScottPlot.Plottable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Test
{
    /// <summary>
    ///  �f�[�^�̃v���b�g������N���X
    ///  FormPlot�P�ɑ΂��Ċ��蓖�Ă��A�f�[�^�̎󂯎��A�\���̍X�V���s���B
    ///  
    /// registerData�֐����Ăяo���āA�f�[�^�̓o�^�A�\���̍X�V���s���Ă��������B
    /// </summary>
    public class Plotter
    {
        private FormsPlot formPlot;

        /// <summary>
        ///  �c���A�\������d���l�̃��X�g
        /// </summary>
        private readonly List<double> voltageDataList;
        /// <summary>
        ///  �����A����(��s)�̃��X�g�B �c���̃��X�g�̃C���f�b�N�X�ƈ�v����
        /// </summary>
        private readonly List<double> timeDataList;
        /// <summary>
        ///  �X�L���b�^�[�𑀍삷��N���X�B
        ///  �P�̉�ʂŕ����̃v���b�g������悤�ɂȂ�ƁA�������ς�邩��
        /// </summary>
        private readonly ScatterPlot scatterPlot;
     
        /// <summary>
        ///  FormPlot�̂ݑ��삷��Ƃ��̃R���X�g���N�^
        /// </summary>
        /// <param name="formPlot"> ���삷��FormPlot�̃C���X�^���X��n�� </param>
        public Plotter(FormsPlot formPlot)
        {
            this.formPlot = formPlot;
            voltageDataList = new List<double>();
            timeDataList = new List<double>();
            scatterPlot = formPlot.Plot.AddScatter(timeDataList.ToArray(), voltageDataList.ToArray());
        }

        /// <summary>
        ///  �f�[�^�̓o�^�A�\���̍X�V���s���֐��B
        ///  ������ĂԂƁA���E�c���̕��̍X�V�A�\���͈͂̍X�V���A���ׂĂ��s����(�悤�ɂ���)
        /// </summary>
        /// <param name="u_sec"> �d���l��ǂݎ��������(�v���O�����J�n����o�߂������ԁA�P�ʃ�s) </param>
        /// <param name="voltageData"> �ǂݎ�����d���l�B�P�ʂ�V </param>
        public void registerData(long u_sec, double voltageData)
        {
            voltageDataList.Add(voltageData);
            timeDataList.Add(u_sec * 10e-6);
            scatterPlot.Update(timeDataList.ToArray(), voltageDataList.ToArray());
            formPlot.Plot.AxisAuto(); // �����I�Ɏ��͈̔͂𒲐�����J�[
            // label���̕�����ς�����
        }
    }
}