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
		private FlyoutNavigationController _navigation;
		private bool _navigationDisplayed = false;

		public ConferencesListView () : base(UITableViewStyle.Plain)
		{		
		}

		private ConferencesListViewModel VM { get { return this.ViewModel as ConferencesListViewModel; } } 

		private UIBarButtonItem CreateSliderButton(string imageName, PanelType panelType)
		{
			UIButton button = new UIButton(new RectangleF(0, 0, 40f, 40f));
			button.SetBackgroundImage(UIImage.FromBundle(imageName), UIControlState.Normal);
			button.TouchUpInside += delegate
			{
				SlidingPanelsNavigationViewController navController = NavigationController as SlidingPanelsNavigationViewController;
				navController.TogglePanel(panelType);
			};

			return new UIBarButtonItem(button);
		}

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
			NavigationItem.RightBarButtonItem = CreateSliderButton ("Images/Tabs/Alien.png", PanelType.RightPanel);


		
			/*
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
					new UIViewController { View = new UILabel { Text = "Vegetables (drag right)" } },
					new FlyoutItem { OnViewLoaded =  SettingsSelected},
					new UIViewController { View = new UILabel { Text = "Minerals (drag right)" } },
				},
			};

			*/
			TableView.ReloadData ();
		}

		private void SettingsSelected()
		{
			HideFlyout ();
			VM.ShowSettingsCommand.Execute (null);
		}

		// on select
		// remove from parent
		// call command on vm

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
				HideFlyout ();
			}
			else
			{
				_navigation.ToggleMenu ();
				View.AddSubview (_navigation.View);
				_navigationDisplayed = true;
			}
		}

		private void HideFlyout()
		{
			_navigation.View.RemoveFromSuperview();
			_navigationDisplayed = false;

		}
	}
}

