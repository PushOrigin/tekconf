using System;
using Cirrious.MvvmCross.ViewModels;
using TekConf.RemoteData.Dtos.v1;
using System.Collections.Generic;
using TekConf.Core.ViewModels;

namespace TekConf.Core
{
	public class ConferenceFavoriteSessionsViewModel : MvxViewModel
	{
		private ConferenceSessionsViewModel _sessionsVm;

		public ConferenceFavoriteSessionsViewModel (ConferenceSessionsViewModel sessionsVm)
		{
			_sessionsVm = sessionsVm;
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
				RaisePropertyChanged("Schedule");
			}
		}

		public List<FullSessionDto> FavoriteSessions 
		{
			get { return Schedule.sessions; }
			set 
			{ 
				Schedule.sessions = value; 
				RaisePropertyChanged("FavoriteSessions");
			}
		}
	}
}

