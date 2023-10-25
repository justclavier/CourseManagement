using CourseManagement.ViewModel;
using System.Windows;
using System.Windows.Input;

namespace CourseManagement.View
{
	/// <summary>
	/// LoginView.xaml 的交互逻辑
	/// </summary>
	public partial class LoginView : Window
	{
		public LoginView()
		{
			InitializeComponent();
			this.DataContext = new LoginViewModel();
		}

		private void WinMove_LeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			if (e.LeftButton == MouseButtonState.Pressed)
			{
				this.DragMove();
			}
		}
	}
}
