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
			var titleField = new UILabel(new RectangleF(10, 5, 300, 40));
			titleField.TextAlignment = UITextAlignment.Center;
			Add(titleField);
			/*			
			var speakerField = new UILabel(new RectangleF(10, 40, 300, 40));
			speakerField.TextAlignment = UITextAlignment.Center;
			Add(speakerField);

			var descriptionField = new UILabel(new RectangleF(10, 60, 300, 40));
			descriptionField.TextAlignment = UITextAlignment.Center;
			Add(descriptionField);

			var difficultyField = new UILabel(new RectangleF(10, 80, 300, 40));
			difficultyField.TextAlignment = UITextAlignment.Center;
			Add(difficultyField);

			var roomField = new UILabel(new RectangleF(10, 100, 300, 40));
			roomField.TextAlignment = UITextAlignment.Center;
			Add(roomField);

			var startField = new UILabel(new RectangleF(10, 120, 300, 40));
			startField.TextAlignment = UITextAlignment.Center;
			Add(startField);*/

			this.DelayBind (() =>{
				var set = this.CreateBindingSet<FullSessionDtoCell, FullSessionDto>();
				set.Bind(titleField).To(c=>c.title);
				/*				set.Bind(speakerField).To(c=>c.speakerNames);
				set.Bind(descriptionField).To(c=>c.startDescription);
				set.Bind(difficultyField).To(c=>c.speakerNames);
				set.Bind(roomField).To(c=>c.room);		
				set.Bind(startField).To(c=>c.start);
				*/
				set.Apply();
			});
		}
	}
}

