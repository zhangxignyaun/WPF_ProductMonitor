using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductMonitor.Models
{
    /// <summary>
    /// 环境信息
    /// </summary>
   public class EnviromentModel
    {
        /// <summary>
        /// 环境信息名称
        /// </summary>
        public string EnItemName {  get; set; }
        /// <summary>
        /// 环境信息数值
        /// </summary>
        public double EnValue { get; set; }
    }
}
