using Android.Views;
using Cirrious.MvvmCross.Binding.BindingContext;
using TekConf.Core.ViewModels;
using Android.Graphics.Drawables;
using Android.Graphics;
using Android.Content;
using Cirrious.MvvmCross.Plugins.Messenger;
using Cirrious.CrossCore;
using TekConf.Core.Messages;

namespace TekConf.UI.Android.Views
{
	using Cirrious.MvvmCross.Droid.Views;

	using global::Android.App;
	using global::Android.OS;

	[Activity(Label = "Session Detail")]
	public class SessionDetailView : MvxActivity
	{
		protected override void OnCreate(Bundle bundle)
		{
			RequestWindowFeature(WindowFeatures.ActionBar);

			base.OnCreate(bundle);

			SetContentView(Resource.Layout.SessionDetailView);

			var set = this.CreateBindingSet<SessionDetailView, SessionDetailViewModel>();
			set.Apply();

			ActionBar.SetBackgroundDrawable(new ColorDrawable(new Color(r:129,g:153,b:77)));
			ActionBar.SetDisplayShowHomeEnabled(false);

			Setup.CurrentActivityContext = (Context)this;

			var messenger = Mvx.Resolve<IMvxMessenger> ();

			_favoriteRefreshMessageToken = messenger.Subscribe<RefreshSessionFavoriteIconMessage> (
				message => RunOnUiThread (() => RefreshFavoriteIcon ())
			);
		}

		private void RefreshFavoriteIcon ()
		{
			isFavorited = false;
			var vm = DataContext as SessionDetailViewModel;
			if (vm != null) {

				if (vm.Session != null) {
					isFavorited |= vm.Session.isAddedToSchedule == true;
				}
			}

			InvalidateOptionsMenu ();
		}

		private MvxSubscriptionToken _favoriteRefreshMessageToken;

		public override bool OnCreateOptionsMenu(IMenu menu)
		{
			MenuInflater.Inflate(Resource.Menu.SessionDetailActionItems, menu);
			return true;
		}

		public override bool OnOptionsItemSelected(IMenuItem item)
		{
			var vm = this.DataContext as SessionDetailViewModel;
			if (vm != null) 
			{
				switch (item.ToString ()) 
				{
					case "Favorite":
						vm.AddFavoriteCommand.Execute(null);
						break;
					case "Speakers":
						//TODO
						break;
					case "Refresh":
						var navigation = new SessionDetailViewModel.Navigation () 
						{
							ConferenceSlug = vm.ConferenceSlug,
							SessionSlug = vm.Session.slug
						};
						vm.Refresh (navigation);
						break;

					case "Settings":
						vm.ShowSettingsCommand.Execute (null);
						break;
				}
			}

			return false;
		}
	
		private bool isFavorited;
		public override bool OnPrepareOptionsMenu (IMenu menu)
		{
			var menuItem = menu.GetItem (1);
			if (isFavorited)
			{
				menuItem.SetIcon(Resource.Drawable.heart_icon24);
			}
			else
			{
				menuItem.SetIcon(Resource.Drawable.heart_empty_icon24);
			}

			return base.OnPrepareOptionsMenu (menu);
		}	
	
	}
}