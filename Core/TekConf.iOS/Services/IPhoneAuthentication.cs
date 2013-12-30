using System;
using TekConf.Core.Services;
using TekConf.Core.Entities;
using Cirrious.MvvmCross.Plugins.Sqlite;

namespace TekConf.iOS
{
	public class IPhoneAuthentication : IAuthentication
	{
		private readonly ISQLiteConnection _connection;
		private string _userName;

		public IPhoneAuthentication (ISQLiteConnection connection)
		{
			_connection = connection;
			_connection.CreateTable<UserEntity>();
			LoadUserName();
		}

		#region IAuthentication implementation

		public bool IsAuthenticated
		{
			get
			{
				return false;
				//return Setup.MobileService.CurrentUser != null || !string.IsNullOrWhiteSpace(UserName);
			}
		}

		public string OAuthProvider
		{
			get
			{
				//if (IsAuthenticated)
				//	return Setup.MobileService.CurrentUser.UserId.Split(':')[0];

				return string.Empty;
			}
		}

		public string UserName
		{
			get
			{
				return _userName;
			}
			set
			{
				if (value != _userName)
				{
					_userName = value;
					SaveUserName(value);
				}
			}
		}

		private void LoadUserName()
		{
			var userEntity = _connection.Table<UserEntity>().FirstOrDefault();
			if (userEntity != null && !string.IsNullOrWhiteSpace(userEntity.UserName))
			{
				_userName = userEntity.UserName;
			}
		}

		private void SaveUserName(string value)
		{
			var userEntity = _connection.Table<UserEntity>().FirstOrDefault();
			if (userEntity != null)
			{
				_connection.Delete(userEntity);
			}

			if (!string.IsNullOrWhiteSpace(_userName))
			{
				var user = new UserEntity() { UserName = _userName };
				_connection.Insert(user);
			}
		}

		#endregion
	}
}

