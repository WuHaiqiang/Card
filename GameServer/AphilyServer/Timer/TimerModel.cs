using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AphilyServer.Timera
{
    /// <summary>
    /// 当定时器达到时间后触发
    /// </summary>
    public delegate void TimeDelegate();

    /// <summary>
    /// 定时器任务的数据模型
    /// </summary>
    public class TimerModel
    {
        public int Id;

        /// <summary>
        /// 任务执行的时间
        /// </summary>
        public long Time;

        private TimeDelegate timeDelegate;

        public TimerModel(int id, long time, TimeDelegate td)
        {
            this.Id = id;
            this.Time = time;
            this.timeDelegate = td;
        }

        public void Run()
        {
            timeDelegate();
        }
    }
}
