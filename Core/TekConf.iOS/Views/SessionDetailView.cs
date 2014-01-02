using System;
using Cirrious.MvvmCross.Touch.Views;
using MonoTouch.UIKit;
using System.Drawing;
using Cirrious.MvvmCross.Binding.BindingContext;
using TekConf.Core.ViewModels;
using FlyoutNavigation;

namespace TekConf.iOS
{
	public class SessionDetailView : MvxViewController
	{
		public SessionDetailView ()
		{
		}

		public override void ViewDidLoad ()
		{
			View = new UIView () { BackgroundColor = UIColor.White };
			base.ViewDidLoad ();

			var titleField = new UILabel (new RectangleF (10, 20, 300, 40));
			titleField.TextAlignment = UITextAlignment.Center;
			Add (titleField);

			var speakerField = new UILabel (new RectangleF (10, 40, 300, 40));
			speakerField.TextAlignment = UITextAlignment.Center;
			Add (speakerField);

			var descriptionField = new UILabel (new RectangleF (10, 60, 300, 40));
			descriptionField.TextAlignment = UITextAlignment.Center;
			Add (descriptionField);

			var difficultyField = new UILabel (new RectangleF (10, 80, 300, 40));
			difficultyField.TextAlignment = UITextAlignment.Center;
			Add (difficultyField);

			var roomField = new UILabel (new RectangleF (10, 100, 300, 40));
			roomField.TextAlignment = UITextAlignment.Center;
			Add (roomField);

			var startField = new UILabel (new RectangleF (10, 120, 300, 40));
			startField.TextAlignment = UITextAlignment.Center;
			Add (startField);

			var set = this.CreateBindingSet<SessionDetailView, SessionDetailViewModel> ();
			set.Bind (titleField).To (c => c.Session.title);
			//set.Bind(speakerField).To(c=>c.Session.speakers);
			set.Bind (descriptionField).To (c => c.Session.description);
			//set.Bind(difficultyField).To(c=>c.Session.difficulty);
			set.Bind (roomField).To (c => c.Session.room);		
			set.Bind (startField).To (c => c.Session.startDescription);
			set.Apply ();
		}
	}
}

