using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Cirrious.MvvmCross.Touch.Views;
using Cirrious.MvvmCross.Binding.BindingContext;
using TekConf.Core.ViewModels;

namespace TekConf.iOS
{
	public partial class ConferenceDetailView : MvxViewController
	{
		public ConferenceDetailView () : base ("ConferenceDetailView", null)
		{
		}

		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();
			
			// Release any cached data, images, etc that aren't in use.
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			
			//var imageView = new UIImageView(new RectangleF(0,0, 140, 140));
			//Add (imageView);
			//_imageViewLoader = new MvxImageViewLoader(() => imageView);

			var nameField = new UILabel(new RectangleF(10, 140, 300, 40));
			Add(nameField);

			var addressField = new UILabel(new RectangleF(10, 160, 300, 40));
			Add(addressField);

			var set = this.CreateBindingSet<ConferenceDetailView, ConferenceDetailViewModel> ();
			set.Bind (nameField).To (vm => vm.Conference.name);
			set.Apply ();
		}
	}
}

