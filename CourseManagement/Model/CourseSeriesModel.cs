using LiveCharts;
using System.Collections.ObjectModel;

namespace CourseManagement.Model
{
	public class CourseSeriesModel
	{
		public string CourseName { get; set; }

		public SeriesCollection SeriesCollection { get; set; }
		public ObservableCollection<SeriesModel> SeriesList { get; set; }
	}
}
