﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ProductMonitor.OpCommand
{
    public  class Command:ICommand
    {
        /// <summary>
        /// 定义委托
        /// </summary>
        private Action _action;
        public Command(Action action) 
        {
            _action = action;
        }

        public event EventHandler? CanExecuteChanged;
        /// <summary>
        /// 是否执行
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public bool CanExecute(object? parameter)
        {
            return true;
        }
        /// <summary>
        /// 执行方法
        /// </summary>
        /// <param name="parameter"></param>
        public void Execute(object? parameter)
        {
            if (_action !=null)
            {
                _action();
            }
        }
    }
}
