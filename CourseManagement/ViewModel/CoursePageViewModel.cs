using CourseManagement.Common;
using CourseManagement.DataAccess;
using CourseManagement.Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace CourseManagement.ViewModel
{
	public class CoursePageViewModel
	{
		public ObservableCollection<CategoryItemModel> CategoryCourses { get; set; }
		public ObservableCollection<CategoryItemModel> CategoryTechnology { get; set; }
		public ObservableCollection<CategoryItemModel> CategoryTeacher { get; set; }

		public ObservableCollection<CourseModel> CourseList { get; set; } = new ObservableCollection<CourseModel>();

		private List<CourseModel> courseAll;

		public CommandBase OpenCourseUrlCommand { get; set; }
		public CommandBase TeacherFilterCommand { get; set; }

		public CoursePageViewModel()
		{
			this.OpenCourseUrlCommand = new CommandBase();
			this.OpenCourseUrlCommand.DoCanExecute = new System.Func<object, bool>(o => true);
			this.OpenCourseUrlCommand.DoExecute = new System.Action<object>(o => { System.Diagnostics.Process.Start(o.ToString()); });

			this.TeacherFilterCommand = new CommandBase();
			this.TeacherFilterCommand.DoCanExecute = new System.Func<object, bool>(o => true);
			this.TeacherFilterCommand.DoExecute = new System.Action<object>(DoFilter);

			this.InitCategory();
			this.InitCourseList();
		}

		private void DoFilter(object o)
		{
			string teacher = o.ToString();
			List<CourseModel> temp = courseAll;
			if (teacher != "全部")
			{
				temp = courseAll.Where(c => c.Teachers.Exists(e => e == teacher)).ToList();
			}

			CourseList.Clear();
			foreach (var item in temp)
			{
				CourseList.Add(item);
			}
		}

		private void InitCategory()
		{
			this.CategoryCourses = new ObservableCollection<CategoryItemModel>();
			this.CategoryCourses.Add(new CategoryItemModel("全部", true));
			this.CategoryCourses.Add(new CategoryItemModel("公开课"));
			this.CategoryCourses.Add(new CategoryItemModel("VIP课程"));

			this.CategoryTechnology = new ObservableCollection<CategoryItemModel>();
			this.CategoryTechnology.Add(new CategoryItemModel("全部", true));
			this.CategoryTechnology.Add(new CategoryItemModel("C#"));
			this.CategoryTechnology.Add(new CategoryItemModel("ASP.NET"));
			this.CategoryTechnology.Add(new CategoryItemModel("微服务"));
			this.CategoryTechnology.Add(new CategoryItemModel("Java"));

			this.CategoryTeacher = new ObservableCollection<CategoryItemModel>();
			this.CategoryTeacher.Add(new CategoryItemModel("全部", true));
			foreach (var item in LocalDataAccess.GetInstance().GetTeachers())
			{
				this.CategoryTeacher.Add(new CategoryItemModel(item));
			}
		}

		private void InitCourseList()
		{
			for (int i = 0; i < 10; i++)
			{
				CourseList.Add(new CourseModel { IsShowSkeleton = true });
			}
			Task.Run(new System.Action(async () =>
			{
				courseAll = LocalDataAccess.GetInstance().GetCourses();
				await Task.Delay(4000);
				Application.Current.Dispatcher.Invoke(new System.Action(() =>
				{
					CourseList.Clear();
					foreach (var item in courseAll)
					{
						CourseList.Add(item);
					}
				}));

			}));
		}
	}
}
