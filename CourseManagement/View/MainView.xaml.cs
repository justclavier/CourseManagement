using CourseManagement.Common;
using CourseManagement.ViewModel;
using System.Windows;
using System.Windows.Input;

namespace CourseManagement.View
{
	/// <summary>
	/// MainWindow.xaml 的交互逻辑
	/// </summary>
	public partial class MainView : Window
	{
		public MainView()
		{
			InitializeComponent();

			MainViewModel model = new MainViewModel();
			this.DataContext = model;
			model.UserInfo.Avatar = GlobalValues.UserInfo.Avatar;
			model.UserInfo.UserName = GlobalValues.UserInfo.RealName;
			model.UserInfo.Gender = GlobalValues.UserInfo.Gender;

			this.MaxHeight = SystemParameters.PrimaryScreenHeight;
		}

		private void Border_MouseLeftButtnDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			if (e.LeftButton == MouseButtonState.Pressed)
			{
				this.DragMove();
			}
		}

		private void btnMin_Click(object sender, RoutedEventArgs e)
		{
			this.WindowState = WindowState.Minimized;
		}

		private void btnMax_Click(object sender, RoutedEventArgs e)
		{
			this.WindowState = this.WindowState == WindowState.Maximized ? WindowState.Normal : WindowState.Maximized;
		}

		private void btnClose_Click(object sender, RoutedEventArgs e)
		{
			this.Close();
		}
	}
}
