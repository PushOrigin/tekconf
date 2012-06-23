using System;
using System.Collections.Generic;

namespace ArtekSoftware.Conference.UI.Web
{
  public class SessionDto
  {
    public string slug { get; set; }
    public string conferenceSlug { get; set; }
    public string title { get; set; }
    public DateTime start { get; set; }
    public DateTime end { get; set; }
    public string room { get; set; }
    public string difficulty { get; set; }
    public string description { get; set; }
    public string twitterHashTag { get; set; }
    public string sessionType { get; set; }
    public string url { get; set; }
    public string linksUrl { get; set; }
    public string tagsUrl { get; set; }
    public string subjectsUrl { get; set; }
    public string speakersUrl { get; set; }

    public List<string> links { get; set; }
    public List<string> tags { get; set; }
    public List<string> subjects { get; set; }
    public List<SpeakersDto> speakers { get; set; }
  }
}