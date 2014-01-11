
using System;
using Cirrious.MvvmCross.Touch.Views;
using FlyoutNavigation;
using MonoTouch.UIKit;
using MonoTouch.Dialog;
using System.Linq;
using TekConf.Core;

namespace TekConf.iOS
{
	public class MainWindowView : MvxViewController 
	{
		FlyoutNavigationController navigation;

		// Data we'll use to create our flyout menu and views:
		string[] Tasks = {
			"Conferences",
			"Learn C#",
			"Write Killer App",
			"Add Platforms",
			"Profit",
			"Meet Obama",
		};

		//private MainWindowViewModel VM { get { return this.ViewModel as MainWindowViewModel; } }

		public override void ViewDidLoad ()
		{
			View = new UIView ();
			base.ViewDidLoad ();

			navigation = new FlyoutNavigationController ();
			navigation.View.Frame = UIScreen.MainScreen.Bounds;
			View.AddSubview (navigation.View);

			// Create the menu:
			navigation.NavigationRoot = new RootElement ("Task List") {
				new Section ("Task List") {
					from page in Tasks
					select new StringElement (page) as Element
				}
			};

			// Create an array of UINavigationControllers that correspond to your
			// menu items:
			navigation.ViewControllers = Array.ConvertAll (Tasks, title =>
				new UINavigationController (new TaskPageController (navigation, title))
			);
		}

		class TaskPageController : DialogViewController
		{
			public TaskPageController (FlyoutNavigationController navigation, string title) : base (null)
			{
				Root = new RootElement (title) {
					new Section {
						new CheckboxElement (title)
					}
				};
				NavigationItem.LeftBarButtonItem = new UIBarButtonItem (UIBarButtonSystemItem.Action, delegate {
					navigation.ToggleMenu ();
				});
			}
		}	
	}
}

