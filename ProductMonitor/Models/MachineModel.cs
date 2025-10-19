using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductMonitor.Models
{
    /// <summary>
    /// 机台数据模型
    /// </summary>
    public class MachineModel
    {
        /// <summary>
        /// 机台名称
        /// </summary>
        public string Name {  get; set; }
        /// <summary>
        /// 机台状态
        /// </summary>
        public string Status {  get; set; }
        /// <summary>
        /// 计划任务数量
        /// </summary>
        public int PlanCount {  get; set; }
        /// <summary>
        /// 已完成任务数量
        /// </summary>
        public int FnishedCount {  get; set; }
        /// <summary>
        /// 工单编号
        /// </summary>
        public string OrderNo {  get; set; }

        public double Percent { get { return FnishedCount * 100.0 / PlanCount; } }

        public string Percenttag { get { return Math.Round(FnishedCount * 100.0 / PlanCount) + "%"; } }
        
    }
}
