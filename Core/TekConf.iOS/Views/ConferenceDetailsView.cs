using System;
using Cirrious.MvvmCross.Touch.Views;
using MonoTouch.UIKit;
using System.Drawing;

namespace TekConf.iOS
{
	public  class ConferenceDetailsView : MvxViewController 
	{
		public ConferenceDetailsView ()
		{
		}

		public override void ViewDidLoad()
		{
			View = new UIView ();// { BackgroundColor = UIColor.White};
			base.ViewDidLoad();

			var nameField = new UILabel(new RectangleF(10, 140, 300, 40));
			Add(nameField);

			var dateField = new UILabel(new RectangleF(10, 160, 300, 40));
			Add(dateField);

			var addressField = new UILabel(new RectangleF(10, 180, 300, 40));
			Add(addressField);

			//var set = this.CreateBindingSet<ConferenceDetailView, ConferenceDetailViewModel> ();
			//set.Bind (nameField).To (vm => vm.Conference.name);
			//set.Bind (dateField).To (vm => vm.Conference.DateRange);
			//set.Bind (addressField).To (vm => vm.Conference.FormattedAddress);
			//set.Bind (sessionButton).To (vm => vm.ShowSessionsCommand);
			//set.Apply ();


			NavigationItem.BackBarButtonItem = new UIBarButtonItem()
			{
				Title = "Kid1"
			};

			var label = new UILabel(new RectangleF(10, 10, 300, 40));
			Add(label);
			var button = new UIButton(UIButtonType.RoundedRect);
			button.Frame = new RectangleF(10,50,300,40);
			button.SetTitle("Go", UIControlState.Normal);
			Add(button);

			//this.CreateBinding(label).To<Child1ViewModel>(vm => vm.Foo).Apply();
			//this.CreateBinding(button).To<Child1ViewModel>(vm => vm.GoCommand).Apply();
		}
	}
}

