using CourseManagement.Common;
using CourseManagement.Model;
using System;
using System.Reflection;
using System.Windows;

namespace CourseManagement.ViewModel
{
	public class MainViewModel : NotifyBase
	{
		public UserModel UserInfo { get; set; }

		private int _searchText;

		public int SearchText
		{
			get { return _searchText; }
			set { _searchText = value; this.DoNotify(); }
		}

		private FrameworkElement _mainContent;

		public FrameworkElement MainContent
		{
			get { return _mainContent; }
			set { _mainContent = value; this.DoNotify(); }
		}

		public CommandBase NavChangedCommand { get; set; }

		public MainViewModel()
		{
			UserInfo = new UserModel();
			this.NavChangedCommand = new CommandBase();
			this.NavChangedCommand.DoExecute = new Action<object>(DoNavChanged);
			this.NavChangedCommand.DoCanExecute = new Func<object, bool>((o) => true);
		}

		private void DoNavChanged(object obj)
		{
			Type type = Type.GetType("CourseManagement.View." + obj.ToString());
			ConstructorInfo cti = type.GetConstructor(Type.EmptyTypes);
			this.MainContent = (FrameworkElement)cti.Invoke(null);
		}
	}
}
