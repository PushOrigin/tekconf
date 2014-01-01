using System;
using Cirrious.MvvmCross.Binding.Touch.Views;
using Cirrious.MvvmCross.Binding.BindingContext;
using TekConf.Core.Repositories;
using MonoTouch.UIKit;
using System.Drawing;

namespace TekConf.iOS
{
	public class ConferenceCell : MvxTableViewCell
	{
		private readonly MvxImageViewLoader _imageViewLoader;

		public ConferenceCell (IntPtr handle) : base(handle)
		{
			var imageView = new UIImageView(new RectangleF(5,3, 310, 185));
			Add (imageView);
			_imageViewLoader = new MvxImageViewLoader(() => imageView);

			var nameField = new UILabel(new RectangleF(10, 180, 300, 40));
			nameField.TextAlignment = UITextAlignment.Center;
			Add(nameField);

			var dateField = new UILabel(new RectangleF(10, 200, 300, 40));
			dateField.TextAlignment = UITextAlignment.Center;
			Add(dateField);

			var cityField = new UILabel(new RectangleF(10, 220, 300, 40));
			cityField.TextAlignment = UITextAlignment.Center;
			Add(cityField);

			this.DelayBind (() =>{
				var set = this.CreateBindingSet<ConferenceCell, ConferencesListViewDto>();
				set.Bind(nameField).To(c=>c.name);
				set.Bind(dateField).To(c=>c.DateRange);
				set.Bind(cityField).To(c=>c.FormattedCity);			
				set.Bind(_imageViewLoader).To(vm =>vm.imageUrl);
				set.Apply();
			});
		}
	}
}

