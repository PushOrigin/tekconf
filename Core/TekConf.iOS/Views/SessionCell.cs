using System;
using Cirrious.MvvmCross.Binding.Touch.Views;
using TekConf.RemoteData.Dtos.v1;
using Cirrious.MvvmCross.Binding.BindingContext;
using MonoTouch.UIKit;
using System.Drawing;
using TekConf.Core.Repositories;

namespace TekConf.iOS
{
	public class SessionCell : MvxTableViewCell
	{
		public SessionCell (IntPtr handle) : base(handle)
		{
			var titleField = new UITextView(new RectangleF(10, 5, 300, 40));
			titleField.TextAlignment = UITextAlignment.Left;
			Add(titleField);

			var speakerField = new UITextView(new RectangleF(10, 30, 300, 20));
			speakerField.TextAlignment = UITextAlignment.Left;
			Add(speakerField);

			var startField = new UITextView(new RectangleF(10, 50, 300, 20));
			startField.TextAlignment = UITextAlignment.Left;
			Add(startField);

			var roomField = new UITextView(new RectangleF(10, 70, 300, 20));
			roomField.TextAlignment = UITextAlignment.Left;
			roomField.TextColor = UIColor.Blue;
			Add(roomField);

			var difficultyField = new UITextView(new RectangleF(10, 95, 300, 20));
			difficultyField.TextAlignment = UITextAlignment.Left;
			Add(difficultyField);

			/*
			var descriptionField = new UITextView(new RectangleF(10, 110, 300, 20));
			descriptionField.TextAlignment = UITextAlignment.Left;
			Add(descriptionField);*/

			this.DelayBind (() =>{
				var set = this.CreateBindingSet<SessionCell, ConferenceSessionListDto>();
				set.Bind(titleField).To(c=>c.title);
				set.Bind(roomField).To(c=>c.room);
				set.Bind(startField).To(c=>c.start);
				set.Bind(speakerField).To(c=>c.speakerNames);
				//set.Bind(speakerField).To(c=>c.descriptionField);
				set.Apply();
			});
		}
	}
}

