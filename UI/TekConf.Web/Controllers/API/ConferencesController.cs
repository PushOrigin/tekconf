

namespace TekConf.Web.Controllers.API
{
    //using System.Collections.Generic;
    ///using System.Data.Entity;
    //using System.Threading.Tasks;
    //using System.Web.Http;

    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Threading.Tasks;
    using System.Web.Http;

    using AutoMapper;

    using TekConf.Common.Entities;

    using ApiController = TekConf.Web.Controllers.ApiController;

    public class DbContext : System.Data.Entity.DbContext
    {
        public DbContext() : base("TekConf") { }
        public DbSet<ConferenceEntity> Conferences { get; set; }
    }

    public class Conference
    {
        
    }

    public class ConferencesController : ApiController
    {
        public async Task<IEnumerable<Conference>> Get()
        {
            List<ConferenceEntity> conferenceEntities;
            using (var db = new DbContext())
            {
                conferenceEntities = await db.Conferences.ToListAsync();
            }

            var conferences = Mapper.Map<IEnumerable<Conference>>(conferenceEntities);
            return conferences;
        }

        public string Get(int id)
        {
            return "value";
        }

        public void Post([FromBody]string value)
        {
        }

        public void Put(int id, [FromBody]string value)
        {
        }

        public void Delete(int id)
        {
        }
    }
}