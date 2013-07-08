﻿using System;
using System.Collections.Generic;
using Cirrious.MvvmCross.Plugins.File;
using Cirrious.MvvmCross.Plugins.Messenger;
using Cirrious.MvvmCross.Plugins.Network.Reachability;
using Cirrious.MvvmCross.Plugins.Sqlite;
using TekConf.Core.Models;
using TekConf.Core.Repositories;
using TekConf.RemoteData.Dtos.v1;

namespace TekConf.Core.Services
{

	public class RemoteDataService : IRemoteDataService
	{
		private readonly IMvxFileStore _fileStore;
		private readonly ICacheService _cache;
		private readonly IAuthentication _authentication;
		private readonly ILocalScheduleRepository _localScheduleRepository;
		private readonly ILocalConferencesRepository _localConferencesRepository;
		private readonly ISQLiteConnectionFactory _factory;
		private readonly IMvxMessenger _messenger;
		private readonly IMvxReachability _reachability;

		public RemoteDataService(IMvxFileStore fileStore, ICacheService cache, IAuthentication authentication, ILocalScheduleRepository localScheduleRepository, 
																										ILocalConferencesRepository localConferencesRepository,
																										ISQLiteConnectionFactory factory, 
																										IMvxMessenger messenger)
		{
			_fileStore = fileStore;
			_cache = cache;
			_authentication = authentication;
			_localScheduleRepository = localScheduleRepository;
			_localConferencesRepository = localConferencesRepository;
			_factory = factory;
			_messenger = messenger;
			//_reachability = reachability;
			_reachability = null;
		}

		public void GetConferences(
			bool isRefreshing = false,
			string userName = null,
			string sortBy = "end",
			bool? showPastConferences = null,
			bool? showOnlyOpenCalls = null,
			bool? showOnlyOnSale = null,
			string search = null,
			string city = null,
			string state = null,
			string country = null,
			double? latitude = null,
			double? longitude = null,
			double? distance = null,
			Action<IEnumerable<ConferencesListViewDto>> success = null,
			Action<Exception> error = null)
		{
			ConferencesService.GetConferencesAsync(search, _fileStore, _localConferencesRepository, isRefreshing, _cache, success, error);
		}

		public void GetConferenceSessionsList(string slug, bool isRefreshing, Action<ConferenceSessionsListViewDto> success = null, Action<Exception> error = null)
		{
			ConferenceSessionsService.GetConferenceSessionsAsync(_fileStore, _localScheduleRepository, _localConferencesRepository, _reachability, slug, isRefreshing, null, _authentication, _factory, success, error);
		}

		//public void GetConferenceDetail(string slug, bool isRefreshing, Action<ConferenceDetailViewDto> success = null, Action<Exception> error = null)
		//{
		//	ConferenceService.GetConferenceDetailAsync(_fileStore, _localScheduleRepository, _localConferencesRepository, _reachability, slug, isRefreshing, _cache, _authentication, success, error);
		//}

		public void GetSchedule(string userName, string conferenceSlug, bool isRefreshing, Action<ScheduleDto> success = null, Action<Exception> error = null)
		{
			ScheduleService.GetScheduleAsync(_fileStore, _localScheduleRepository, userName, conferenceSlug, isRefreshing, _cache, success, error);
		}

		public void GetSchedules(string userName, bool isRefreshing, Action<IEnumerable<ConferencesListViewDto>> success = null, Action<Exception> error = null)
		{
			ScheduleService.GetSchedulesAsync(_fileStore, _localScheduleRepository, userName, isRefreshing, _cache, success, error);
		}

		public void LoginWithTekConf(string userName, string password, Action<bool, string> success = null, Action<Exception> error = null)
		{
			UserService.GetAuthenticationAsync(userName, password, _messenger, success, error);
		}

		public void AddToSchedule(string userName, string conferenceSlug, Action<ScheduleDto> success = null, Action<Exception> error = null)
		{
			ScheduleService.AddToScheduleAsync(_fileStore, _localScheduleRepository, userName, conferenceSlug, false, _cache, success, error);
		}

		public void AddSessionToSchedule(string userName, string conferenceSlug, string sessionSlug, Action<ScheduleDto> success = null, Action<Exception> error = null)
		{
			ScheduleService.AddSessionToScheduleAsync(_fileStore, _localScheduleRepository, userName, conferenceSlug, sessionSlug, false, _cache, success, error);
		}

		public void RemoveFromSchedule(string userName, string conferenceSlug, Action<ScheduleDto> success = null, Action<Exception> error = null)
		{
			ScheduleService.RemoveFromScheduleAsync(_fileStore, _localScheduleRepository, userName, conferenceSlug, false, _cache, success, error);
		}

		public void RemoveSessionFromSchedule(string userName, string conferenceSlug, string sessionSlug, Action<ScheduleDto> success = null, Action<Exception> error = null)
		{
			ScheduleService.RemoveSessionFromScheduleAsync(_fileStore, _localScheduleRepository, userName, conferenceSlug, sessionSlug, false, _cache, success, error);
		}

		//public void GetSession(string conferenceSlug, string sessionSlug, bool isRefreshing, Action<SessionDetailDto> success, Action<Exception> error)
		//{
		//	SessionService.GetSessionAsync(_fileStore, conferenceSlug, sessionSlug, isRefreshing, _localConferencesRepository, _factory, _cache, success, error);
		//}

		public void GetIsOauthUserRegistered(string userId, Action<string> success, Action<Exception> error)
		{
			UserService.GetIsOauthUserRegisteredAsync(userId, _messenger, success, error);
		}

		public void CreateOauthUser(string userId, string userName, Action<string> success, Action<Exception> error)
		{
			UserService.CreateOauthUserAsync(userId, userName, _messenger, success, error);
		}
	}
}