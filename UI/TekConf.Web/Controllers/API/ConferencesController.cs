namespace TekConf.Web.Controllers.API
{

    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Http;

    using AutoMapper;

    using Ploeh.AutoFixture;

    using TekConf.Common.Entities;

    public class TekConfContext : System.Data.Entity.DbContext
    {
        public TekConfContext()
            : base("TekConf")
        {
        }

        public DbSet<ConferenceEntity> Conferences { get; set; }
        public DbSet<SessionEntity> Sessions { get; set; }
    }

    public class Conference
    {
        public string Slug { get; set; }
    }

    public class ConferencesController : ApiController
    {
        public void Delete(int id)
        {
        }

        public async Task<IEnumerable<Conference>> Get()
        {
            //List<ConferenceEntity> conferenceEntities;
            IEnumerable<Conference> conferences;
            using (var db = new TekConfContext())
            {
                conferences = await db.Conferences.OrderBy(x => x.Slug).Take(1000).Select(x => new Conference { Slug = x.Slug }).ToListAsync();
            }

            //var conferences = Mapper.Map<IEnumerable<Conference>>(conferenceEntities);
            return conferences;
        }

        public string Get(int id)
        {
            return "value";
        }

        public async Task<int> Post([FromBody] Conference value)
        {
            var fixture = new Fixture();
            int returnValue;
            using (var db = new TekConfContext())
            {
                var conference = new ConferenceEntity() { Address = new AddressEntity(), Slug = fixture.Create<string>() };
                db.Conferences.Add(conference);
                returnValue = await db.SaveChangesAsync();
            }

            return returnValue;
        }

        public void Put(int id, [FromBody] string value)
        {
        }
    }
}