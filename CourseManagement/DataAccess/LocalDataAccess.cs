﻿using CourseManagement.Common;
using CourseManagement.DataAccess.DataEntity;
//using System.Data.SqlClient;
using MySql.Data.MySqlClient;
using System;
using System.Configuration;
using System.Data;

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
	}
}
