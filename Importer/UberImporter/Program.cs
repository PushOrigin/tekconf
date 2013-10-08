using UberImporter.Importers.MonkeySpace2012;

namespace UberImporter
{
    using AutoMapper;

    using TekConf.Common.Entities;

    class Program
    {
        static void Main(string[] args)
        {
            BootstrapAutoMapper();
            //MongoDbConnection connection = new MongoDbConnection();
            //var collection = connection.RemoteDatabase.GetCollection<ConferenceEntity>("conferences");

            //var slugs = new List<string>()
            //                {
            //                    "MonkeySpace-2012",
            //                    "RailsConf-2012",
            //                    "CodeStock-2012",
            //                    "1DevDayDetroit-2012"
            //                };
            //foreach (var slug in slugs)
            //{
            //    IMongoQuery query1 = Query.EQ("slug", slug);
            //    collection.Remove(query1);

            //    IMongoQuery query2 = Query.EQ("slug", slug.ToLower());
            //    collection.Remove(query2);
            //}


            var cododn2013 = new Cododn2013Importer();
            //var monkeySpace = new MonkeySpaceImporter();
            //var railsConf = new RailsConfImporter();
            //var codestock = new CodeStockImporter();
            //var oneDevDayDetroit = new OneDevDayDetroitImporter();
            //var codeMash2013 = new CodeMash2013Importer();

            //oneDevDayDetroit.Import();
            cododn2013.Import();
            //railsConf.Import();
            //codestock.Import();
            //codeMash2013.Import();
        }

        private static void BootstrapAutoMapper()
        {
            Mapper.CreateMap<Cododn2013Importer.Session, SessionEntity>()
                .ForMember(x => x.description, opt => opt.MapFrom(s => s.Abstract))
                .ForMember(x => x.room, opt => opt.MapFrom(s => s.Room))
                .ForMember(x => x.title, opt => opt.MapFrom(s => s.Title))
                .ForMember(x => x._id, opt => opt.Ignore())
                ;

            Mapper.CreateMap<Cododn2013Importer.Speaker, SpeakerEntity>()
                .ForMember(x => x.description, opt => opt.MapFrom(s => s.Bio))
                .ForMember(x => x.firstName, opt => opt.MapFrom(s => s.Name.Split(' ')[0]))
                .ForMember(x => x.lastName, opt => opt.MapFrom(s => s.Name.Split(' ')[1]))
                .ForMember(x => x._id, opt => opt.Ignore())
                ;
        }
    }




}
