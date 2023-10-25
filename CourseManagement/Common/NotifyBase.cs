﻿using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace CourseManagement.Common
{
	public class NotifyBase : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		public void DoNotify([CallerMemberName] string propName = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
		}
	}
}
