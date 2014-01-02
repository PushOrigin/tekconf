using System;
using Cirrious.MvvmCross.Touch.Views;
using MonoTouch.UIKit;
using System.Drawing;

namespace TekConf.iOS
{
	public class ConferenceLocationView : MvxViewController
	{
		public ConferenceLocationView ()
		{
		}

		public override void ViewDidLoad()
		{
			View = new UIView () { BackgroundColor = UIColor.White};
			base.ViewDidLoad ();

			var nameField = new UILabel (new RectangleF (10, 140, 300, 40));
			Add (nameField);

			var dateField = new UILabel (new RectangleF (10, 160, 300, 40));
			Add (dateField);

			var addressField = new UILabel (new RectangleF (10, 180, 300, 40));
			Add (addressField);
		}
	}
}

