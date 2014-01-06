using System;
using Cirrious.MvvmCross.ViewModels;
using TekConf.RemoteData.Dtos.v1;
using System.Collections.Generic;
using TekConf.Core.ViewModels;
using Cirrious.MvvmCross.Plugins.Messenger;
using Cirrious.CrossCore;
using TekConf.Core.Messages;
using System.Windows.Input;
using TekConf.Core.Repositories;

namespace TekConf.Core
{
	public class ConferenceFavoriteSessionsViewModel : MvxViewModel
	{
		private ConferenceSessionsViewModel _sessionsVm;
		private MvxSubscriptionToken _favoritesUpdatedMessageToken;

		public ConferenceFavoriteSessionsViewModel (ConferenceSessionsViewModel sessionsVm)
		{
			_sessionsVm = sessionsVm;
			var messenger = Mvx.Resolve<IMvxMessenger>();
			_favoritesUpdatedMessageToken = messenger.Subscribe<FavoriteSessionAddedMessage>(OnFavoritesUpdatedMessage);
		}

		private void OnFavoritesUpdatedMessage(FavoriteSessionAddedMessage message) 
		{
			RaisePropertyChanged (() => FavoriteSessions);
		}

		private ScheduleDto _schedule;
		public ScheduleDto Schedule
		{
			get
			{
				return _sessionsVm.Schedule;
			}
			set
			{
				_sessionsVm.Schedule = value;
				RaisePropertyChanged (() => Schedule);
			}
		}

		public List<FullSessionDto> FavoriteSessions 
		{
			get { return Schedule.sessions; }
			set 
			{ 
				Schedule.sessions = value; 
				RaisePropertyChanged (() => FavoriteSessions);
			}
		}

		public ICommand ShowSessionDetailCommand
		{
			get
			{
				return new MvxCommand<FullSessionDto>(dto => 
					ShowViewModel<SessionDetailViewModel>(
						new SessionDetailViewModel.Navigation() {
							ConferenceSlug =  _sessionsVm.Conference.slug,
							SessionSlug = dto.slug
						} 
					)
				);
			}
		}

	}
}

