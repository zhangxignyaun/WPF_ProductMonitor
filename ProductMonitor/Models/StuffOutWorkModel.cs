using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductMonitor.Models
{
    /// <summary>
    /// 缺岗员工属性
    /// </summary>
     public class StuffOutWorkModel
    {
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name {  get; set; }
        /// <summary>
        /// 职责
        /// </summary>
        public string Duty {  get; set; }
        /// <summary>
        /// 工时
        /// </summary>
        public double ManHour {  get; set; }
    }
}
