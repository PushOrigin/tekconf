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
		private MvxImageViewLoader _imageViewLoader;

		public ConferenceCell (IntPtr handle) : base(handle)
		{
			var imageView = new UIImageView(new RectangleF(0,0, 140, 140));
			Add (imageView);
			_imageViewLoader = new MvxImageViewLoader(() => imageView);

			var nameField = new UILabel(new RectangleF(10, 140, 300, 40));
			Add(nameField);

			var label = new UILabel(new RectangleF(10, 160, 300, 40));
			Add(label);

			//var addressField = new UITextField (new RectangleF (10, 10, 300, 40));
			//Add (addressField);

			this.DelayBind (() =>{
				var set = this.CreateBindingSet<ConferenceCell, ConferencesListViewDto>();
				set.Bind(_imageViewLoader).To(vm =>vm.imageUrl);
				set.Bind(nameField).To(c=>c.name);
				//	set.Bind(addressField).To(c=>c.FormattedAddress);
				set.Bind(label).To(c=>c.FormattedCity);			
				set.Apply();
			});
		}
	}
}

