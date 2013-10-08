namespace UberImporter.Importers.MonkeySpace2012
{
    using System;
    using System.Linq;
    using System.Net;

    using AutoMapper;

    using MongoDB.Driver.Linq;

    using ServiceStack.Text;

    using TekConf.Common.Entities;

    using TinyMessenger;

    public class Cododn2013Importer
    {
        public void Import()
        {
            var connection = new MongoDbConnection();
            var collection = connection.RemoteDatabase.GetCollection<ConferenceEntity>("conferences");
            var sessionsUrl = "http://cododn.azurewebsites.net/api/sessions";
            var speakersUrl = "http://cododn.azurewebsites.net/api/presenters";
            var request = new WebClient();
            var sessionsJson = request.DownloadString(sessionsUrl);
            var speakersJson = request.DownloadString(speakersUrl);
            var sessionsRoot = sessionsJson.FromJson<SessionRoot>();
            var speakersRoot = speakersJson.FromJson<SpeakerRoot>();

            if (sessionsRoot != null)
            {
                var conference = collection.AsQueryable().SingleOrDefault(c => c.slug == "central-ohio-day-of-net-2013");
                if (conference == null)
                {
                    ITinyMessengerHub hub = new TinyMessengerHub();
                    IEntityConfiguration configuration = new EntityConfiguration();
                    var repository = new ConferenceRepository(configuration);
                    conference = new ConferenceEntity(hub, repository) { };
                }
                else
                {
                    foreach (var session in sessionsRoot.Sessions)
                    {
                        var sessionEntity =
                            conference.sessions.SingleOrDefault(s => s.slug == session.Title.GenerateSlug());
                        if (sessionEntity == null)
                        {
                            sessionEntity = Mapper.Map<SessionEntity>(session);
                            if (sessionEntity.start == default(DateTime))
                            {
                                if (conference.start != null)
                                {
                                    sessionEntity.start = conference.start.Value;
                                }
                            }
                            if (sessionEntity.end == default(DateTime))
                            {
                                if (conference.start != null)
                                {
                                    sessionEntity.end = conference.start.Value.AddHours(1);
                                }
                            }
                            sessionEntity.slug = session.Title.GenerateSlug();
                            conference.AddSession(sessionEntity);
                        }
                        sessionEntity.slug = session.Title.GenerateSlug();
                        var speakerJson = speakersRoot.Speakers.SingleOrDefault(s => s.SpeakerUrl == session.SpeakerUrl);
                        var speakerEntity = Mapper.Map<SpeakerEntity>(speakerJson);
                        speakerEntity.slug = speakerEntity.fullName.ToLower().GenerateSlug();
                        if (sessionEntity.speakers.All(x => x.slug != speakerEntity.slug))
                        {
                            conference.AddSpeakerToSession(sessionEntity.slug, speakerEntity);
                        }
                    }
                }
                conference.Save();
            }
        }

        public class SessionRoot
        {
            public Session[] Sessions { get; set; }
        }

        public class Session
        {
            public string Id { get; set; }
            public string Title { get; set; }
            public string Abstract { get; set; }
            public object Room { get; set; }
            public string[] Tags { get; set; }
            public string SessionUrl { get; set; }
            public string SpeakerUrl { get; set; }
        }

        public class SpeakerRoot
        {
            public Speaker[] Speakers { get; set; }
        }

        public class Speaker
        {
            public string Id { get; set; }
            public string Name { get; set; }
            public string Bio { get; set; }
            public string SpeakerUrl { get; set; }
            public string[] SessionUrls { get; set; }
        }
    }
}