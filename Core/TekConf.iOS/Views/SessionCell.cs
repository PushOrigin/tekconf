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
			var titleField = new UILabel(new RectangleF(10, 5, 300, 40));
			titleField.TextAlignment = UITextAlignment.Center;
			Add(titleField);

			var speakerField = new UILabel(new RectangleF(10, 30, 300, 40));
			speakerField.TextAlignment = UITextAlignment.Center;
			Add(speakerField);

			var startField = new UILabel(new RectangleF(10, 50, 300, 40));
			startField.TextAlignment = UITextAlignment.Center;
			Add(startField);

			var roomField = new UILabel(new RectangleF(10, 70, 300, 40));
			roomField.TextAlignment = UITextAlignment.Center;
			Add(roomField);

			var difficultyField = new UILabel(new RectangleF(10, 90, 300, 40));
			difficultyField.TextAlignment = UITextAlignment.Center;
			Add(difficultyField);

			var descriptionField = new UILabel(new RectangleF(10, 110, 300, 40));
			descriptionField.TextAlignment = UITextAlignment.Center;
			Add(descriptionField);

			this.DelayBind (() =>{
				var set = this.CreateBindingSet<SessionCell, ConferenceSessionListDto>();
				set.Bind(titleField).To(c=>c.title);
				set.Bind(roomField).To(c=>c.room);
				set.Bind(startField).To(c=>c.start);
				set.Bind(speakerField).To(c=>c.speakerNames);
				set.Bind(speakerField).To(c=>c.startDescription);
				set.Apply();
			});
		}
	}
}

