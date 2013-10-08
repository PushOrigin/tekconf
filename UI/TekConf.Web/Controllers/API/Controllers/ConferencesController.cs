namespace TekConf.Web.Controllers.API
{
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Http;
    using AutoMapper;
    using TekConf.Common.Entities;

    public class ConferencesController : ApiController
    {
        private readonly ITekConfContext _tekConfContext;

        private readonly IConferenceRepository _webConferenceRepository;

        public ConferencesController(ITekConfContext tekConfContext, IConferenceRepository webConferenceRepository)
        {
            _tekConfContext = tekConfContext;
            _webConferenceRepository = webConferenceRepository;
        }

        public void Delete(int id)
        {
        }

        public async Task<IEnumerable<ConferenceApiModel>> Get()
        {
            var conferenceEntities = await _tekConfContext.Conferences
                                                .Include(x => x.Sessions)
                                                .OrderBy(x => x.Slug)
                                                .Take(1000)
                                                .ToListAsync();

            var conferences = Mapper.Map<IEnumerable<ConferenceApiModel>>(conferenceEntities);

            return conferences;
        }

        public async Task<ConferenceApiModel> Get(string id)
        {
            var conferenceEntity = await _tekConfContext.Conferences.SingleOrDefaultAsync(x => x.Slug == id);

            var conferenceModel = Mapper.Map<ConferenceApiModel>(conferenceEntity);
            return conferenceModel;
        }

        public async Task<int> Post([FromBody] ConferenceApiModel value)
        {
            var fullConferences = _webConferenceRepository.GetConferences(search:null,sortBy:null,showPastConferences:true,showOnlyWithOpenCalls:false,showOnlyFeatured:false,showOnlyOnSale:false,longitude:null,latitude:null,distance:null,city:null,state:null,country:null);

            foreach (var fullConference in fullConferences)
            {
                if (!_tekConfContext.Conferences.Any(x => x.Slug == fullConference.slug))
                {
                    var conferenceEntity = Mapper.Map<ConferenceEntity>(fullConference);
                    _tekConfContext.Conferences.Add(conferenceEntity);
                }
            }

            return await _tekConfContext.SaveChangesAsync();
        }

        public void Put(int id, [FromBody] string value)
        {
        }
    }
}