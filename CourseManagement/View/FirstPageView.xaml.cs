using CourseManagement.ViewModel;
using System.Windows.Controls;

namespace CourseManagement.View
{
	/// <summary>
	/// FirstPageView.xaml 的交互逻辑
	/// </summary>
	public partial class FirstPageView : UserControl
	{
		public FirstPageView()
		{
			InitializeComponent();
			this.DataContext = new FirstPageViewModel();
		}
	}
}
