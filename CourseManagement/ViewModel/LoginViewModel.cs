using CourseManagement.Common;
using CourseManagement.DataAccess;
using CourseManagement.Model;
using System;
using System.Threading.Tasks;
using System.Windows;

namespace CourseManagement.ViewModel
{
	public class LoginViewModel : NotifyBase
	{
		public LoginModel LoginModel { get; set; } = new LoginModel();
		public CommandBase CloseWindowCommand { get; set; }
		public CommandBase LoginCommand { get; set; }

		private string _errorMessage;

		private Visibility _showProgress;

		public string ErrorMessage
		{
			get { return _errorMessage; }
			set { _errorMessage = value; this.DoNotify(); }
		}

		public Visibility ShowProgress
		{
			get { return _showProgress; }
			set { _showProgress = value; this.DoNotify(); }
		}


		public LoginViewModel()
		{
			this.CloseWindowCommand = new CommandBase();
			this.CloseWindowCommand.DoExecute = new Action<object>((o) =>
			{
				(o as Window).Close();
			});
			this.CloseWindowCommand.DoCanExecute = new Func<object, bool>((o) =>
			{
				return true;
			});

			this.LoginCommand = new CommandBase();
			this.LoginCommand.DoExecute = new Action<object>(DoLogin);
			this.LoginCommand.DoCanExecute = new Func<object, bool>((o) => { return true; });
		}

		private void DoLogin(object o)
		{
			this.ShowProgress = Visibility.Visible;
			this.ErrorMessage = "";

			if (string.IsNullOrEmpty(LoginModel.UserName))
			{
				this.ErrorMessage = "请输入用户名！";
				this.ShowProgress = Visibility.Collapsed;
				return;
			}
			if (string.IsNullOrEmpty(LoginModel.Password))
			{
				this.ErrorMessage = "请输入密码！";
				this.ShowProgress = Visibility.Collapsed;
				return;
			}
			if (string.IsNullOrEmpty(LoginModel.ValidationCode))
			{
				this.ErrorMessage = "请输入验证码！";
				this.ShowProgress = Visibility.Collapsed;
				return;
			}

			if (LoginModel.ValidationCode.ToLower() != "zvyk")
			{
				this.ErrorMessage = "验证码输入不正确！";
				this.ShowProgress = Visibility.Collapsed;
				return;
			}

			Task.Run(new Action(() =>
			{
				try
				{
					var user = LocalDataAccess.GetInstance().CheckUserInfo(LoginModel.UserName, LoginModel.Password);
					if (user == null)
					{
						throw new Exception("登录失败！用户名或密码错误！");
					}

					GlobalValues.UserInfo = user;

					Application.Current.Dispatcher.Invoke(new Action(() =>
					{
						(o as Window).DialogResult = true;
					}));
				}
				catch (Exception ex)
				{
					this.ErrorMessage += ex.Message;
				}
			}));
		}
	}
}
