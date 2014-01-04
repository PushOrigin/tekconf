using System.Windows.Input;
using Cirrious.MvvmCross.ViewModels;
using TekConf.Core.Services;

namespace TekConf.Core.ViewModels
{
	public class TekConfLoginViewModel : MvxViewModel
	{
		private readonly IRemoteDataService _remoteDataService;
		private readonly IAuthentication _authentication;
		private readonly IMessageBox _messageBox;
		private readonly INetworkConnection _networkConnection;

		public TekConfLoginViewModel(IRemoteDataService remoteDataService, IAuthentication authentication, IMessageBox messageBox, INetworkConnection networkConnection)
		{
			_remoteDataService = remoteDataService;
			_authentication = authentication;
			_messageBox = messageBox;
			_networkConnection = networkConnection;
		}

		public void Init()
		{
		}

		public async void Login()
		{
			IsLoggingIn = true;
			if (!_networkConnection.IsNetworkConnected())
			{
				InvokeOnMainThread(() => _messageBox.Show(_networkConnection.NetworkDownMessage));
			}
			else
			{
				var result = await _remoteDataService.LoginWithTekConf(UserName, Password);
				LoginSuccess(result.IsLoggedIn, result.UserName);
			}
		}

		public ICommand ShowConferencesListCommand
		{
			get
			{
				return new MvxCommand(() => ShowViewModel<ConferencesListViewModel>());
			}
		}

		private void LoginSuccess(bool isLoggedIn, string userName)
		{
			IsLoggingIn = false;
			UserName = userName;
			_authentication.UserName = userName;
			ShowConferencesListCommand.Execute(null);
		}

		bool isLoggingIn;
		public bool IsLoggingIn {
			get {
				return isLoggingIn;
			}
			set {
				if (value != isLoggingIn)
					RaisePropertyChanged("IsLoggingIn");
				isLoggingIn = value;
			}
		}
		string userName;
		public string UserName {
			get {
				return userName;
			}
			set {
				if (value != userName)
					RaisePropertyChanged("UserName");
				userName = value;
			}
		}
		string password;
		public string Password {
			get {
				return password;
			}
			set {
				if (value != password)
					RaisePropertyChanged("Password");
				password = value;
			}
		}
	}
}