using System;
using TekConf.Core.Services;
using MonoTouch.UIKit;

namespace TekConf.iOS
{
	public class IPhoneMessageBox : IMessageBox 
	{
		public IPhoneMessageBox ()
		{
		}

		#region IMessageBox implementation

		public void Show (string message)
		{
			UIAlertView _error = new UIAlertView ("Important", message, null, "Ok", null);
			_error.Show ();
		}

		#endregion
	}
}

