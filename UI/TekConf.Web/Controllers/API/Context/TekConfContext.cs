namespace TekConf.Web.Controllers.API
{
    using System.Data.Entity;
    using System.Threading.Tasks;

    public interface ITekConfContext
    {
        IDbSet<ConferenceEntity> Conferences { get; set; }
        IDbSet<SessionEntity> Sessions { get; set; }

        int SaveChanges();

        Task<int> SaveChangesAsync();
    }

    public class TekConfContext : DbContext, ITekConfContext
    {
        public TekConfContext() : base("TekConf") { }

        public IDbSet<ConferenceEntity> Conferences { get; set; }
        public IDbSet<SessionEntity> Sessions { get; set; }

    }
}