using ProductMonitor.OpCommand;
using ProductMonitor.UserControls;
using ProductMonitor.ViewModels;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace ProductMonitor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        MainWindowViewModel mainWindowViewModel = new MainWindowViewModel();
        public MainWindow()
        {
            InitializeComponent();
            
            this.DataContext = mainWindowViewModel;
            
        }

        #region 窗口操作
        private void Btn_Min(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void Btn_Max(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == WindowState.Normal)
            {
                this.WindowState = System.Windows.WindowState.Maximized;
            }
            else
            {
                this.WindowState = System.Windows.WindowState.Normal;
            }
        }

        private void Btn_Close(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }
        #endregion

        public void ShowWorkShopUC()
        {
            WorkShopUC workShopUC = new WorkShopUC();
            mainWindowViewModel.MonitorUC = workShopUC;
            //厚度动画
            LodingAnimation(workShopUC);

        }
        public Command ShowWorkShopUCCommand { get { return new Command(ShowWorkShopUC); } }

        public void GoBackMonitor()
        {
            MonitorUC monitorUC = new MonitorUC();
            mainWindowViewModel.MonitorUC = monitorUC;
            //厚度动画
            LodingAnimation(monitorUC);
        }
        /// <summary>
        /// 返回命令
        /// </summary>
        public Command GoBackMonitorCommand { get { return new Command(GoBackMonitor); } }

        /// <summary>
        /// 页面跳转缓冲动画
        /// </summary>
        /// <param name="obj"></param>
        public  void LodingAnimation(DependencyObject obj)
        {
            ThicknessAnimation thicknessAnimation = new ThicknessAnimation(
                new Thickness(0, 50, 0, -50), new Thickness(0, 0, 0, 0), new TimeSpan(0, 0, 0, 0, 500)
                );
            //透明度
            DoubleAnimation doubleAnimation = new DoubleAnimation(0, 1, new TimeSpan(0, 0, 0, 0, 500));

            Storyboard.SetTarget(thicknessAnimation, obj);
            Storyboard.SetTarget(doubleAnimation, obj);
            Storyboard.SetTargetProperty(thicknessAnimation, new PropertyPath("Margin"));
            Storyboard.SetTargetProperty(doubleAnimation, new PropertyPath("Opacity"));

            Storyboard storyboard = new Storyboard();
            storyboard.Children.Add(thicknessAnimation);
            storyboard.Children.Add(doubleAnimation);
            storyboard.Begin();
        }
    }
}