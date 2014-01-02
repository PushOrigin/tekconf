using System;
using Cirrious.MvvmCross.Touch.Views;
using TekConf.Core;
using MonoTouch.UIKit;
using Cirrious.MvvmCross.ViewModels;
using Cirrious.CrossCore;
using TekConf.Core.ViewModels;

namespace TekConf.iOS
{
	public sealed class ConferenceDetailTabView : MvxTabBarViewController 
	{
		public ConferenceDetailTabView ()
		{
			ViewDidLoad ();
		}

		private ConferenceDetailTabViewModel VM { get { return base.ViewModel as ConferenceDetailTabViewModel; } }

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			if (ViewModel == null)
				return;

			var detailVm = VM.DetailVm;
			detailVm.Init(VM.Slug);
			VM.SessionsVm.Init (VM.Slug);

			var viewControllers = new UIViewController[] {
				CreateTabFor ("Details", "3d bar chart", VM.DetailVm),
				CreateTabFor("Sessions", "Bomb", VM.SessionsVm),
				CreateTabFor("Location", "Liner", VM.LocationVm),
			};
			ViewControllers = viewControllers;
			CustomizableViewControllers = new UIViewController[] { };
			SelectedViewController = ViewControllers[0];
		}

		private int _createdSoFarCount = 0;

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

		public void ShowGrandChild(IMvxTouchView view)
		{
			var currentNav = SelectedViewController as UINavigationController;
			currentNav.PushViewController(view as UIViewController, true);
		}
	}
}

