﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using FluentMongo.Linq;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using ServiceStack.Text;
using TekConf.Common.Entities;
using TinyMessenger;

namespace UberImporter.Importers.MonkeySpace2012
{
    public class MonkeySpaceImporter
    {
        public void Import()
        {
            MongoDbConnection connection = new MongoDbConnection();
            var collection = connection.RemoteDatabase.GetCollection<ConferenceEntity>("conferences");

            var url = "http://monkeyspace.org/data/schedule.json";
            var request = new WebClient();
            var json = request.DownloadString(url);
            var root = json.FromJson<root>();
            if (root != null)
            {
                if (!collection.AsQueryable().Any(c => c.slug == "monkeyspace-2013"))
                {
                    ITinyMessengerHub hub = new TinyMessengerHub();
                    IEntityConfiguration configuration = new EntityConfiguration();
                    var repository = new ConferenceRepository(configuration);
                    var conference = new ConferenceEntity(hub, repository)
                    {
                        //_id = Guid.NewGuid(),
                        _id = Guid.NewGuid(),
                        dateAdded = DateTime.Now,
                        defaultTalkLength = 60,

                        description = @"MonkeySpace, formerly known as Monospace, is the official cross platform and open-source .NET conference. Want to learn more about developing for the iPhone, Android, Mac, and *nix platforms using .NET technologies? How about developing games or learning more about open-source projects using .NET technologies? MonkeySpace has provided an annual venue to collaborate, share, and socialize around these topics and more.",
                        start = new DateTime(2013, 07, 22),
                        end = new DateTime(2013, 07, 25),
                        registrationOpens = DateTime.Now.AddDays(-1),
                        registrationCloses = new DateTime(2013, 07, 25),
                        slug = "monkeyspace-2013",
                        facebookUrl = "",
                        homepageUrl = "http://monkeyspace.org",
                        imageUrl = "/img/conferences/MonkeySpace.jpg",
                        lanyrdUrl = "",
                        location = "Columbia College",
                        address = new AddressEntity()
                        {

                            City = "Chicago",
                            Country = "US",
                            State = "IL",
                            StreetNumber = 600,
                            StreetName = "S. Michigan Ave",
                            PostalArea = "60605"
                        },
                        meetupUrl = "",
                        name = "MonkeySpace 2013",
                        tagLine = ".net. Everywhere.",
                        twitterHashTag = "#monkeySpace",
                        twitterName = "@monkeySpaceConf"
                    };

                    foreach (var day in root.days)
                    {
                        foreach (var session in day.sessions)
                        {
                            var sessionEntity = new SessionEntity()
                            {
                                _id = Guid.NewGuid(),
                                description = session.@abstract,
                                start = session.begins.AddHours(-4),
                                end = session.ends.AddHours(-4),
                                title = session.title,
                                room = session.location,
                                slug = session.title.ToLower().GenerateSlug(),
                                twitterHashTag = "#ms-" + session.title.ToLower().Trim().Replace(" ", "-").SafeSubstring(0, 10)
                            };
                            conference.AddSession(sessionEntity);

                            sessionEntity.speakers = new List<SpeakerEntity>();
                            foreach (var speaker in session.speakers)
                            {

                                if (!string.IsNullOrWhiteSpace(speaker.twitterHandle) && !speaker.twitterHandle.StartsWith("@"))
                                {
                                    speaker.twitterHandle = "@" + speaker.twitterHandle;
                                }

                                var speakerEntity = new SpeakerEntity()
                                {
                                    _id = Guid.NewGuid(),
                                    firstName = speaker.name.Split(' ')[0],
                                    lastName = speaker.name.Split(' ')[1],
                                    twitterName = speaker.twitterHandle,
                                    description = speaker.bio,
                                    profileImageUrl = speaker.headshotUrl,
                                    slug = speaker.name.ToLower().GenerateSlug()
                                };
                                conference.AddSpeakerToSession(sessionEntity.slug, speakerEntity);
                            }
                        }
                    }

                    conference.Save();
                }
            }




        }
    }


    public class root
    {
        public List<day> days { get; set; }
    }

    public class day
    {
        public DateTime date { get; set; }
        public List<session> sessions { get; set; }
    }
    public class session
    {
        public int id { get; set; }
        public string title { get; set; }
        public string @abstract { get; set; }
        public List<speaker> speakers { get; set; }
        public DateTime begins { get; set; }
        public DateTime ends { get; set; }
        public string location { get; set; }
    }
    public class speaker
    {
        public int id { get; set; }
        public string name { get; set; }
        public string twitterHandle { get; set; }
        public string bio { get; set; }
        public string headshotUrl { get; set; }
    }
}
