using System;
using Cirrious.MvvmCross.Touch.Views;
using MonoTouch.UIKit;
using System.Drawing;
using Cirrious.MvvmCross.Binding.BindingContext;
using TekConf.Core.ViewModels;
using FlyoutNavigation;
using Cirrious.MvvmCross.Plugins.Messenger;
using Cirrious.CrossCore;
using TekConf.Core.Messages;
using TekConf.Core.Services;

namespace TekConf.iOS
{
	public class SessionDetailView : MvxViewController
	{
		private UIButton _favoriteButton;
		private MvxSubscriptionToken _sessionDetailExceptionToken;

		public SessionDetailView ()
		{
			var messenger = Mvx.Resolve<IMvxMessenger> ();
			_sessionDetailExceptionToken = messenger.Subscribe<SessionDetailExceptionMessage> (message => {
				if (message.ExceptionObject.Message == "The remote server returned an error: NotFound.") {
					const string errorMessage = "Could not connect to remote server. Please check your network connection and try again.";
					var mb = Mvx.Resolve<IMessageBox> ();
					mb.Show (errorMessage);
				}
			});
		}

		private SessionDetailViewModel VM { get { return ViewModel as SessionDetailViewModel; } }

		public override void ViewDidLoad ()
		{
			View = new UIView () { BackgroundColor = UIColor.White };
			base.ViewDidLoad ();

			var titleField = new UILabel (new RectangleF (10, 20, 300, 40));
			titleField.TextAlignment = UITextAlignment.Center;
			Add (titleField);

			var speakerField = new UILabel (new RectangleF (10, 40, 300, 40));
			speakerField.TextAlignment = UITextAlignment.Center;
			Add (speakerField);

			_favoriteButton = UIButton.FromType (UIButtonType.RoundedRect);
			_favoriteButton.Frame = new RectangleF (5, 300, 310, 40);
			Add (_favoriteButton);
			RefreshFavoriteIcon ();
			_favoriteButton.TouchUpInside += (object sender, EventArgs e) => {
				if (VM != null)
					VM.AddFavoriteCommand.Execute(VM.Session.slug);
				RefreshFavoriteIcon();
			};

			var descriptionField = new UILabel (new RectangleF (10, 60, 300, 40));
			descriptionField.TextAlignment = UITextAlignment.Center;
			Add (descriptionField);

			var difficultyField = new UILabel (new RectangleF (10, 80, 300, 40));
			difficultyField.TextAlignment = UITextAlignment.Center;
			Add (difficultyField);

			var roomField = new UILabel (new RectangleF (10, 100, 300, 40));
			roomField.TextAlignment = UITextAlignment.Center;
			Add (roomField);

			var startField = new UILabel (new RectangleF (10, 120, 300, 40));
			startField.TextAlignment = UITextAlignment.Center;
			Add (startField);

			var set = this.CreateBindingSet<SessionDetailView, SessionDetailViewModel> ();
			set.Bind (titleField).To (c => c.Session.title);
			//set.Bind(speakerField).To(c=>c.Session.speakers);
			set.Bind (descriptionField).To (c => c.Session.description);
			//set.Bind(difficultyField).To(c=>c.Session.difficulty);
			set.Bind (roomField).To (c => c.Session.room);		
			set.Bind (startField).To (c => c.Session.startDescription);
			set.Apply ();
		}

		private void RefreshFavoriteIcon()
		{
			if (VM != null)
			{
				string imageUrl = "Images/appbar.heart.png";
				string imgText = "Add To Favorites";
				if (VM.Session != null)
				{
					if (VM.Session.isAddedToSchedule == true)
					{
						imageUrl = "Images/appbar.heart.cross.png";
						imgText = "Remove from Favorites";
					}
				}
				_favoriteButton.SetImage (UIImage.FromFile (imageUrl), UIControlState.Normal);
				_favoriteButton.SetTitle (imgText, UIControlState.Normal);
			}
		}
	}
}

