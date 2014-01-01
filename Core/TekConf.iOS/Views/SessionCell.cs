using System;
using Cirrious.MvvmCross.Binding.Touch.Views;
using TekConf.RemoteData.Dtos.v1;
using Cirrious.MvvmCross.Binding.BindingContext;
using MonoTouch.UIKit;
using System.Drawing;

namespace TekConf.iOS
{
	public class SessionCell : MvxTableViewCell
	{
		public SessionCell (IntPtr handle) : base(handle)
		{
			var nameField = new UILabel(new RectangleF(10, 20, 300, 40));
			nameField.TextAlignment = UITextAlignment.Center;
			Add(nameField);

			var dateField = new UILabel(new RectangleF(10, 40, 300, 40));
			dateField.TextAlignment = UITextAlignment.Center;
			Add(dateField);

			var cityField = new UILabel(new RectangleF(10, 60, 300, 40));
			cityField.TextAlignment = UITextAlignment.Center;
			Add(cityField);
			cityField.Text = "test";

			this.DelayBind (() =>{
				//var set = this.CreateBindingSet<SessionCell, FullSessionGroup>();
				//set.Bind(nameField).To(c=>c.description);
				//set.Bind(dateField).To(c=>c.difficulty);
				//set.Bind(cityField).To(c=>c.room);			
				//set.Apply();
			});
		}
	}
}

