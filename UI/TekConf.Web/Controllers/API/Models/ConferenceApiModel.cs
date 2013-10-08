namespace TekConf.Web.Controllers.API
{
    using System;
    using System.Collections.Generic;

    using TekConf.Common.Entities;

    public class ConferenceApiModel
    {
        public int ConferenceId { get; set; }
        public string Slug { get; set; }
        
        public DateTime? Start { get; set; }
        public DateTime? CallForSpeakersCloses { get; set; }
        public DateTime? CallForSpeakersOpens { get; set; }
        public DateTime? DateAdded { get; set; }
        public DateTime? DatePublished { get; set; }
        public DateTime? End { get; set; }
        public DateTime? RegistrationCloses { get; set; }
        public DateTime? RegistrationOpens { get; set; }

        public int DefaultTalkLength { get; set; }

        public double Distance { get; set; }
        public double[] Position { get; set; }

        public bool IsLive { get; set; }
        public bool? IsOnline { get; set; }
        public bool IsSaved { get; set; }

        public string Name { get; set; }
        public string TagLine { get; set; }
        public string TwitterHashTag { get; set; }
        public string TwitterName { get; set; }
        public string VimeoUrl { get; set; }
        public string YoutubeUrl { get; set; }
        public string Description { get; set; }
        public string FacebookUrl { get; set; }
        public string GithubUrl { get; set; }
        public string GooglePlusUrl { get; set; }
        public string HomepageUrl { get; set; }
        public string ImageUrl { get; set; }
        public string LanyrdUrl { get; set; }
        public string LinkedInUrl { get; set; }
        public string Location { get; set; }
        public string MeetupUrl { get; set; }

        public List<string> Rooms { get; set; }
        public List<string> SessionTypes { get; set; }
        public IEnumerable<string> Subjects { get; set; }
        public IEnumerable<string> Tags { get; set; }

        public ICollection<SessionEntity> Sessions { get; set; }
        public AddressEntity Address { get; set; }
    
    }
}