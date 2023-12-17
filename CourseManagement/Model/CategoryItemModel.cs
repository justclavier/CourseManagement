namespace CourseManagement.Model
{
	public class CategoryItemModel
	{
		public CategoryItemModel()
		{

		}
		public CategoryItemModel(string name, bool state = false)
		{
			this.CategoryName = name;
			this.IsSelected = state;
		}
		public bool IsSelected { get; set; }
		public string CategoryName { get; set; }
	}
}
