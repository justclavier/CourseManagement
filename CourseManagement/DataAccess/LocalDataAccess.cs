﻿using CourseManagement.Common;
using CourseManagement.DataAccess.DataEntity;
using CourseManagement.Model;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
//using System.Data.SqlClient;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;

namespace CourseManagement.DataAccess
{
	public class LocalDataAccess
	{
		private static LocalDataAccess instance;
		private LocalDataAccess() { }

		public static LocalDataAccess GetInstance()
		{
			return instance ?? (instance = new LocalDataAccess());
		}

		//SqlConnection conn;
		//SqlCommand comm;
		//SqlDataAdapter adapter;
		MySqlConnection conn;
		MySqlCommand comm;
		MySqlDataAdapter adapter;

		private void Dispose()
		{
			if (adapter != null)
			{
				adapter.Dispose();
				adapter = null;
			}

			if (comm != null)
			{
				comm.Dispose();
				comm = null;
			}

			if (conn != null)
			{
				conn.Close();
				conn.Dispose();
				conn = null;
			}
		}

		private bool DBConnection()
		{
			string connStr = ConfigurationManager.ConnectionStrings["db"].ConnectionString;

			if (conn == null)
			{
				conn = new MySqlConnection(connStr);
			}

			try
			{
				conn.Open();
				return true;
			}
			catch (Exception ex)
			{
				return false;
			}
		}

		public UserEntity CheckUserInfo(string userName, string pwd)
		{
			try
			{
				if (DBConnection())
				{
					string userSql = "select * from users where user_name=@userName and password=@pwd and is_validation=1";
					adapter = new MySqlDataAdapter(userSql, conn);
					adapter.SelectCommand.Parameters.Add(new MySqlParameter("@userName", MySqlDbType.VarChar) { Value = userName });
					adapter.SelectCommand.Parameters.Add(new MySqlParameter("@pwd", MySqlDbType.VarChar) { Value = MD5Provider.GetMD5String(pwd + "@" + userName) });

					DataTable table = new DataTable();
					int count = adapter.Fill(table);

					if (count < 0)
					{
						throw new Exception("用户名或密码不正确！");
					}

					DataRow dr = table.Rows[0];
					if (dr.Field<Int32>("is_can_login") == 0)
					{
						throw new Exception("当前用户没有权限使用此平台！");
					}

					UserEntity userInfo = new UserEntity();
					userInfo.UserName = dr.Field<string>("user_name");
					userInfo.RealName = dr.Field<string>("real_name");
					userInfo.Password = dr.Field<string>("Password");
					userInfo.Avatar = dr.Field<string>("avatar");
					userInfo.Gender = dr.Field<Int32>("gender");

					return userInfo;
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
			finally
			{
				conn.Close();
				this.Dispose();
			}

			return null;
		}

		public List<CourseSeriesModel> GetCoursePlayRecord()
		{
			try
			{
				List<CourseSeriesModel> cModelList = new List<CourseSeriesModel>();
				if (DBConnection())
				{
					string userSql = @"select a.course_name,a.course_id,b.play_count,b.is_growing,b.growing_rate,c.platform_name from courses a
left join play_record b
on a.course_id=b.course_id
left join platforms c
on b.platform_id=c.platform_id
order by a.course_id,c.platform_id";
					adapter = new MySqlDataAdapter(userSql, conn);

					DataTable table = new DataTable();
					int count = adapter.Fill(table);

					string courseId = "";
					CourseSeriesModel cModel = null;

					foreach (DataRow dr in table.AsEnumerable())
					{
						string tempId = dr.Field<string>("course_id");
						if (courseId != tempId)
						{
							courseId = tempId;
							cModel = new CourseSeriesModel();
							cModelList.Add(cModel);

							cModel.CourseName = dr.Field<string>("course_id");
							cModel.SeriesCollection = new LiveCharts.SeriesCollection();
							cModel.SeriesList = new System.Collections.ObjectModel.ObservableCollection<SeriesModel>();
						}
						if (cModel != null)
						{
							cModel.SeriesCollection.Add(new PieSeries
							{
								Title = dr.Field<string>("platform_name"),
								Values = new ChartValues<ObservableValue> { new ObservableValue((double)dr.Field<decimal>("play_count")) },
								DataLabels = false
							}); ;

							cModel.SeriesList.Add(new SeriesModel
							{
								SeriesName = dr.Field<string>("platform_name"),
								CurrentValue = (int)dr.Field<decimal>("play_count"),
								IsGrowing = dr.Field<Int32>("is_growing") == 1,
								ChangeRate = (int)dr.Field<decimal>("growing_rate")
							});
						}
					}
				}
				return cModelList;
			}
			catch (Exception ex)
			{
				throw ex;
			}
			finally
			{
				conn.Close();
				this.Dispose();
			}
		}

		public List<string> GetTeachers()
		{
			try
			{
				List<string> result = new List<string>();
				if (this.DBConnection())
				{
					string sql = "SELECT real_name FROM `users` WHERE is_teacher=1";
					adapter = new MySqlDataAdapter(sql, conn);

					DataTable table = new DataTable();
					int count = adapter.Fill(table);

					if (count > 0)
					{
						result = table.AsEnumerable().Select(c => c.Field<string>("real_name")).ToList();
					}
				}

				return result;
			}
			catch (Exception ex)
			{
				throw ex;
			}
			finally
			{
				conn.Close();
				this.Dispose();
			}
		}

		public List<CourseModel> GetCourses()
		{
			try
			{
				List<CourseModel> result = new List<CourseModel>();
				if (this.DBConnection())
				{
					string sql = @"select a.course_id,a.course_name,a.course_cover,a.course_url,a.description,c.real_name FROM courses a
left join course_teacher_relation b
on a.course_id=b.course_id
left join users c
on b.teacher_id=c.user_id
ORDER BY a.course_id";
					adapter = new MySqlDataAdapter(sql, conn);

					DataTable table = new DataTable();
					int count = adapter.Fill(table);

					if (count > 0)
					{
						string courseId = "";
						CourseModel model = null;
						foreach (DataRow dr in table.AsEnumerable())
						{
							string tempId = dr.Field<string>("course_id");
							if (courseId != tempId)
							{
								courseId = tempId;
								model = new CourseModel();
								model.CourseName = dr.Field<string>("course_name");
								model.Cover = dr.Field<string>("course_cover");
								model.Url = dr.Field<string>("course_url");
								model.Description = dr.Field<string>("description");
								model.Teachers = new List<string> { };
								result.Add(model);
							}
							if (model != null)
							{
								model.Teachers.Add(dr.Field<string>("real_name"));
							}
						}
					}
				}

				return result;
			}
			catch (Exception ex)
			{
				throw ex;
			}
			finally
			{
				conn.Close();
				this.Dispose();
			}
		}
	}
}
