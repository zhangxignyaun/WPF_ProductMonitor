using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace ProductMonitor.CustomControls
{
    /// <summary>
    /// 按照步骤 1a 或 1b 操作，然后执行步骤 2 以在 XAML 文件中使用此自定义控件。
    ///
    /// 步骤 1a) 在当前项目中存在的 XAML 文件中使用该自定义控件。
    /// 将此 XmlNamespace 特性添加到要使用该特性的标记文件的根
    /// 元素中:
    ///
    ///     xmlns:MyNamespace="clr-namespace:ProductMonitor.CustomControls"
    ///
    ///
    /// 步骤 1b) 在其他项目中存在的 XAML 文件中使用该自定义控件。
    /// 将此 XmlNamespace 特性添加到要使用该特性的标记文件的根
    /// 元素中:
    ///
    ///     xmlns:MyNamespace="clr-namespace:ProductMonitor.CustomControls;assembly=ProductMonitor.CustomControls"
    ///
    /// 您还需要添加一个从 XAML 文件所在的项目到此项目的项目引用，
    /// 并重新生成以避免编译错误:
    ///
    ///     在解决方案资源管理器中右击目标项目，然后依次单击
    ///     “添加引用”->“项目”->[浏览查找并选择此项目]
    ///
    ///
    /// 步骤 2)
    /// 继续操作并在 XAML 文件中使用控件。
    ///
    ///     <MyNamespace:CircleProgressBar/>
    ///
    /// </summary>
    public class CircleProgressBar:Control
    {
        public CircleProgressBar()
        {
            
        }

      

        // 依赖属性：进度百分比（0-100）
        public double Progress
        {
            get { return (double)GetValue(ProgressProperty); }
            set { SetValue(ProgressProperty, value); }
        }
        public static readonly DependencyProperty ProgressProperty =
            DependencyProperty.Register("Progress", typeof(double), typeof(CircleProgressBar),
                new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.AffectsRender));

        // 依赖属性：圆环宽度
        public double RingThickness
        {
            get { return (double)GetValue(RingThicknessProperty); }
            set { SetValue(RingThicknessProperty, value); }
        }
        public static readonly DependencyProperty RingThicknessProperty =
            DependencyProperty.Register("RingThickness", typeof(double), typeof(CircleProgressBar),
                new FrameworkPropertyMetadata(10.0, FrameworkPropertyMetadataOptions.AffectsRender));

        // 依赖属性：背景圆环颜色
        public Brush RingBackground
        {
            get { return (Brush)GetValue(RingBackgroundProperty); }
            set { SetValue(RingBackgroundProperty, value); }
        }
        public static readonly DependencyProperty RingBackgroundProperty =
            DependencyProperty.Register("RingBackground", typeof(Brush), typeof(CircleProgressBar),
                new FrameworkPropertyMetadata(Brushes.LightGray, FrameworkPropertyMetadataOptions.AffectsRender));

        // 依赖属性：进度圆环颜色
        public Brush RingForeground
        {
            get { return (Brush)GetValue(RingForegroundProperty); }
            set { SetValue(RingForegroundProperty, value); }
        }
        public static readonly DependencyProperty RingForegroundProperty =
            DependencyProperty.Register("RingForeground", typeof(Brush), typeof(CircleProgressBar),
                new FrameworkPropertyMetadata(Brushes.Blue, FrameworkPropertyMetadataOptions.AffectsRender));

        static CircleProgressBar()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CircleProgressBar),
                new FrameworkPropertyMetadata(typeof(CircleProgressBar)));
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);

            // 获取控件尺寸（取宽高最小值，确保为圆形）
            double size = Math.Min(ActualWidth, ActualHeight);
            if (size <= 0) return;

            // 计算圆环绘制区域（考虑圆环厚度）
            double innerSize = size - RingThickness;
            double radius = innerSize / 2;
            //Rect outerRect = new Rect(RingThickness / 2, RingThickness / 2, innerSize, innerSize);
            //圆心坐标
            //Point center =new Point(outerRect.X+ outerRect.Width/2, outerRect.Y + outerRect.Height/2);
            Point center = new Point( ActualWidth / 2, ActualHeight / 2);
            // 绘制背景圆环（完整圆环）
            var backgroundPen = new Pen(RingBackground, RingThickness);
            drawingContext.DrawEllipse(null, backgroundPen, center, radius, radius);

            // 绘制进度圆环（按百分比绘制圆弧）
            if (Progress > 0)
            {
                double angle = 360 * (Progress / 100); // 进度对应的角度（0-360°）
                var foregroundPen = new Pen(RingForeground, RingThickness)
                {
                    StartLineCap = PenLineCap.Round, // 圆弧起点圆角
                    EndLineCap = PenLineCap.Round      // 圆弧终点圆角
                };

                // 计算圆弧路径（从顶部12点方向开始，顺时针绘制）
                PathGeometry pathGeometry = new PathGeometry();
                PathFigure pathFigure = new PathFigure
                {
                    StartPoint = new Point(center.X, center.Y - radius), //new Point(center.X, outerRect.Top), // 起点：顶部中点
                    IsClosed = false
                };

                // 添加圆弧段（参数：终点、半径、旋转角度、是否大于半圆、顺时针方向）
                pathFigure.Segments.Add(new ArcSegment(
                    GetArcEndPoint(center, radius, angle),
                    new Size(radius, radius),
                    0, angle > 180, SweepDirection.Clockwise, true));

                pathGeometry.Figures.Add(pathFigure);
                drawingContext.DrawGeometry(null, foregroundPen, pathGeometry);
            }
        }

        // 计算圆弧终点坐标
        private Point GetArcEndPoint(Point center, double radius, double angleDegrees)
        {
            double angleRadians = angleDegrees * Math.PI / 180;
            double x = center.X + radius * Math.Sin(angleRadians); // 注意：WPF坐标系Y轴向下，需调整三角函数
            double y = center.Y - radius * Math.Cos(angleRadians);
            return new Point(x, y);
        }
    }
}
