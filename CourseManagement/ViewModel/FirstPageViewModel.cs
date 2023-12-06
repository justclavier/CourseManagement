using CourseManagement.Common;
using CourseManagement.DataAccess;
using CourseManagement.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace CourseManagement.ViewModel
{
	public class FirstPageViewModel : NotifyBase
	{
		private int _instrumentValue;

		public int InstrumentValue
		{
			get { return _instrumentValue; }
			set { _instrumentValue = value; this.DoNotify(); }
		}

		private int _itemCount;

		public int ItemCount
		{
			get { return _itemCount; }
			set { _itemCount = value; this.DoNotify(); }
		}


		public ObservableCollection<CourseSeriesModel> CourseSeriesList { get; set; } = new ObservableCollection<CourseSeriesModel>();

		Random random = new Random();
		bool taskSwitch = true;
		List<Task> taskList = new List<Task>();

		public FirstPageViewModel()
		{
			this.RefreshInstrumentValue();
			this.InitCourseSeries();
		}

		private void InitCourseSeries()
		{
			//CourseSeriesList.Add(new CourseSeriesModel
			//{
			//	CourseName = "Java高级实战VIP班",
			//	SeriesCollection = new LiveCharts.SeriesCollection {
			//		new PieSeries {
			//		Title = "zhaoxi",
			//		Values = new ChartValues<ObservableValue>{new ObservableValue(123)},
			//		DataLabels=false
			//	},
			//	new PieSeries {
			//		Title = "zhaoxi",
			//		Values = new ChartValues<ObservableValue>{new ObservableValue(123)},
			//		DataLabels=false
			//	}},
			//	SeriesList = new ObservableCollection<SeriesModel>
			//	{
			//		new SeriesModel{SeriesName="b站",CurrentValue=467,IsGrowing=false,ChangeRate=75},
			//		new SeriesModel{SeriesName="b站",CurrentValue=467,IsGrowing=true,ChangeRate=75},
			//		new SeriesModel{SeriesName="b站",CurrentValue=467,IsGrowing=false,ChangeRate=75},
			//		new SeriesModel{SeriesName="b站",CurrentValue=467,IsGrowing=false,ChangeRate=75},
			//		new SeriesModel{SeriesName="b站",CurrentValue=467,IsGrowing=true,ChangeRate=75}
			//	}
			//});
			//CourseSeriesList.Add(new CourseSeriesModel
			//{
			//	CourseName = "Java高级实战VIP班",
			//	SeriesCollection = new LiveCharts.SeriesCollection {
			//		new PieSeries {
			//		Title = "zhaoxi",
			//		Values = new ChartValues<ObservableValue>{new ObservableValue(123)},
			//		DataLabels=false
			//	},
			//	new PieSeries {
			//		Title = "zhaoxi",
			//		Values = new ChartValues<ObservableValue>{new ObservableValue(123)},
			//		DataLabels=false
			//	}},
			//	SeriesList = new ObservableCollection<SeriesModel>
			//	{
			//		new SeriesModel{SeriesName="b站",CurrentValue=467,IsGrowing=false,ChangeRate=75},
			//		new SeriesModel{SeriesName="b站",CurrentValue=467,IsGrowing=true,ChangeRate=75},
			//		new SeriesModel{SeriesName="b站",CurrentValue=467,IsGrowing=false,ChangeRate=75},
			//		new SeriesModel{SeriesName="b站",CurrentValue=467,IsGrowing=false,ChangeRate=75},
			//		new SeriesModel{SeriesName="b站",CurrentValue=467,IsGrowing=true,ChangeRate=75}
			//	}
			//});
			//CourseSeriesList.Add(new CourseSeriesModel
			//{
			//	CourseName = "Java高级实战VIP班",
			//	SeriesCollection = new LiveCharts.SeriesCollection {
			//		new PieSeries {
			//		Title = "zhaoxi",
			//		Values = new ChartValues<ObservableValue>{new ObservableValue(123)},
			//		DataLabels=false
			//	},
			//	new PieSeries {
			//		Title = "zhaoxi",
			//		Values = new ChartValues<ObservableValue>{new ObservableValue(123)},
			//		DataLabels=false
			//	}},
			//	SeriesList = new ObservableCollection<SeriesModel>
			//	{
			//		new SeriesModel{SeriesName="b站",CurrentValue=467,IsGrowing=false,ChangeRate=75},
			//		new SeriesModel{SeriesName="b站",CurrentValue=467,IsGrowing=true,ChangeRate=75},
			//		new SeriesModel{SeriesName="b站",CurrentValue=467,IsGrowing=false,ChangeRate=75},
			//		new SeriesModel{SeriesName="b站",CurrentValue=467,IsGrowing=false,ChangeRate=75},
			//		new SeriesModel{SeriesName="b站",CurrentValue=467,IsGrowing=true,ChangeRate=75}
			//	}
			//});

			var cList = LocalDataAccess.GetInstance().GetCoursePlayRecord();
			this.ItemCount = cList.Max(c => c.SeriesList.Count);
			foreach (var item in cList)
			{
				this.CourseSeriesList.Add(item);
			}
		}

		private void RefreshInstrumentValue()
		{
			var task = Task.Factory.StartNew(new Action(async () =>
			{
				while (taskSwitch)
				{
					InstrumentValue = random.Next(Math.Max(this.InstrumentValue - 5, -10), Math.Min(this.InstrumentValue + 5, 90));
					await Task.Delay(1000);
				}
			}));
			taskList.Add(task);
		}

		public void Dispose()
		{
			try
			{
				taskSwitch = false;
				Task.WaitAll(this.taskList.ToArray());
			}
			catch { }
		}
	}
}
