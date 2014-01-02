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

namespace TekConf.iOS
{
	public class ConferencesListView : MvxTableViewController
	{
		private LoadingOverlay _loadingOverlay;
		private FlyoutNavigationController _navigation;
		private bool _navigationDisplayed = false;

		public ConferencesListView ()
		{
		}

		private ConferencesListViewModel VM { get { return this.ViewModel as ConferencesListViewModel; } } 

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			// ios7 layout
			if (RespondsToSelector(new Selector("edgesForExtendedLayout")))
				EdgesForExtendedLayout = UIRectEdge.None;

			var source = new MvxSimpleTableViewSource (TableView, typeof(ConferenceCell));
			TableView.RowHeight = 250;
			TableView.Source = source;

			var set = this.CreateBindingSet<ConferencesListView, ConferencesListViewModel>();
			set.Bind (source).To (vm => vm.Conferences);
			set.Bind (source).For (s => s.SelectionChangedCommand).To (vm => vm.ShowDetailTabCommand);
			set.Apply();

			this.NavigationItem.SetLeftBarButtonItem(new UIBarButtonItem(UIBarButtonSystemItem.Refresh, OnRefresh), true);
			this.NavigationItem.SetRightBarButtonItem(new UIBarButtonItem(UIBarButtonSystemItem.Organize, OnFlyoutSelected), true);

			_navigation = new FlyoutNavigationController {
				// Create the navigation menu
				NavigationRoot = new RootElement ("Navigation") {
					new Section ("TekConf Menu") {
						new StringElement ("Settings"),
						new StringElement ("Vegetables"),
						new StringElement ("Minerals"),
					}
				},
				// Supply view controllers corresponding to menu items:
				ViewControllers = new [] {
					new UIViewController { View = new UILabel { Text = "Animals (drag right)" } },
					new UIViewController { View = new UILabel { Text = "Vegetables (drag right)" } },
					new UIViewController { View = new UILabel { Text = "Minerals (drag right)" } },
				},
			};

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

		private void OnFlyoutSelected(object sender, EventArgs e)
		{
			if(_navigationDisplayed)
			{
				_navigation.View.RemoveFromSuperview();
				_navigationDisplayed = false;
			}
			else
			{
				_navigation.ToggleMenu ();
				View.AddSubview (_navigation.View);
				_navigationDisplayed = true;
			}
		}
	}
}

