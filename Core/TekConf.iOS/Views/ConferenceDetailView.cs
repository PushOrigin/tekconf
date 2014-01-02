using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Cirrious.MvvmCross.Touch.Views;
using Cirrious.MvvmCross.Binding.BindingContext;
using TekConf.Core.ViewModels;
using Cirrious.MvvmCross.ViewModels;
using Cirrious.MvvmCross.Binding.Touch.Views;

namespace TekConf.iOS
{
	public class ConferenceDetailView : MvxViewController
	{
		private MvxImageViewLoader _imageViewLoader;

		public ConferenceDetailView ()
		{
		}

		public override void ViewDidLoad ()
		{
			View = new UIView () { BackgroundColor = UIColor.White};
			base.ViewDidLoad ();

			var imageView = new UIImageView(new RectangleF(5,65, 310, 185));
			Add (imageView);
			_imageViewLoader = new MvxImageViewLoader(() => imageView);


			var nameField = new UILabel(new RectangleF(10, 340, 300, 40));
			nameField.TextAlignment = UITextAlignment.Center;
			Add(nameField);

			var dateField = new UILabel(new RectangleF(10, 360, 300, 40));
			dateField.TextAlignment = UITextAlignment.Center;
			Add(dateField);

			var addressField = new UILabel(new RectangleF(10, 380, 300, 40));
			addressField.TextAlignment = UITextAlignment.Center;
			Add(addressField);

			var set = this.CreateBindingSet<ConferenceDetailView, ConferenceDetailViewModel> ();
			set.Bind (nameField).To (vm => vm.Conference.name);
			set.Bind (dateField).To (vm => vm.Conference.DateRange);
			set.Bind (addressField).To (vm => vm.Conference.FormattedAddress);
			set.Bind(_imageViewLoader).To(vm =>vm.Conference.imageUrl);
			set.Apply ();
		}

	}
}

