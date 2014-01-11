using System;
using Cirrious.MvvmCross.Touch.Views;
using MonoTouch.UIKit;
using System.Drawing;
using MonoTouch.ObjCRuntime;
using TekConf.Core.ViewModels;
using Cirrious.MvvmCross.Binding.BindingContext;
using Cirrious.MvvmCross.Binding.Touch.Views;
using FlyoutNavigation;
using MonoTouch.Dialog;
using Cirrious.CrossCore;
using SlidingPanels.Lib.PanelContainers;
using SlidingPanels.Lib;
using Cirrious.MvvmCross.Dialog.Touch;
using Cirrious.MvvmCross.Plugins.Messenger;
using TekConf.Core.Messages;
using TekConf.Core.Services;

namespace TekConf.iOS
{
	public class ConferencesListView : MvxDialogViewController
	{
		private MvxSubscriptionToken _conferencesListAllExceptionToken;
		private MvxSubscriptionToken _conferencesListFavoritesExceptionToken;

		public ConferencesListView () : base(UITableViewStyle.Plain)
		{		
			var messenger = Mvx.Resolve<IMvxMessenger>();
			_conferencesListAllExceptionToken = messenger.Subscribe<ConferencesListAllExceptionMessage> (message => {
				if (message.ExceptionObject.Message == "The remote server returned an error: NotFound.") {
					const string errorMessage = "Could not connect to remote server. Please check your network connection and try again.";
					var mb = Mvx.Resolve<IMessageBox> ();
					mb.Show (errorMessage);
				}
			});

			_conferencesListFavoritesExceptionToken = messenger.Subscribe<ConferencesListFavoritesExceptionMessage> (message => {
				if (message.ExceptionObject.Message == "The remote server returned an error: NotFound.") {
					const string errorMessage = "Could not connect to remote server. Please check your network connection and try again.";
					var mb = Mvx.Resolve<IMessageBox> ();
					mb.Show (errorMessage);
				}
			});
		}

		private ConferencesListViewModel VM { get { return this.ViewModel as ConferencesListViewModel; } } 

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			var source = new MvxSimpleTableViewSource (TableView, typeof(ConferenceCell));
			TableView.RowHeight = 250;
			TableView.Source = source;

			var set = this.CreateBindingSet<ConferencesListView, ConferencesListViewModel>();
			set.Bind (source).To (vm => vm.Conferences);
			set.Bind (source).For (s => s.SelectionChangedCommand).To (vm => vm.ShowDetailTabCommand);
			set.Apply();

			//NavigationItem.RightBarButtonItem = ViewExtensions.CreateSliderButton ("Images/menu.png", PanelType.RightPanel, NavigationController as SlidingPanelsNavigationViewController );

			TableView.ReloadData ();
		}
	}
}

