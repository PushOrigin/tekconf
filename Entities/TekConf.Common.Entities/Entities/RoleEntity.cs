namespace TekConf.Common.Entities
{
    using System;

    using MongoDB.Bson.Serialization.Attributes;
    using MongoDB.Bson.Serialization.IdGenerators;

    public class RoleEntity : IEntity
    {
        [BsonId(IdGenerator = typeof(CombGuidGenerator))]
        public Guid _id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
    }
}