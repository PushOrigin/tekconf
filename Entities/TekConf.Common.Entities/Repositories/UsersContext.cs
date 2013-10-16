namespace TekConf.Common.Entities
{
    using System.Data.Entity;

    public class UsersContext : DbContext
    {
        public UsersContext()
            : base("DefaultConnection")
        {
        }

        public DbSet<AspNetUsers> AspNetUsers { get; set; }
    }

    public class AspNetUsers
    {
        public virtual string Id { get; set; }
        public virtual string UserName { get; set; }
    }
}