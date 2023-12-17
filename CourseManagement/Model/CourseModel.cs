using System.Collections.Generic;

namespace CourseManagement.Model
{
	public class CourseModel
	{
		public string CourseName { get; set; }
		public string Cover { get; set; }
		public string Url { get; set; }
		public string Description { get; set; }
		public List<string> Teachers { get; set; }
		public bool IsShowSkeleton { get; set; }
	}
}
