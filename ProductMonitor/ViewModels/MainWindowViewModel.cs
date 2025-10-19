using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using ProductMonitor.Models;
using ProductMonitor.UserControls;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace ProductMonitor.ViewModels
{
    internal class MainWindowViewModel:INotifyPropertyChanged
    {
        private readonly DispatcherTimer _timer = new DispatcherTimer();//初始化定时器
        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public MainWindowViewModel() {
            #region 环境监控信息初始化
            EnviromentList = new List<EnviromentModel>();
            EnviromentList.Add(new EnviromentModel{EnItemName="光照(Lux)",EnValue=222 });
            EnviromentList.Add(new EnviromentModel{EnItemName="噪音(db)",EnValue=23 });
            EnviromentList.Add(new EnviromentModel{EnItemName="温度(℃)",EnValue=56 });
            EnviromentList.Add(new EnviromentModel{EnItemName="湿度(%)",EnValue=43 });
            EnviromentList.Add(new EnviromentModel{EnItemName="PM2.5(m³)",EnValue=20 });
            EnviromentList.Add(new EnviromentModel{EnItemName="硫化氢(PPM)",EnValue=15 });
            EnviromentList.Add(new EnviromentModel{EnItemName="氮气(PPM)",EnValue=17 });
            #endregion

            #region 报警信息初始化
            AlarmList = new List<AlarmModel>();
            AlarmList.Add(new AlarmModel { Num="000001", Msg="出现未知错误", Time="2025-10-11 14:34", Duration=6} );
            AlarmList.Add(new AlarmModel { Num="000002", Msg="转速过快", Time="2025-10-11 14:34", Duration=3} );
            AlarmList.Add(new AlarmModel { Num="000003", Msg="温度过高", Time="2025-10-11 14:34", Duration=4} );
            AlarmList.Add(new AlarmModel { Num="000004", Msg="错误停止", Time="2025-10-11 14:34", Duration=52} );
            #endregion

            #region 饼状图信息初始化
            // 定义饼图数据（名称+值）
            PieSeries = new SeriesCollection
        {
            new PieSeries
            {
                Title = "产品A",
                Values = new ChartValues<ObservableValue> { new ObservableValue(40) }, // 占比40%
                Fill = System.Windows.Media.Brushes.Orange
            },
            new PieSeries
            {
                Title = "产品B",
                Values = new ChartValues<ObservableValue> { new ObservableValue(30) }, // 占比30%
                Fill = System.Windows.Media.Brushes.Green
            },
            new PieSeries
            {
                Title = "产品C",
                Values = new ChartValues<ObservableValue> { new ObservableValue(30) }, // 占比30%
                Fill = System.Windows.Media.Brushes.Blue
            }
        };
            #endregion

            #region 设备属性初始化
            DeviceModelList = new List<DeviceModel>();
            DeviceModelList.Add(new DeviceModel { DeviceItem = "电能(Kw.h)", Value = 60.8 });
            DeviceModelList.Add(new DeviceModel { DeviceItem = "电压(V)", Value = 346 });
            DeviceModelList.Add(new DeviceModel { DeviceItem = "电流(A)", Value = 6 });
            DeviceModelList.Add(new DeviceModel { DeviceItem = "压差(Kpa)", Value = 16 });
            DeviceModelList.Add(new DeviceModel { DeviceItem = "温度(℃)", Value = 26 });
            DeviceModelList.Add(new DeviceModel { DeviceItem = "振动(mm/s)", Value = 36 });

            #endregion

            #region 雷达图数据初始化
            RadarList = new List<RadarModel>();
            RadarList.Add(new RadarModel { Item = "水", Value = 37 });
            RadarList.Add(new RadarModel { Item = "木", Value = 24 });
            RadarList.Add(new RadarModel { Item = "金", Value = 45 });
            RadarList.Add(new RadarModel { Item = "土", Value = 24 });
            RadarList.Add(new RadarModel { Item = "火", Value = 67 });
            #endregion

            #region 初始化人员缺岗信息
            StuffOutWorkList = new List<StuffOutWorkModel>();
            StuffOutWorkList.Add(new StuffOutWorkModel { Name="张三",Duty="技术员",ManHour=32});
            StuffOutWorkList.Add(new StuffOutWorkModel { Name="李四",Duty="车工",ManHour=23});
            StuffOutWorkList.Add(new StuffOutWorkModel { Name="王五",Duty="车工",ManHour=34});
            StuffOutWorkList.Add(new StuffOutWorkModel { Name="小明",Duty="技术员",ManHour=12});
            StuffOutWorkList.Add(new StuffOutWorkModel { Name="小红",Duty="统计员",ManHour=34});
            #endregion

            #region 初始化车间列表
            WorkShopList = new List<WorkShopModel>();
            WorkShopList.Add(new WorkShopModel { WorkShopName="装凯车间",WorkingCount=10,WaitCount=3,FaultCount=1,StopCount=6});
            WorkShopList.Add(new WorkShopModel { WorkShopName="贴片车间",WorkingCount=5,WaitCount=3,FaultCount=1,StopCount=2});
            WorkShopList.Add(new WorkShopModel { WorkShopName="封装车间",WorkingCount=6,WaitCount=2,FaultCount=2,StopCount=1});
            WorkShopList.Add(new WorkShopModel { WorkShopName="挤压车间",WorkingCount=6,WaitCount=2,FaultCount=2,StopCount=1});
            #endregion

            #region 初始化机台数据
            MachineList = new List<MachineModel>();
            Random random = new Random();
            for (int i=0; i<20;i++)
            {
                int plan = random.Next(100,1000);
                int finish = random.Next(0,plan);
                MachineList.Add(new MachineModel
                {
                    Name = "焊接机-"+(i+1),
                    FnishedCount = finish,
                    PlanCount = plan,
                    Status="作业中",
                    OrderNo="H2025101700123"
                });
            }
            MachineList.Add(new MachineModel
            {
                Name = "焊接机-" + (26),
                FnishedCount = 22,
                PlanCount = 22,
                Status = "作业中",
                OrderNo = "H2025101700123"
            });
            #endregion
            //配置定时器（每秒触发一次）
            _timer.Interval = TimeSpan.FromSeconds(1);
            _timer.Tick += (s, e) =>
            {
                TimeStr = DateTime.Now.ToString("HH:mm");
                DateStr = DateTime.Now.ToString("yyyy-MM-dd");
                WeekStr = DateTime.Now.ToString("dddd", new CultureInfo("zh-CN"));
            };
            _timer.Start();


        }
        /// <summary>
        /// 监控用户控件
        /// </summary>
        private UserControl _MonitorUC;

		public UserControl MonitorUC
        {
			get {
                if (_MonitorUC == null)
                {
                   _MonitorUC = new MonitorUC();
                }
                return _MonitorUC;
            }
			set { 
                _MonitorUC = value;
                OnPropertyChanged();
            }
		}

        #region 主界面获取系统时间

        /// <summary>
        ///小时：分
        /// </summary>
        private String _timeStr;

        public String TimeStr
        {
            get { return _timeStr; }
            set {
                _timeStr = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        ///年月日
        /// </summary>
        private String _dateStr;

        public String DateStr
        {
            get { return _dateStr; }
            set
            {
                _dateStr = value;
                OnPropertyChanged();
            }
        }
        /// <summary>
        ///星期
        /// </summary>
        private String _weekStr;

        public String WeekStr
        {
            get { return _weekStr; }
            set
            {
                _weekStr = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region 获取数量
        /// <summary>
        /// 机台总数
        /// </summary>
        private String _machineCount ="0123";
        /// <summary>
        /// 机台总数
        /// </summary>
        public String MachineCount
        {
            get { return _machineCount; }
            set {
                _machineCount = value; 
                OnPropertyChanged();
            }
        }


        /// <summary>
        /// 生产计数
        /// </summary>
        private String _productCount = "1278";
        /// <summary>
        /// 生产计数
        /// </summary>
        public String ProductCount
        {
            get { return _productCount; }
            set
            {
                _productCount = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 不良计数
        /// </summary>
        private String _badCount = "0034";
        /// <summary>
        /// 不良计数
        /// </summary>
        public String BadCount
        {
            get { return _badCount; }
            set
            {
                _badCount = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region 获取环境信息
        private List<EnviromentModel> _enviromentList;

        public List<EnviromentModel> EnviromentList
        {
            get {
                
                return _enviromentList; 
            }
            set {
                _enviromentList = value; 
                OnPropertyChanged();
            }
        }



        #endregion

        #region 报警信息
        private List<AlarmModel> _alarmList;

        public List<AlarmModel> AlarmList
        {
            get { return _alarmList; }
            set { 
                _alarmList = value; 
                OnPropertyChanged();
            }
        }

        #endregion

        #region 饼状图信息
        private SeriesCollection _pieSeries;

        public SeriesCollection PieSeries
        {
            get { return _pieSeries; }
            set { _pieSeries = value; 
                OnPropertyChanged();
                
            }
        }

        #endregion
        #region 设备数据
        private List<DeviceModel> _deviceModelList;

        public List<DeviceModel> DeviceModelList
        {
            get { return _deviceModelList; }
            set {
                _deviceModelList = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region 雷达
        private List<RadarModel> _radarList;

        public List<RadarModel> RadarList
        {
            get { return _radarList; }
            set { _radarList = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region 缺岗员工属性
        private List<StuffOutWorkModel> _stuffOutWorkList;

        public List<StuffOutWorkModel> StuffOutWorkList
        {
            get { return _stuffOutWorkList; }
            set { _stuffOutWorkList = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region 车间属性
        private List<WorkShopModel> _workShopList;

        public List<WorkShopModel> WorkShopList
        {
            get { return _workShopList; }
            set { _workShopList = value; 
                OnPropertyChanged();
            }
        }

        #endregion

        #region 机台属性
        private List<MachineModel> _machineList;

        public List<MachineModel> MachineList
        {
            get { return _machineList; }
            set { _machineList = value; 
                OnPropertyChanged();
            }
        }

        #endregion
    }
}
