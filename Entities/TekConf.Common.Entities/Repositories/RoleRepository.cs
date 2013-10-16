using System;
using System.Linq;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Driver.Linq;

namespace TekConf.Common.Entities.Repositories
{
    public interface IRoleRepository : IRepository<RoleEntity>
    {

    }

    public class RoleRepository : IRoleRepository
    {
        private readonly IEntityConfiguration _entityConfiguration;

        public RoleRepository(IEntityConfiguration entityConfiguration)
        {
            this._entityConfiguration = entityConfiguration;
            CreateIndexes();
        }

        public void Save(RoleEntity entity)
        {
            var collection = MongoCollection();
            collection.Save(entity);
        }

        public IQueryable<RoleEntity> AsQueryable()
        {
            var collection = MongoCollection();
            return collection.AsQueryable();
        }

        private void CreateIndexes()
        {
            var collection = this.LocalDatabase.GetCollection<RoleEntity>("roles");
            collection.EnsureIndex(new string[] { "Name" });

        }
        public void Remove(Guid id)
        {
            var collection = this.LocalDatabase.GetCollection<RoleEntity>("roles");
            collection.Remove(Query.EQ("_id", id));
        }

        private MongoCollection<RoleEntity> MongoCollection()
        {
            var collection = this.LocalDatabase.GetCollection<RoleEntity>("roles");
            return collection;
        }

        private MongoServer _localServer;
        private MongoDatabase _localDatabase;
        private MongoDatabase LocalDatabase
        {
            get
            {
                if (_localServer == null)
                {
                    var mongoServer = this._entityConfiguration.MongoServer;
                    _localServer = MongoServer.Create(mongoServer);
                }

                if (_localDatabase == null)
                {
                    _localDatabase = _localServer.GetDatabase("tekconf");

                }
                return _localDatabase;
            }
        }
    }
}
