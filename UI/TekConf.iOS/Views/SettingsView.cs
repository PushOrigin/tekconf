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

		private SettingsViewModel VM { get { return this.ViewModel as SettingsViewModel; } }

		public override void ViewDidLoad ()
		{
			View = new UIView () { BackgroundColor = UIColor.White };
			base.ViewDidLoad ();

			if (RespondsToSelector(new Selector("edgesForExtendedLayout")))
				EdgesForExtendedLayout = UIRectEdge.None;

			NavigationItem.RightBarButtonItem = ViewExtensions.CreateSliderButton ("Images/menu.png", PanelType.RightPanel, NavigationController as SlidingPanelsNavigationViewController );

			//var label = new UILabel (new RectangleF (0, 40, 300, 40));
			//label.Text = "TEST";
			//Add (label);

			var loginButton = UIButton.FromType(UIButtonType.RoundedRect);
			loginButton.Frame = new RectangleF (5, 10, 150, 100);
			//loginButton.SetTitle ("TekConf", UIControlState.Normal);
			loginButton.SetImage(UIImage.FromFile ("Images/TekConf.png"), UIControlState.Normal);
			Add (loginButton);

			var googleButton = UIButton.FromType(UIButtonType.RoundedRect);
			googleButton.Frame = new RectangleF (160, 10, 150, 100);
			//googleButton.SetTitle ("Google Plus", UIControlState.Normal);
			googleButton.SetImage(UIImage.FromFile ("Images/Google_Plus_001.jpg"), UIControlState.Normal);
			googleButton.TouchUpInside += (object sender, EventArgs e) => {
				VM.IsOauthUserRegistered("google");
			};
			Add (googleButton);

			var twitterButton = UIButton.FromType(UIButtonType.RoundedRect);
			twitterButton.Frame = new RectangleF (5, 120, 150, 100);
			//twitterButton.SetTitle ("Twitter", UIControlState.Normal);
			twitterButton.SetImage(UIImage.FromFile ("Images/Twitter_001.jpg"), UIControlState.Normal);
			twitterButton.TouchUpInside += (object sender, EventArgs e) => {
				VM.IsOauthUserRegistered("twitter");
			};
			Add (twitterButton);

			var facebookButton = UIButton.FromType(UIButtonType.RoundedRect);
			facebookButton.Frame = new RectangleF (160, 120, 150, 100);
			//facebookButton.SetTitle ("Facebook", UIControlState.Normal);
			facebookButton.SetImage(UIImage.FromFile ("Images/facebook_001.jpg"), UIControlState.Normal);
			facebookButton.TouchUpInside += (object sender, EventArgs e) => {
				VM.IsOauthUserRegistered("facebook");
			};

			Add (facebookButton);


			var set = this.CreateBindingSet<SettingsView, SettingsViewModel> ();
			set.Bind (loginButton).To (vm => vm.ShowTekConfLoginCommand);
			set.Apply ();
		}
	}
}

