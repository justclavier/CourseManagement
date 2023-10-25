using CourseManagement.View;
using System.Windows;

namespace CourseManagement
{
	/// <summary>
	/// App.xaml 的交互逻辑
	/// </summary>
	public partial class App : Application
	{
		protected override void OnStartup(StartupEventArgs e)
		{
			base.OnStartup(e);

			if (new LoginView().ShowDialog() == true)
			{
				new MainView().ShowDialog();
			}

			Application.Current.Shutdown();
		}
	}
}
