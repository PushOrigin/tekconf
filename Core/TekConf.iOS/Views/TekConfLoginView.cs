using System;
using Cirrious.MvvmCross.Touch.Views;
using MonoTouch.UIKit;
using System.Drawing;
using TekConf.Core.ViewModels;
using Cirrious.MvvmCross.Binding.BindingContext;
using MonoTouch.ObjCRuntime;

namespace TekConf.iOS
{
	public class TekConfLoginView : MvxViewController
	{
		public TekConfLoginView ()
		{
		}

		private TekConfLoginViewModel VM { get { return this.ViewModel as TekConfLoginViewModel; } }

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

			var passwordLabel = new UILabel (new RectangleF (5, 65, 300, 40));
			passwordLabel.Text = "Password";
			Add (passwordLabel);
			var password = new UITextField (new RectangleF (15, 95, 300, 40));
			password.SecureTextEntry = true;
			Add (password);

			var loginButton = UIButton.FromType(UIButtonType.RoundedRect);
			loginButton.Frame = new RectangleF (0, 120, 300, 40);
			loginButton.SetTitle ("Login", UIControlState.Normal);
			Add (loginButton);
			loginButton.TouchUpInside += (object sender, EventArgs e) => {
				VM.Login();
			};

			var set = this.CreateBindingSet<TekConfLoginView, TekConfLoginViewModel> ();
			set.Bind (userName).To (vm => vm.UserName);
			set.Bind (password).To (vm => vm.Password);
			set.Apply ();


		}
	}
}

