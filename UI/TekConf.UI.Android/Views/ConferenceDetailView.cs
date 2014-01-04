using Android.Widget;
using Android.Content;
using Android.Graphics;
using Android.Views;
using Cirrious.MvvmCross.Binding.Droid.Views;
using TekConf.Core.ViewModels;
using Cirrious.MvvmCross.Binding.BindingContext;
using Cirrious.MvvmCross.Binding.Droid.BindingContext;
using System.Collections.Generic;
using System;
using Android.Graphics.Drawables;
using Cirrious.MvvmCross.Plugins.Messenger;
using Cirrious.CrossCore;
using TekConf.Core.Messages;

namespace TekConf.UI.Android.Views
{
	using Cirrious.MvvmCross.Droid.Views;
	using global::Android.App;
	using global::Android.OS;

	[Activity (Label = "Detail")]
	public class ConferenceDetailView : MvxActivity
	{
		private MvxSubscriptionToken _favoriteRefreshMessageToken;

		protected override void OnCreate (Bundle bundle)
		{
			RequestWindowFeature (WindowFeatures.ActionBar);

			base.OnCreate (bundle);

			SetContentView (Resource.Layout.ConferenceDetailView);

			var set = this.CreateBindingSet<ConferenceDetailView, ConferenceDetailViewModel> ();
			set.Apply ();

			ActionBar.SetBackgroundDrawable (new ColorDrawable (new Color (r: 129, g: 153, b: 77)));
			ActionBar.SetDisplayShowHomeEnabled (false);

			Setup.CurrentActivityContext = (Context)this;
			var messenger = Mvx.Resolve<IMvxMessenger> ();

			_favoriteRefreshMessageToken = messenger.Subscribe<RefreshConferenceFavoriteIconMessage> (
				message => RunOnUiThread (() => RefreshFavoriteIcon ())
			);
		}

		public override bool OnCreateOptionsMenu (IMenu menu)
		{
			MenuInflater.Inflate (Resource.Menu.ConferenceDetailActionItems, menu);
			return true;
		}

		public override bool OnPrepareOptionsMenu (IMenu menu)
		{
			var menuItem = menu.GetItem (1);
			if (isFavorited) {
				menuItem.SetIcon (Resource.Drawable.heart_icon24);
			} else {
				menuItem.SetIcon (Resource.Drawable.heart_empty_icon24);
			}
			return base.OnPrepareOptionsMenu (menu);
		}

		private bool isFavorited { get; set; }

		public override bool OnOptionsItemSelected (IMenuItem item)
		{
			var vm = this.DataContext as ConferenceDetailViewModel;
			if (vm != null) {
				switch (item.ToString ()) {
				case "Sessions":
					vm.ShowSessionsCommand.Execute (vm.Conference.slug);
					break;
				case "Favorite":
					vm.AddFavoriteCommand.Execute (null);
					break;
				case "Refresh":
					vm.Refresh (vm.Conference.slug);
					break;
				case "Settings":
					vm.ShowSettingsCommand.Execute (null);
					break;
				}
			}

			return false;
		}

		private void RefreshFavoriteIcon ()
		{
			isFavorited = false;
			var vm = DataContext as ConferenceDetailViewModel;
			if (vm != null) {

				if (vm.Conference != null) {
					isFavorited |= vm.Conference.isAddedToSchedule == true;
				}
			}

			InvalidateOptionsMenu ();
		}
	}
}