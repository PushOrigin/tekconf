using System;
using Cirrious.MvvmCross.ViewModels;
using TekConf.Core.ViewModels;
using System.Collections.Generic;
using TekConf.Core.Repositories;
using Cirrious.MvvmCross.Plugins.Messenger;
using TekConf.Core.Messages;

namespace TekConf.Core
{
	public class ConferenceFavoriteListViewModel : MvxViewModel
	{
		private ConferencesListViewModel _conferencesListVm;
		private MvxSubscriptionToken _favoritesUpdatedMessageToken;

		public ConferenceFavoriteListViewModel (IMvxMessenger messenger, ConferencesListViewModel conferencesListVm)
		{
			_conferencesListVm = conferencesListVm;
			_favoritesUpdatedMessageToken = messenger.Subscribe<FavoriteConferencesUpdatedMessage>(OnFavoritesUpdatedMessage);
		}

		private void OnFavoritesUpdatedMessage(FavoriteConferencesUpdatedMessage message)
		{
			RaisePropertyChanged (() => Favorites);
		}

		public ConferencesListViewModel ConferencesListVm { get { return _conferencesListVm; } }

		public List<ConferencesListViewDto> Favorites
		{
			get { return _conferencesListVm.Favorites; }
			set { throw new InvalidOperationException (); }
		}

	}
}

