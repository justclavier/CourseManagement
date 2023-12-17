using CourseManagement.Model;
using CourseManagement.ViewModel;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Data;

namespace CourseManagement.View
{
	/// <summary>
	/// CoursePageView.xaml 的交互逻辑
	/// </summary>
	public partial class CoursePageView : UserControl
	{
		public CoursePageView()
		{
			InitializeComponent();
			this.DataContext = new CoursePageViewModel();
		}

		private void RadioButton_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			RadioButton button = sender as RadioButton;
			string teacher = button.Content.ToString();

			ICollectionView view = CollectionViewSource.GetDefaultView(this.icCourses.ItemsSource);
			if (teacher == "全部")
			{
				view.Filter = null;
				//view.SortDescriptions.Add(new SortDescription("CourseName", ListSortDirection.Descending));
			}
			else
			{
				view.Filter = new System.Predicate<object>(o =>
				{
					return (o as CourseModel).Teachers.Exists(t => t == teacher);
				});
			}
		}
	}
}
