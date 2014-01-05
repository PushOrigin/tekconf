using System;
using Cirrious.MvvmCross.Touch.Views;
using TekConf.Core;
using Cirrious.MvvmCross.ViewModels;
using MonoTouch.UIKit;
using SlidingPanels.Lib.PanelContainers;
using SlidingPanels.Lib;
using Cirrious.MvvmCross.Binding.BindingContext;

namespace TekConf.iOS
{
	public class ConferencesTabView : MvxTabBarViewController
	{
		private LoadingOverlay _loadingOverlay;
		private int _createdSoFarCount = 0;

		public ConferencesTabView ()
		{
			ViewDidLoad ();
		}

		private ConferencesTabViewModel VM { get { return base.ViewModel as ConferencesTabViewModel; } }

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			if (ViewModel == null)
				return;

			VM.ConferencesListVm.Init (new TekConf.Core.ViewModels.ConferencesListViewModel.Parameters{ IsRefreshing = false });

			var viewControllers = new UIViewController[] {
				CreateTabFor ("Conferences", "3d bar chart", VM.ConferencesListVm),
				CreateTabFor ("Favorites", "Liner", VM.ConferenceFavoritesVm),
			};
			ViewControllers = viewControllers;
			CustomizableViewControllers = new UIViewController[] { };
			SelectedViewController = ViewControllers [0];

			NavigationItem.SetLeftBarButtonItem(new UIBarButtonItem(UIBarButtonSystemItem.Refresh, OnRefresh), true);
			//NavigationItem.RightBarButtonItem = ViewExtensions.CreateSliderButton ("Images/menu.png", PanelType.RightPanel, NavigationController as SlidingPanelsNavigationViewController);
		}

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);
			NavigationItem.RightBarButtonItem = ViewExtensions.CreateSliderButton ("Images/menu.png", PanelType.RightPanel, NavigationController as SlidingPanelsNavigationViewController);
		}

		private UIViewController CreateTabFor(string title, string imageName, IMvxViewModel viewModel)
		{
			var controller = new UINavigationController();
			var screen = this.CreateViewControllerFor(viewModel) as UIViewController;
			SetTitleAndTabBarItem(screen, title, imageName);
			controller.PushViewController(screen, false);
			return controller;
		}

		private void SetTitleAndTabBarItem(UIViewController screen, string title, string imageName)
		{
			screen.Title = title;
			screen.TabBarItem = new UITabBarItem(title, UIImage.FromBundle("Images/Tabs/" + imageName + ".png"),
				_createdSoFarCount);
			_createdSoFarCount++;
		}

		private async void OnRefresh(object sender, EventArgs e)
		{
			_loadingOverlay = new LoadingOverlay (UIScreen.MainScreen.Bounds);
			View.Add (_loadingOverlay);

			await VM.ConferencesListVm.Refresh();
			//TableView.ReloadData ();

			_loadingOverlay.Hide ();
		}

	}
}

