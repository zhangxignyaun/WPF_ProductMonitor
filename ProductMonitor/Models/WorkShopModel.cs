﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductMonitor.Models
{
    /// <summary>
    /// 车间数据模型
    /// </summary>
    public class WorkShopModel
    {
        /// <summary>
        /// 车间名称
        /// </summary>
        public string WorkShopName { get; set; }
        /// <summary>
        /// 作业数量
        /// </summary>
        public int  WorkingCount{ get; set; }
        /// <summary>
        /// 等待数量
        /// </summary>
        public int WaitCount { get; set; }
        /// <summary>
        /// 故障数量
        /// </summary>
        public int FaultCount { get; set; }
        /// <summary>
        /// 停机数量
        /// </summary>
        public int StopCount { get; set; }
        /// <summary>
        /// 总数量
        /// </summary>
        public int TotalCount { get { return WorkingCount + WaitCount + FaultCount + StopCount; } }
        
    }
}
