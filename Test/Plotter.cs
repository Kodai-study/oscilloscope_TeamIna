using ScottPlot;
using ScottPlot.Plottable;
using System;
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

        /// <summary>
        ///  横軸の縮尺を設定するときの設定方法
        ///  ByRange : 横軸すべてで値を設定する(始点から終点までの時間)
        ///  ByDivSize : 1目盛りの時間で値を設定する
        /// </summary>
        public enum AxisScaleMode { ByRange, ByDivSize }

        /// <summary>
        ///  横軸の秒数をmsの単位に変換する関数
        /// </summary>
        private static readonly Func<double, string> msecFormat = (tickValue) =>
        {
            return $"{tickValue * 1e+3:F2}ms";
        };

        /// <summary>
        ///  横軸の秒数をμsの単位に変換する関数
        /// </summary>
        private static readonly Func<double, string> usecFormat = (tickValue) =>
        {
            return $"{tickValue * 1e+6:F2}us";
        };

        /// <summary>
        /// 横軸の秒数をsの単位に変換する関数
        /// </summary>
        private static readonly Func<double, string> secFormat = (tickValue) =>
        {
            return $"{tickValue:F2}s";
        };

        private readonly FormsPlot formPlot;

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

        private double spanTime = 10 * 1e-3;

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
            formPlot.Plot.XAxis.TickLabelFormat(msecFormat);
            formPlot.Refresh();
            voltageDataList.Remove(0.0);
            timeDataList.Remove(0.0);
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