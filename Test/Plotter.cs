using ScottPlot;
using ScottPlot.Plottable;
using System.Collections.Generic;

namespace Test
{
    /// <summary>
    ///  データのプロットをするクラス
    ///  FormPlot１つに対して割り当てられ、データの受け取り、表示の更新を行う。
    ///  
    /// registerData関数を呼び出して、データの登録、表示の更新を行ってください。
    /// </summary>
    public class Plotter
    {
        public enum AxisScaleMode { ByRange, ByDivSize }


        private FormsPlot formPlot;

        /// <summary>
        ///  縦軸、表示する電圧値のリスト
        /// </summary>
        private readonly List<double> voltageDataList;
        /// <summary>
        ///  横軸、時間(μs)のリスト。 縦軸のリストのインデックスと一致する
        /// </summary>
        private readonly List<double> timeDataList;
        /// <summary>
        ///  スキャッターを操作するクラス。
        ///  １つの画面で複数のプロットをするようになると、やり方が変わるかも
        /// </summary>
        private readonly ScatterPlot scatterPlot;

        /// <summary>
        ///  FormPlotのみ操作するときのコンストラクタ
        /// </summary>
        /// <param name="formPlot"> 操作するFormPlotのインスタンスを渡す </param>
        public Plotter(FormsPlot formPlot)
        {
            this.formPlot = formPlot;
            voltageDataList = new List<double>();
            timeDataList = new List<double>();
            voltageDataList.Add(0.0);
            timeDataList.Add(0.0);
            scatterPlot = formPlot.Plot.AddScatter(timeDataList.ToArray(), voltageDataList.ToArray());
            formPlot.Plot.XLabel("時間(ms)");
            formPlot.Plot.YLabel("電圧(V)");
            formPlot.Refresh();
        }

        /// <summary>
        ///  データの登録、表示の更新を行う関数。
        ///  これを呼ぶと、横・縦軸の幅の更新、表示範囲の更新等、すべてが行われる(ようにする)
        /// </summary>
        /// <param name="u_sec"> 電圧値を読み取った時間(プログラム開始から経過した時間、単位μs) </param>
        /// <param name="voltageData"> 読み取った電圧値。単位はV </param>
        public void registerData(long u_sec, double voltageData)
        {
            voltageDataList.Add(voltageData);
            timeDataList.Add(u_sec * 10e-6);
            scatterPlot.Update(timeDataList.ToArray(), voltageDataList.ToArray());
            formPlot.Plot.AxisAuto();
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