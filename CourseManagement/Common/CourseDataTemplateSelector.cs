using CourseManagement.Model;
using System.Windows;
using System.Windows.Controls;

namespace CourseManagement.Common
{
	public class CourseDataTemplateSelector : DataTemplateSelector
	{
		public DataTemplate DefaultTemplate { get; set; }
		public DataTemplate SkeletonTemplate { get; set; }

		public override DataTemplate SelectTemplate(object item, DependencyObject container)
		{
			if ((item as CourseModel).IsShowSkeleton)
			{
				return SkeletonTemplate;
			}
			return DefaultTemplate;
		}
	}
}
