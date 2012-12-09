using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text.RegularExpressions;
using AutoMapper;
using MongoDB.Driver.Builders;
using TekConf.RemoteData.Dtos.v1;
using TekConf.RemoteData.v1;
using TekConf.UI.Api.Services.Requests.v1;
using TekConf.UI.Api.UrlResolvers.v1;
using FluentMongo.Linq;
using ServiceStack.CacheAccess;
using ServiceStack.ServiceHost;
using TinyMessenger;

namespace TekConf.UI.Api.Services.v1
{
    public class ConferencesService : MongoServiceBase
    {
        private readonly ITinyMessengerHub _hub;
        public ICacheClient CacheClient { get; set; }

        public ConferencesService(ITinyMessengerHub hub)
        {
            _hub = hub;
        }

        public object Get(Conferences request)
        {
            //Prerun();

            if (request.showOnlyFeatured)
            {
                return GetFeaturedConferences(request);
            }
            else
            {
                return GetAllConferences(request);
            }
        }

        private void Prerun()
        {
            try
            {
                var collection = this.RemoteDatabase.GetCollection<ConferenceEntity>("conferences");
                var confs = collection.AsQueryable()
                    .ToList();

                var toDelete = confs.Where(x => x.slug.ToLower().StartsWith("temp")).ToList();

                foreach (var conf in toDelete)
                {
                    collection.Remove(Query.EQ("_id", conf._id));
                }
                
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        private object GetAllConferences(Conferences request)
        {
            string searchCacheKey = request.search ?? string.Empty;
            string sortByCacheKey = request.sortBy ?? string.Empty;
            string showPastConferencesCacheKey = request.showPastConferences.ToString() ?? string.Empty;

            var cacheKey = "GetAllConferences-" + searchCacheKey + "-" + sortByCacheKey + "-" + showPastConferencesCacheKey;
            var expireInTimespan = new TimeSpan(0, 0, 120);

            var result = base.RequestContext.ToOptimizedResultUsingCache(this.CacheClient, cacheKey, expireInTimespan, () =>
            {
                var orderByFunc = GetOrderByFunc(request.sortBy);
                var search = GetSearch(request.search);
                var showPastConferences = GetShowPastConferences(request.showPastConferences);

                var collection = this.RemoteDatabase.GetCollection<ConferenceEntity>("conferences");
                var query = collection
                  .AsQueryable();

                if (search != null)
                {
                    query = query.Where(search);
                }

                if (showPastConferences != null)
                {
                    query = query.Where(showPastConferences);
                }

                List<ConferencesDto> conferencesDtos = null;
                List<ConferenceEntity> conferences = null;
                try
                {
                    //TODO : query = query.Where(c => c.isLive);
                    if (request.sortBy == "dateAdded")
                    {
                        query = query.OrderByDescending(orderByFunc).ThenBy(c => c.start).AsQueryable();
                    }
                    else
                    {
                        query = query.OrderBy(orderByFunc).ThenBy(c => c.start).AsQueryable();
                    }

                    conferences = query
                        .Select(c => new ConferenceEntity()
                        {
                            name = c.name,
                            start = c.start,
                            end = c.end,
                            registrationCloses = c.registrationCloses,
                            registrationOpens = c.registrationOpens,
                            location = c.location,
                            address = c.address,
                            description = c.description,
                            imageUrl = c.imageUrl,
                        })
                      .ToList();
                    conferencesDtos = Mapper.Map<List<ConferencesDto>>(conferences);
                }
                catch (Exception ex)
                {
                    var e = ex.Message;
                    throw;
                }

                return conferencesDtos.ToList();
            });

            return result;
        }

        private object GetFeaturedConferences(Conferences request)
        {
            var cacheKey = "GetFeaturedConferences";
            var expireInTimespan = new TimeSpan(0, 0, 120);

            return base.RequestContext.ToOptimizedResultUsingCache(this.CacheClient, cacheKey, expireInTimespan, () =>
            {
                var collection = this.RemoteDatabase.GetCollection<ConferenceEntity>("conferences");

                List<ConferenceEntity> conferences;
                try
                {
                    conferences = collection
                        .AsQueryable()
                        .Where(c => c.start >= DateTime.Now.AddDays(-2))
                        .OrderBy(c => c.start)
                        .ToList()
                        .Where(c => !string.IsNullOrWhiteSpace(c.description))
                        .Take(4)
.                       ToList();
                }
                catch (Exception ex)
                {
                    var s = ex.Message;
                    throw;
                }


                var conferencesDtos = Mapper.Map<List<FullConferenceDto>>(conferences);

                return conferencesDtos.ToList();
            });
        }


        private Expression<Func<ConferenceEntity, bool>> GetShowPastConferences(bool? showPastConferences)
        {
            Expression<Func<ConferenceEntity, bool>> searchBy = null;

            if (showPastConferences == null || !(bool)showPastConferences)
            {
                searchBy = c => c.end > DateTime.Now.AddDays(1);
            }

            return searchBy;
        }
        private Expression<Func<ConferenceEntity, bool>> GetSearch(string search)
        {
            Expression<Func<ConferenceEntity, bool>> searchBy = null;
            
            if (!string.IsNullOrWhiteSpace(search))
            {
                var regex = new Regex(search, RegexOptions.IgnoreCase);
                
                searchBy = c => Regex.IsMatch(c.name, search, RegexOptions.IgnoreCase)
                    || Regex.IsMatch(c.description, search, RegexOptions.IgnoreCase)
                    || Regex.IsMatch(c.address.City, search, RegexOptions.IgnoreCase)
                    || Regex.IsMatch(c.address.Country, search, RegexOptions.IgnoreCase)
                    || c.sessions.Any(s => Regex.IsMatch(s.description, search, RegexOptions.IgnoreCase))
                    || c.sessions.Any(s => Regex.IsMatch(s.title, search, RegexOptions.IgnoreCase))
                    //|| c.sessions.Any(s => s.tags.Any(t => t.Contains(search)))
                    //|| c.sessions.Any(session => session.speakers.Any(s => s.firstName.Contains(search)))
                    //|| c.sessions.Any(session => session.speakers.Any(s => s.lastName.Contains(search)))
                    //|| c.sessions.Any(session => session.speakers.Any(s => s.twitterName.Contains(search)))
                    ;
            }

            return searchBy;
        }

        private Func<ConferenceEntity, object> GetOrderByFunc(string sortBy)
        {
            Func<ConferenceEntity, object> orderByFunc = null;

            if (sortBy == "startDate")
            {
                orderByFunc = c => c.start;
            }
            else if (sortBy == "name")
            {
                orderByFunc = c => c.name;
            }
            else if (sortBy == "callForSpeakersOpeningDate")
            {
                orderByFunc = c => c.callForSpeakersOpens;
            }
            else if (sortBy == "callForSpeakersClosingDate")
            {
                orderByFunc = c => c.callForSpeakersCloses;
            }
            else if (sortBy == "registrationOpens")
            {
                orderByFunc = c => c.registrationOpens;
            }
            else if (sortBy == "dateAdded")
            {
                orderByFunc = c => c.datePublished;
            }
            else
            {
                orderByFunc = c => c.end;
            }

            return orderByFunc;
        }
    }

    public class SessionResult
    {
        public DateKey DateKey { get; set; }
        public SessionEntity Session { get; set; }
    }

    public class DateKey
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public int Day { get; set; }
    }

}