using ProductMonitor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ProductMonitor.UserControls
{
    /// <summary>
    /// RadarUC.xaml 的交互逻辑
    /// </summary>
    public partial class RadarUC : UserControl
    {

        public RadarUC()
        {
            InitializeComponent();
            SizeChanged += OnSizeChanged;
            #region 雷达图数据初始化
            //ItemSource = new List<RadarModel>();
            //ItemSource.Add(new RadarModel { Item = "水", Value = 37 });
            //ItemSource.Add(new RadarModel { Item = "木", Value = 24 });
            //ItemSource.Add(new RadarModel { Item = "金", Value = 45 });
            //ItemSource.Add(new RadarModel { Item = "土", Value = 24 });
            //ItemSource.Add(new RadarModel { Item = "火", Value = 67 });
            #endregion
        }

        private void OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            Drag();
        }

        
        private List<RadarModel> _itemSource;
        /// <summary>
        /// 数据绑定
        /// </summary>
        public List<RadarModel> ItemSource
        {
            get { return (List<RadarModel>)GetValue(ItemSourceProperty); }
            set { SetValue(ItemSourceProperty, value); }
        }
        /// <summary>
        /// /依赖属性
        /// </summary>
        public static readonly DependencyProperty ItemSourceProperty = DependencyProperty.Register("ItemSource",typeof(List<RadarModel>),typeof
            (RadarUC));
        /// <summary>
        /// 画图
        /// </summary>
        public void Drag()
        {
            if (ItemSource == null || ItemSource.Count==0)
            {

            }
            //重画前清空画布
            mainCanvas.Children.Clear();
            P1.Points.Clear();
            P2.Points.Clear();
            P3.Points.Clear();
            P4.Points.Clear();
            P5.Points.Clear();

            //调整雷达图边框大小
            double size = Math.Min(RenderSize.Width, RenderSize.Height);
            mainGrid.Width = size; 
            mainGrid.Height = size;
            
            double radius = size / 2;
            //double centerX = RenderSize.Width / 2;
            //double centerY =  RenderSize.Height / 2;
            double step =360.0/ ItemSource.Count;
            double rotation = -90.0;//旋转【偏移量
            for (int i=0;i< ItemSource.Count;i++)
            {
                // 计算当前顶点的角度（弧度），并叠加旋转偏移
                double angle = (i * step + rotation) * Math.PI / 180.0;
                double x =(radius-20) * Math.Cos(angle);//X轴偏移量
                double y = (radius-20) * Math.Sin(angle);//Y轴偏移量

                
                Point p2 = new Point(radius + x * 0.75, radius + y * 0.75);
                Point p3 = new Point(radius + x * 0.5, radius + y * 0.5);
                Point p4 = new Point(radius + x * 0.25, radius + y * 0.25);

                P1.Points.Add(new Point(radius+x, radius + y));
                P2.Points.Add(p2);
                P3.Points.Add(p3);
                P4.Points.Add(p4);


                P5.Points.Add(new Point(radius + x * ItemSource[i].Value * 0.01, radius + y * ItemSource[i].Value * 0.01));

                Polyline polyline = new Polyline();
                Color hexColor = (Color)ColorConverter.ConvertFromString("#11ffffff");
                polyline.Stroke = new SolidColorBrush(hexColor);
                
                //连接顶点
                polyline.Points.Add(new Point(radius, radius));
                polyline.Points.Add(p4);
                polyline.Points.Add(p3);
                polyline.Points.Add(p2);
                polyline.Points.Add(new Point(radius+x, radius+ y));

                TextBlock text = new TextBlock();

                text.Width = 60;
                text.FontSize = 12;
                text.TextAlignment = TextAlignment.Center;
                text.Text = ItemSource[i].Item;
                text.Foreground = new SolidColorBrush(Colors.White);
                text.SetValue(Canvas.LeftProperty, radius + ((radius - 10) * Math.Cos(angle))-30);
                text.SetValue(Canvas.TopProperty, radius + ((radius - 10) * Math.Sin(angle))-10);

                mainCanvas.Children.Add(text);
                mainCanvas.Children.Add(polyline);
            }


        }
    }
}
