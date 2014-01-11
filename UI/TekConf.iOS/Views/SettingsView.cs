using System;
using Cirrious.MvvmCross.Touch.Views;
using MonoTouch.UIKit;
using System.Drawing;
using TekConf.Core.ViewModels;
using Cirrious.MvvmCross.Binding.BindingContext;
using MonoTouch.ObjCRuntime;
using SlidingPanels.Lib;
using SlidingPanels.Lib.PanelContainers;
using Cirrious.MvvmCross.Dialog.Touch;

namespace TekConf.iOS
{
	public class SettingsView : MvxDialogViewController
	{
		public SettingsView () : base(UITableViewStyle.Plain)
		{
		}

		public override void ViewDidLoad ()
		{
			View = new UIView () { BackgroundColor = UIColor.White };
			base.ViewDidLoad ();

			if (RespondsToSelector(new Selector("edgesForExtendedLayout")))
				EdgesForExtendedLayout = UIRectEdge.None;

			NavigationItem.RightBarButtonItem = ViewExtensions.CreateSliderButton ("Images/menu.png", PanelType.RightPanel, NavigationController as SlidingPanelsNavigationViewController );

			var label = new UILabel (new RectangleF (0, 40, 300, 40));
			label.Text = "TEST";
			Add (label);

			var loginButton = UIButton.FromType(UIButtonType.RoundedRect);
			loginButton.Frame = new RectangleF (0, 10, 300, 40);
			loginButton.SetTitle ("Login Settings", UIControlState.Normal);
			Add (loginButton);

			var set = this.CreateBindingSet<SettingsView, SettingsViewModel> ();
			set.Bind (loginButton).To (vm => vm.ShowTekConfLoginCommand);
			set.Apply ();
		}
	}
}

