using System;
using Cirrious.MvvmCross.Touch.Views;
using MonoTouch.UIKit;
using MonoTouch.ObjCRuntime;
using System.Drawing;
using Cirrious.MvvmCross.Binding.BindingContext;
using TekConf.Core.ViewModels;

namespace TekConf.iOS
{				 
	public class OAuthRegisterView : MvxViewController
	{
		public OAuthRegisterView ()
		{
		}
	
		private OAuthRegisterViewModel VM { get { return this.ViewModel as OAuthRegisterViewModel; } }

		public override void ViewDidLoad ()
		{
			View = new UIView () { BackgroundColor = UIColor.White };
			base.ViewDidLoad ();

			if (RespondsToSelector(new Selector("edgesForExtendedLayout")))
				EdgesForExtendedLayout = UIRectEdge.None;

			var userNameLabel = new UILabel (new RectangleF (5, 5, 300, 40));
			userNameLabel.Text = "Username";
			Add (userNameLabel);
			var userName = new UITextField (new RectangleF (15, 35, 300, 40));
			userName.Placeholder = "Username";
			Add (userName);


			var loginButton = UIButton.FromType(UIButtonType.RoundedRect);
			loginButton.Frame = new RectangleF (0, 120, 300, 40);
			loginButton.SetTitle ("Register With TekConf", UIControlState.Normal);
			Add (loginButton);
			loginButton.TouchUpInside += async(object sender, EventArgs e) => {
				await VM.CreateOAuthUser();
			};

			var set = this.CreateBindingSet<OAuthRegisterView, OAuthRegisterViewModel> ();
			set.Bind (userName).To (vm => vm.UserName);
			set.Apply ();
		}
	}
}

