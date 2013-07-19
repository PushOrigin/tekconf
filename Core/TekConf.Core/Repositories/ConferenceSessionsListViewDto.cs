using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TekConf.Core.Entities;
using TekConf.RemoteData.Dtos.v1;

namespace TekConf.Core.Repositories
{
	public class ConferenceSessionsListViewDto
	{

		public ConferenceSessionsListViewDto(IEnumerable<SessionEntity> sessions)
		{
			if (sessions != null)
			{
				Sessions = sessions.Select(x => new ConferenceSessionListDto(x));
			}
			else
			{
				Sessions = new List<ConferenceSessionListDto>();
			}
		}

		public ConferenceSessionsListViewDto(IEnumerable<FullSessionDto> sessions)
		{
			if (sessions != null)
			{
				Sessions = sessions.Select(x => new ConferenceSessionListDto(x));
			}
			else
			{
				Sessions = new List<ConferenceSessionListDto>();
			}
		}

		public string name { get; set; }
		public string slug { get; set; }

		public IEnumerable<ConferenceSessionListDto> Sessions { get; set; }

		public List<ConferenceSessionListDto> SessionsByTime
		{
			get
			{
				return Sessions == null ? new List<ConferenceSessionListDto>() : Sessions.OrderBy(x => x.start).ThenBy(t => t.title).ToList();
			}
		}

		public List<ConferenceSessionListDto> SessionsByTitle
		{
			get { return Sessions == null ? new List<ConferenceSessionListDto>() : Sessions.OrderBy(x => x.title).ToList(); }
		}

		public List<ConferenceSessionListDto> SessionsBySpeaker
		{
			get { return Sessions == null ? new List<ConferenceSessionListDto>() : Sessions.OrderBy(x => x.speakerNames).ThenBy(t => t.title).ToList(); }
		}

		public List<ConferenceSessionListDto> SessionsByTag
		{
			get { return Sessions == null ? new List<ConferenceSessionListDto>() : Sessions.OrderBy(x => x.tags.OrderBy(s => s).FirstOrDefault()).ThenBy(t => t.title).ToList(); }
		}

		public List<ConferenceSessionListDto> SessionsByRoom
		{
			get { return Sessions == null ? new List<ConferenceSessionListDto>() : Sessions.OrderBy(x => x.room).ThenBy(t => t.title).ToList(); }
		}
	}
}