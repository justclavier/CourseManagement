using System.Windows;
using System.Windows.Controls;

namespace CourseManagement.Common
{
	internal class PasswordHelper
	{
		public static readonly DependencyProperty PasswordProperty = DependencyProperty.RegisterAttached("Password", typeof(string), typeof(PasswordHelper),
			  new FrameworkPropertyMetadata("", new PropertyChangedCallback(OnPropertyChanged)));
		public static string GetPassword(DependencyObject obj)
		{
			return obj.GetValue(PasswordProperty).ToString();
		}
		public static void SetPassword(DependencyObject obj, string value)
		{
			obj.SetValue(PasswordProperty, value);
		}

		public static readonly DependencyProperty AttachProperty = DependencyProperty.RegisterAttached("Attach", typeof(bool), typeof(PasswordHelper),
			  new FrameworkPropertyMetadata(default(bool), new PropertyChangedCallback(OnAttached)));
		public static bool GetAttach(DependencyObject obj)
		{
			return (bool)obj.GetValue(AttachProperty);
		}
		public static void SetAttach(DependencyObject obj, bool value)
		{
			obj.SetValue(AttachProperty, value);
		}

		static bool _isUpdating = false;

		public static void OnPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			PasswordBox password = d as PasswordBox;
			password.PasswordChanged -= Password_PasswordChanged;
			if (!_isUpdating)
				password.Password = e.NewValue?.ToString();
			password.PasswordChanged += Password_PasswordChanged;
		}

		public static void OnAttached(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			PasswordBox password = d as PasswordBox;
			password.PasswordChanged += Password_PasswordChanged;
		}

		private static void Password_PasswordChanged(object sender, RoutedEventArgs e)
		{
			PasswordBox passwordBox = sender as PasswordBox;
			_isUpdating = true;
			SetPassword(passwordBox, passwordBox.Password);
			_isUpdating = false;
		}
	}
}
