using System.Collections.Generic;
using TekConf.RemoteData.Dtos.v1;

namespace TekConf.Core.Repositories
{
	public interface ILocalScheduleRepository
	{
		ConferenceFavoriteSessionDto NextScheduledSession { get; }
		void SaveSchedules(IEnumerable<FullConferenceDto> scheduledConferences);
		IEnumerable<ConferencesListViewDto> GetConferencesList();
		void SaveScheduleDetail(ScheduleDto schedule);
	}
}