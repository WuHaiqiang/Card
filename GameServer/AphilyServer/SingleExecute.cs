﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AphilyServer
{
    /// <summary>
    /// 一个需要执行的方法
    /// </summary>
    public delegate void ExecuteDelegate();

    /// <summary>
    /// 单线程池
    /// </summary>
    public class SingleExecute
    {
        private static SingleExecute instance = null;
        public static SingleExecute Instance
        {
            get
            {
                lock (str)
                {
                    if (instance == null)
                        instance = new SingleExecute();
                    return instance;
                }
            }
        }

        private static object o;
        private static string str = "lock";

        /// <summary>
        /// 互斥锁
        /// </summary>
        public Mutex mutex;

        private SingleExecute()
        {
            mutex = new Mutex();
        }

        /// <summary>
        /// 单线程处理逻辑
        /// </summary>
        /// <param name="executeDelegate"></param>
        public void Execute(ExecuteDelegate executeDelegate)
        {
            lock (this)
            {
                mutex.WaitOne();
                executeDelegate();
                mutex.ReleaseMutex();
            }
        }
    }
}
