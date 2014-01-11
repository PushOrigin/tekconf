using System;
using Cirrious.MvvmCross.Touch.Views;
using MonoTouch.UIKit;
using Cirrious.MvvmCross.Binding.Touch.Views;
using MonoTouch.ObjCRuntime;
using Cirrious.MvvmCross.Binding.BindingContext;
using TekConf.Core.ViewModels;
using Cirrious.MvvmCross.Dialog.Touch;
using Cirrious.MvvmCross.Plugins.Messenger;
using Cirrious.CrossCore;
using TekConf.Core.Messages;
using TekConf.Core.Services;

namespace TekConf.iOS
{
	public class ConferenceSessionsView : MvxTableViewController 
	{
		private MvxSubscriptionToken _conferencesListAllExceptionToken;

		public ConferenceSessionsView () //: base(UITableViewStyle.Plain)
		{
			var messenger = Mvx.Resolve<IMvxMessenger>();
			_conferencesListAllExceptionToken = messenger.Subscribe<ConferenceSessionsExceptionMessage> (message => {
				if (message.ExceptionObject.Message == "The remote server returned an error: NotFound.")
				{
					const string errorMessage = "Could not connect to remote server. Please check your network connection and try again.";
					var mb = Mvx.Resolve<IMessageBox> ();
					mb.Show (errorMessage);
				}
			});
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad();

			var source = new MvxSessionTableViewSource (TableView, typeof(SessionCell));
			TableView.RowHeight = 110;
			TableView.Source = source;

			var set = this.CreateBindingSet<ConferenceSessionsView, ConferenceSessionsViewModel>();
			set.Bind (source).To (vm => vm.Conference.Sessions);
			set.Bind (source).For (s => s.SelectionChangedCommand).To (vm => vm.ShowSessionDetailCommand);
			set.Apply();

			TableView.ReloadData ();
		}

	}
}

