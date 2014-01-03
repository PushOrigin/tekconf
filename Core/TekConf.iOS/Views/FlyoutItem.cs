using System;
using MonoTouch.UIKit;

namespace TekConf.iOS
{
	public class FlyoutItem : UIViewController 
	{
		public FlyoutItem ()
		{
		}

		public Action OnViewLoaded { get; set; }

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
		}

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);
			if (OnViewLoaded != null)
				OnViewLoaded ();
		}
	}
}

