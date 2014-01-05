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

namespace TekConf.iOS
{
	public class ConferencesListView : MvxDialogViewController
	{
		private LoadingOverlay _loadingOverlay;

		public ConferencesListView () : base(UITableViewStyle.Plain)
		{		
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

			this.NavigationItem.SetLeftBarButtonItem(new UIBarButtonItem(UIBarButtonSystemItem.Refresh, OnRefresh), true);
			NavigationItem.RightBarButtonItem = ViewExtensions.CreateSliderButton ("Images/menu.png", PanelType.RightPanel, NavigationController as SlidingPanelsNavigationViewController );

			TableView.ReloadData ();
		}

		private async void OnRefresh(object sender, EventArgs e)
		{
			_loadingOverlay = new LoadingOverlay (UIScreen.MainScreen.Bounds);
			View.Add (_loadingOverlay);

			await VM.Refresh();
			TableView.ReloadData ();

			_loadingOverlay.Hide ();
		}
	}
}

