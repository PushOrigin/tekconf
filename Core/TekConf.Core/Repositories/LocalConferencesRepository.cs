using System;
using System.Collections.Generic;
using Cirrious.MvvmCross.Plugins.Sqlite;
using TekConf.Core.Entities;
using System.Linq;

namespace TekConf.Core.Repositories
{
	using System.Threading.Tasks;

	public class LocalConferencesRepository : ILocalConferencesRepository
	{
		private readonly ISQLiteConnection _connection;
		
		public LocalConferencesRepository(ISQLiteConnection connection)
		{
			_connection = connection;
			_connection.CreateTable<ConferenceEntity>();
			_connection.CreateTable<SessionEntity>();
		}

		public int Save(string conferenceSlug, SessionEntity session)
		{
			int sessionId;
			var conference = _connection.Table<ConferenceEntity>().FirstOrDefault(x => x.Slug == conferenceSlug);
			var conferenceId = conference.Id;
			var sessionEntity = _connection.Table<SessionEntity>().Where(x => x.ConferenceId == conferenceId).FirstOrDefault(x => x.Slug == session.Slug);
			
			if (sessionEntity != null)
			{
				session.Id = sessionEntity.Id;
				sessionId = _connection.Update(session);
			}
			else
			{
				sessionId = _connection.Insert(session);
			}

			return sessionId;
		}

		public void Save(IList<ConferenceEntity> conferences)
		{
			if (conferences != null)
			{
				foreach (var conference in conferences)
				{
					Save(conference);
				}
			}
		}

		public int Save(ConferenceEntity conference)
		{
			int conferenceId  = 0 ;
			var conferenceEntity = _connection.Table<ConferenceEntity>().FirstOrDefault(x => x.Slug == conference.Slug);
			if (conferenceEntity == null)
			{
				_connection.Insert(conference);
				var entity = _connection.Table<ConferenceEntity>().FirstOrDefault(x => x.Slug == conference.Slug);
				if (entity != null)
				{
					conferenceId = entity.Id;
				}
			}
			else
			{
				//_connection.Delete(conferenceEntity);
				//var conferenceId = _connection.Insert(conference);
				_connection.Update(conference);
				var entity = _connection.Table<ConferenceEntity>().FirstOrDefault(x => x.Slug == conference.Slug);
				if (entity != null)
				{
					conferenceId = entity.Id;
				}
			}

			return conferenceId;
		}

		public void AddSession(SessionEntity session)
		{
			if (session != null && session.ConferenceId != default(int))
			{
				var sessionEntity = _connection.Table<SessionEntity>().Where(x => x.Id == session.Id).FirstOrDefault(x => x.Slug == session.Slug);
				if (sessionEntity == null)
				{
					_connection.Insert(session);
				}
				else
				{
					_connection.Delete(session);
					_connection.Insert(session);
				}
			}
		}

		public SessionEntity Get(string conferenceSlug, string sessionSlug)
		{
			SessionEntity returnValue = null;
			var conference = _connection.Table<ConferenceEntity>().FirstOrDefault(x => x.Slug == conferenceSlug);
			returnValue = _connection.Table<SessionEntity>().Where(x => x.ConferenceId == conference.Id).FirstOrDefault(x => x.Slug == sessionSlug);
			return returnValue;
		}

		public ConferenceEntity Get(string conferenceSlug)
		{
			ConferenceEntity conference = null;
			conference = _connection.Table<ConferenceEntity>().FirstOrDefault(x => x.Slug == conferenceSlug);
			return conference;
		}
		private IList<ConferenceEntity> GetFavorites()
		{
			var conferences = _connection.Table<ConferenceEntity>().Where(x => x.IsAddedToSchedule).ToList();

			return conferences;
		}

		public Task<IList<ConferenceEntity>> ListFavoritesAsync()
		{
			var taskSource = new TaskCompletionSource<IList<ConferenceEntity>>();
			taskSource.SetResult(GetFavorites());
			return taskSource.Task;
		}

		private IList<SessionEntity> ListFavoriteSessions(string conferenceSlug)
		{
			var conference = _connection.Table<ConferenceEntity>().FirstOrDefault(x => x.Slug == conferenceSlug);
			var conferenceId = conference.Id;
			var sessions = _connection.Table<SessionEntity>().Where(x => x.ConferenceId == conferenceId).Where(x => x.IsAddedToSchedule).ToList();

			return sessions;
		}
		public Task<IList<SessionEntity>> ListFavoriteSessionsAsync(string conferenceSlug)
		{
			var taskSource = new TaskCompletionSource<IList<SessionEntity>>();
			taskSource.SetResult(ListFavoriteSessions(conferenceSlug));
			return taskSource.Task;
		}

		private IList<ConferenceEntity> List()
		{
			var filterDate = DateTime.Now.Date.AddDays(-1);
			var conferences = _connection.Table<ConferenceEntity>().Where(x => x.End >= filterDate).OrderBy(x => x.Start).ToList();
			return conferences;
		}

		public Task<IList<ConferenceEntity>> ListAsync()
		{
			var taskSource = new TaskCompletionSource<IList<ConferenceEntity>>();
			taskSource.SetResult(List());
			return taskSource.Task;
		}
	}
}