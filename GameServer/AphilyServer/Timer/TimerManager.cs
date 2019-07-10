using AphilyServer.Concurrent;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace AphilyServer.Timera
{
    /// <summary>
    /// 定时任务（计时器）管理类
    /// </summary>
    public class TimerManager
    {
        private static TimerManager instance;
        public static TimerManager Instance
        {
            get
            {
                lock (instance)
                {
                    if (instance == null)
                        instance = new TimerManager();
                    return instance;
                }
            }
        }

        /// <summary>
        /// 实现定时器的主要功能就是这个Timer类
        /// </summary>
        Timer timer;

        /// <summary>
        /// 这个字典存储： 任务id 和 任务模型 的映射
        /// </summary>
        private ConcurrentDictionary<int, TimerModel> idModelDict = new ConcurrentDictionary<int, TimerModel>();

        /// <summary>
        /// 要移除的任务ID列表
        /// </summary>
        private List<int> removeList = new List<int>();

        /// <summary>
        /// 用来表示id
        /// </summary>
        private ConcurrentInt id = new ConcurrentInt(-1);

        public TimerManager()
        {
            timer = new Timer(10);
            timer.Elapsed += Timer_Elapsed;
        }

        /// <summary>
        /// 达到时间间隔时候触发
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            lock (removeList)
            {
                TimerModel tmpModel = null;
                foreach (var id in removeList)
                {
                    idModelDict.TryRemove(id, out tmpModel);
                }
                removeList.Clear();
            }

            foreach (var model in idModelDict.Values)
            {
                if (model.Time <= DateTime.Now.Ticks)
                    model.Run();
            }
        }

        /// <summary>
        /// 添加定时任务 指定触发的时间 15点05分
        /// </summary>
        public void AddTimeEvent(DateTime dateTime, TimeDelegate timeDelegate)
        {
            long delayTime = dateTime.Ticks - DateTime.Now.Ticks;
            if (delayTime <= 0)
                return;
            AddTimeEvent(delayTime, timeDelegate);
        }

        /// <summary>
        /// 添加定时任务 指定延迟的时间 40s
        /// </summary>
        /// <param name="dateTime">毫秒！！！</param>
        /// <param name="timeDelegate"></param>
        public void AddTimeEvent(long dateTime, TimeDelegate timeDelegate)
        {
            TimerModel model = new TimerModel(id.Add_Get(), DateTime.Now.Ticks + dateTime, timeDelegate);
            idModelDict.TryAdd(model.Id, model);
        }

    }
}
