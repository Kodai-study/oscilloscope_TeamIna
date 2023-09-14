using ScottPlot;
using ScottPlot.Plottable;
using System;
using System.Collections.Generic;

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

        /// <summary>
        ///  �����̏k�ڂ�ݒ肷��Ƃ��̐ݒ���@
        ///  ByRange : �������ׂĂŒl��ݒ肷��(�n�_����I�_�܂ł̎���)
        ///  ByDivSize : 1�ڐ���̎��ԂŒl��ݒ肷��
        /// </summary>
        public enum AxisScaleMode { ByRange, ByDivSize }

        /// <summary>
        ///  �����̕b����ms�̒P�ʂɕϊ�����֐�
        /// </summary>
        private static readonly Func<double, string> msecFormat = (tickValue) =>
        {
            return $"{tickValue * 1e+3:F2}ms";
        };

        /// <summary>
        ///  �����̕b������s�̒P�ʂɕϊ�����֐�
        /// </summary>
        private static readonly Func<double, string> usecFormat = (tickValue) =>
        {
            return $"{tickValue * 1e+6:F2}us";
        };

        /// <summary>
        /// �����̕b����s�̒P�ʂɕϊ�����֐�
        /// </summary>
        private static readonly Func<double, string> secFormat = (tickValue) =>
        {
            return $"{tickValue:F2}s";
        };

        private readonly FormsPlot formPlot;

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

        private double spanTime = 10 * 1e-3;

        /// <summary>
        ///  FormPlot�̂ݑ��삷��Ƃ��̃R���X�g���N�^
        /// </summary>
        /// <param name="formPlot"> ���삷��FormPlot�̃C���X�^���X��n�� </param>
        public Plotter(FormsPlot formPlot)
        {
            this.formPlot = formPlot;
            voltageDataList = new List<double>();
            timeDataList = new List<double>();
            voltageDataList.Add(0.0);
            timeDataList.Add(0.0);
            scatterPlot = formPlot.Plot.AddScatter(timeDataList.ToArray(), voltageDataList.ToArray());
            formPlot.Plot.XLabel("����(ms)");
            formPlot.Plot.YLabel("�d��(V)");
            formPlot.Plot.XAxis.TickLabelFormat(msecFormat);
            formPlot.Refresh();
            voltageDataList.Remove(0.0);
            timeDataList.Remove(0.0);
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
            timeDataList.Add(u_sec * 1e-6);

            if ((u_sec * 1e-6) == 0.0)
            {
                Console.WriteLine("hoge");
            }

            foreach (var e in timeDataList)
            {
                if (e < (u_sec * 1e-6 - 0.1))
                {
                    timeDataList.Remove(e);
                    voltageDataList.Remove(voltageDataList[0]);
                }
                else
                    break;
            }
            scatterPlot.Update(timeDataList.ToArray(), voltageDataList.ToArray());
            formPlot.Plot.AxisAuto();
            formPlot.Refresh();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mode"></param>
        /// <param name="arg1"></param>
        /// <param name="arg2"></param>
        public void SetXAxisScale(AxisScaleMode mode, double arg1, double arg2 = 0.0)
        {
            if (mode == AxisScaleMode.ByDivSize)
            {

            }
            else if (mode == AxisScaleMode.ByRange)
            {

            }
            formPlot.Plot.AxisPan(10, 10);
        }
    }
}