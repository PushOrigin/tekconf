using System;
using Cirrious.MvvmCross.Binding.Touch.Views;
using MonoTouch.UIKit;
using Cirrious.MvvmCross.Binding.BindingContext;
using TekConf.RemoteData.Dtos.v1;
using System.Drawing;

namespace TekConf.iOS
{
	public class FullSessionDtoCell : MvxTableViewCell
	{
		public FullSessionDtoCell (IntPtr handle) : base(handle)
		{
			var titleField = new UITextView(new RectangleF(10, 5, 300, 40));
			titleField.TextAlignment = UITextAlignment.Left;
			Add(titleField);

			var speakerField = new UITextView(new RectangleF(10, 30, 300, 40));
			speakerField.TextAlignment = UITextAlignment.Left;
			Add(speakerField);

			var startField = new UITextView(new RectangleF(10, 50, 300, 40));
			startField.TextAlignment = UITextAlignment.Left;
			Add(startField);

			var roomField = new UITextView(new RectangleF(10, 70, 300, 40));
			roomField.TextAlignment = UITextAlignment.Left;
			roomField.TextColor = UIColor.Blue;
			Add(roomField);

			var difficultyField = new UITextView(new RectangleF(10, 90, 300, 40));
			difficultyField.TextAlignment = UITextAlignment.Left;
			Add(difficultyField);

			var descriptionField = new UITextView(new RectangleF(10, 110, 300, 40));
			descriptionField.TextAlignment = UITextAlignment.Left;
			Add(descriptionField);


			this.DelayBind (() =>{
				var set = this.CreateBindingSet<FullSessionDtoCell, FullSessionDto>();
				set.Bind(titleField).To(c=>c.title);
				set.Bind(roomField).To(c=>c.room);
				set.Bind(startField).To(c=>c.start);
				set.Bind(speakerField).To(c=>c.speakers);
				set.Bind(speakerField).To(c=>c.startDescription);
				set.Apply();
			});
		}
	}
}

