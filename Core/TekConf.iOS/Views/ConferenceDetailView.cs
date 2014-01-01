using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Cirrious.MvvmCross.Touch.Views;
using Cirrious.MvvmCross.Binding.BindingContext;
using TekConf.Core.ViewModels;
using Cirrious.MvvmCross.ViewModels;

namespace TekConf.iOS
{
	public class ConferenceDetailView : MvxViewController
	{
		public ConferenceDetailView ()
		{
		}

		public override void ViewDidLoad ()
		{
			View = new UIView () { BackgroundColor = UIColor.White};
			base.ViewDidLoad ();

			var nameField = new UILabel(new RectangleF(10, 140, 300, 40));
			Add(nameField);

			var dateField = new UILabel(new RectangleF(10, 160, 300, 40));
			Add(dateField);

			var addressField = new UILabel(new RectangleF(10, 180, 300, 40));
			Add(addressField);

			var sessionButton = UIButton.FromType(UIButtonType.RoundedRect);
			sessionButton.Frame = new RectangleF (10, 220, 300, 40);
			sessionButton.SetTitle ("Sessions", UIControlState.Normal);
			Add (sessionButton);


			var set = this.CreateBindingSet<ConferenceDetailView, ConferenceDetailViewModel> ();
			set.Bind (nameField).To (vm => vm.Conference.name);
			set.Bind (dateField).To (vm => vm.Conference.DateRange);
			set.Bind (addressField).To (vm => vm.Conference.FormattedAddress);
			set.Bind (sessionButton).To (vm => vm.ShowSessionsCommand);
			set.Apply ();
		}

	}
}

