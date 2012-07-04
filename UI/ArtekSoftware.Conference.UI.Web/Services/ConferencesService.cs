using System.Collections.Generic;
using System.Linq;
using System.Net;
using ArtekSoftware.Conference.RemoteData.Dtos;
using ArtekSoftware.Conference.UI.Web.Services.Requests;
using AutoMapper;
using MongoDB.Driver.Linq;
using ServiceStack.Common.Web;

namespace ArtekSoftware.Conference.UI.Web
{
  public class ConferencesService : MongoRestServiceBase<ConferencesRequest>
  {
    public override object OnGet(ConferencesRequest request)
    {
      if (request.conferenceSlug == default(string))
      {
        return GetAllConferences();
      }
      else
      {
        return GetSingleConference(request);
      }
    }

    private object GetSingleConference(ConferencesRequest request)
    {
      var conference = this.Database.GetCollection<ConferenceEntity>("conferences")
        .AsQueryable()
        .SingleOrDefault(c => c.slug == request.conferenceSlug);

      if (conference == null)
      {
        throw new HttpError(HttpStatusCode.NotFound, "Conference not found.");
      }

      var conferenceDto = Mapper.Map<ConferenceEntity, ConferenceDto>(conference);
      var conferenceUrlResolver = new ConferenceUrlResolver(conferenceDto.slug);
      var conferenceSessionsUrlResolver = new ConferenceSessionsUrlResolver(conferenceDto.slug);
      var conferenceSpeakersUrlResolver = new ConferenceSpeakersUrlResolver(conferenceDto.slug);

      conferenceDto.url = conferenceUrlResolver.ResolveUrl();
      conferenceDto.sessionsUrl = conferenceSessionsUrlResolver.ResolveUrl();
      conferenceDto.speakersUrl = conferenceSpeakersUrlResolver.ResolveUrl();
      
      return conferenceDto;
    }

    private object GetAllConferences()
    {
      List<ConferenceEntity> conferences = null;

      conferences = this.Database.GetCollection<ConferenceEntity>("conferences")
        .AsQueryable()
        .ToList();

      var conferencesDtos = Mapper.Map<List<ConferenceEntity>, List<ConferencesDto>>(conferences);
      var resolver = new ConferencesUrlResolver();
      foreach (var conferencesDto in conferencesDtos)
      {
        conferencesDto.url = resolver.ResolveUrl(conferencesDto.slug);
      }

      return conferencesDtos.ToList();
    }
  }
}