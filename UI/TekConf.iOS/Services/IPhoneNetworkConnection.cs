using System;
using TekConf.Core.Services;

namespace TekConf.iOS
{
	public class IPhoneNetworkConnection : INetworkConnection
	{
		public bool IsNetworkConnected()
		{
			return true; //TODO
		}

		public string NetworkDownMessage
		{
			get
			{
				return "Could not connect to remote server. Please check your network connection and try again.";			
			}
		}
	}}

