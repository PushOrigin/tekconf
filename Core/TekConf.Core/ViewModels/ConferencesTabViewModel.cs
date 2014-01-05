using System;
using Cirrious.MvvmCross.ViewModels;
using TekConf.Core.ViewModels;
using Cirrious.MvvmCross.Plugins.Messenger;

namespace TekConf.Core
{
	public class ConferencesTabViewModel : MvxViewModel
	{
		private ConferencesListViewModel _conferenceListViewMiodel;
		private ConferenceFavoriteListViewModel _conferenceFavoritesVm;

		public ConferencesTabViewModel (IMvxMessenger messenger,ConferencesListViewModel conferenceListViewMiodel)
		{
			_conferenceListViewMiodel = conferenceListViewMiodel;
			_conferenceFavoritesVm = new ConferenceFavoriteListViewModel (messenger, _conferenceListViewMiodel);
		}

		public ConferencesListViewModel ConferencesListVm 
		{
			get { return _conferenceListViewMiodel; }
			set { _conferenceListViewMiodel = value; }
		}
			
		public ConferenceFavoriteListViewModel ConferenceFavoritesVm 
		{
			get { return _conferenceFavoritesVm; }
			set { _conferenceFavoritesVm = value; }
		}
		 
	}
}

