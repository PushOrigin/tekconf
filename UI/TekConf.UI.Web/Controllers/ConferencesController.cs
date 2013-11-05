using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Web.Http;
using TekConf.Core.Data.Entities;

namespace TekConf.UI.Web.Controllers
{
	public class ConferencesController : ApiController
	{
		public async Task<IEnumerable<Conference>> Get()
		{
			List<Conference> conferences = null;
			using (var context = new TekConfDbContext())
			{
				conferences = await context.Conferences.ToListAsync();
			}

			return conferences;
		}

		public async Task<Conference> Get(int id)
		{
			Conference conference = null;
			using (var context = new TekConfDbContext())
			{
				conference = await context.Conferences.SingleOrDefaultAsync(x => x.Id == id);
			}

			return conference;
		}

		// POST api/conferences
		public void Post([FromBody]string value)
		{
		}

		// PUT api/conferences/5
		public void Put(int id, [FromBody]string value)
		{
		}

		// DELETE api/conferences/5
		public void Delete(int id)
		{
		}
	}
}
