using System;
using Cirrious.MvvmCross.Touch.Views;
using MonoTouch.UIKit;
using System.Drawing;
using TekConf.Core.ViewModels;
using Cirrious.MvvmCross.Binding.BindingContext;

namespace TekConf.iOS
{
	public class TekConfLoginView : MvxViewController
	{
		public TekConfLoginView ()
		{
		}

		public override void ViewDidLoad ()
		{
			View = new UIView () { BackgroundColor = UIColor.White };
			base.ViewDidLoad ();

			var userNameLabel = new UILabel (new RectangleF (0, 5, 300, 20));
			userNameLabel.Text = "Username";
			Add (userNameLabel);
			var userName = new UITextView (new RectangleF (0, 30, 300, 20));
			Add (userName);

			var passwordLabel = new UILabel (new RectangleF (0, 35, 300, 20));
			passwordLabel.Text = "Password";
			Add (passwordLabel);
			var password = new UITextView (new RectangleF (0, 60, 300, 20));
			Add (password);


			var set = this.CreateBindingSet<TekConfLoginView, TekConfLoginViewModel> ();
			set.Bind (userName).To (vm => vm.UserName);
			set.Bind (password).To (vm => vm.Password);
			set.Apply ();


		}
	}
}

