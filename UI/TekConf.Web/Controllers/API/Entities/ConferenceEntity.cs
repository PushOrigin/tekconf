namespace TekConf.Web.Controllers.API
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Data.SqlTypes;
    using System.Linq;

    using MongoDB.Bson.Serialization.Attributes;
    using MongoDB.Bson.Serialization.IdGenerators;

    using TekConf.Common.Entities;

    using TinyMessenger;

    public class ConferenceEntity : IEntity
    {
        private readonly List<ConferenceCreatedMessage> _conferenceCreatedMessages;
        private readonly List<ConferenceEndDateChangedMessage> _conferenceEndDateChangedMessages;
        private readonly List<ConferenceLocationChangedMessage> _conferenceLocationChangedMessages;
        private readonly List<ConferencePublishedMessage> _conferencePublishedMessages;
        private readonly List<ConferenceStartDateChangedMessage> _conferenceStartDateChangedMessages;
        private readonly List<ConferenceUpdatedMessage> _conferenceUpdatedMessages;
        private readonly ITinyMessengerHub _hub;
        private readonly IConferenceRepository _repository;
        private readonly List<SessionAddedMessage> _sessionAddedMessages;
        private readonly List<SessionEndDateChangedMessage> _sessionEndDateChangedMessages;
        private readonly List<SessionRemovedMessage> _sessionRemovedMessages;
        private readonly List<SessionRoomChangedMessage> _sessionRoomChangedMessages;
        private readonly List<SessionStartDateChangedMessage> _sessionStartDateChangedMessages;
        private readonly List<SpeakerAddedMessage> _speakerAddedMessages;
        private readonly List<SpeakerRemovedMessage> _speakerRemovedMessages;
        private string _description;
        private DateTime? _end;
        private string _facebookUrl;
        private string _githubUrl;
        private string _googlePlusUrl;
        private string _homepageUrl;
        private string _imageUrl;
        private bool _isInitializingFromBson;
        private string _lanyrdUrl;
        private string _linkedInUrl;
        private string _location;
        private string _meetupUrl;
        private string _name;
        private IList<SessionEntity> _sessions = new List<SessionEntity>();
        private DateTime? _start;
        private IList<string> _subjects = new List<string>();
        private string _tagLine;
        private IList<string> _tags = new List<string>();
        private string _twitterHashTag;
        private string _twitterName;
        private string _vimeoUrl;
        private string _youtubeUrl;

        private DateTime? _registrationCloses;

        private DateTime? _registrationOpens;

        private DateTime? _callForSpeakersCloses;

        private DateTime? _callForSpeakersOpens;

        private DateTime? _dateAdded;

        private DateTime? _datePublished;

        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value.IsNullOrWhiteSpace() ? value : value.Trim();
            }
        }

        public DateTime? Start
        {
            get
            {
                return _start;
            }
            set
            {
                if (value == DateTime.MinValue)
                {
                    value = null;
                }

                if (!_isInitializingFromBson && IsSaved && _start != value)
                {
                    //_conferenceStartDateChangedMessages.Add(
                    //    new ConferenceStartDateChangedMessage
                    //    {
                    //        ConferenceSlug = Slug,
                    //        ConferenceName = this.Name,
                    //        OldValue = _start,
                    //        NewValue = value
                    //    });
                }

                _start = value;
            }
        }

        [Key]
        public int ConferenceId { get; set; }
        public AddressEntity Address { get; set; }

        public DateTime? CallForSpeakersCloses
        {
            get
            {
                return _callForSpeakersCloses;
            }
            set
            {
                if (value == DateTime.MinValue)
                {
                    value = null;
                }

                _callForSpeakersCloses = value;
            }
        }

        public DateTime? CallForSpeakersOpens
        {
            get
            {
                return _callForSpeakersOpens;
            }
            set
            {
                if (value == DateTime.MinValue)
                {
                    value = null;
                }

                _callForSpeakersOpens = value;
            }
        }

        public DateTime? DateAdded
        {
            get
            {
                return _dateAdded;
            }
            set
            {
                if (value == DateTime.MinValue)
                {
                    value = null;
                }

                _dateAdded = value;
            }
        }

        public DateTime? DatePublished
        {
            get
            {
                return _datePublished;
            }
            set
            {
                if (value == DateTime.MinValue)
                {
                    value = null;
                }

                _datePublished = value;
            }
        }

        public int DefaultTalkLength { get; set; }

        public string Description
        {
            get
            {
                return _description;
            }
            set
            {
                _description = value.IsNullOrWhiteSpace() ? value : value.Trim();
            }
        }

        public double Distance { get; set; }

        public DateTime? End
        {
            get
            {
                return _end;
            }
            set
            {
                if (value == DateTime.MinValue)
                {
                    value = null;
                }
                if (!_isInitializingFromBson && IsSaved && _end != value)
                {
                    //_conferenceEndDateChangedMessages.Add(
                    //    new ConferenceEndDateChangedMessage
                    //    {
                    //        ConferenceName = this.Name,
                    //        ConferenceSlug = Slug,
                    //        OldValue = _end,
                    //        NewValue = value
                    //    });
                }

                _end = value;
            }
        }

        public string FacebookUrl
        {
            get
            {
                return _facebookUrl;
            }
            set
            {
                _facebookUrl = value.IsNullOrWhiteSpace() ? value : value.Trim();
            }
        }

        public string GithubUrl
        {
            get
            {
                return _githubUrl;
            }
            set
            {
                _githubUrl = value.IsNullOrWhiteSpace() ? value : value.Trim();
            }
        }

        public string GooglePlusUrl
        {
            get
            {
                return _googlePlusUrl;
            }
            set
            {
                _googlePlusUrl = value.IsNullOrWhiteSpace() ? value : value.Trim();
            }
        }

        public string HomepageUrl
        {
            get
            {
                return _homepageUrl;
            }
            set
            {
                _homepageUrl = value.IsNullOrWhiteSpace() ? value : value.Trim();
            }
        }

        public string ImageUrl
        {
            get
            {
                return _imageUrl;
            }
            set
            {
                _imageUrl = value.IsNullOrWhiteSpace() ? value : value.Trim();
            }
        }

        public bool IsLive { get; private set; }
        public bool? IsOnline { get; set; }
        public bool IsSaved { get; private set; }

        public string LanyrdUrl
        {
            get
            {
                return _lanyrdUrl;
            }
            set
            {
                _lanyrdUrl = value.IsNullOrWhiteSpace() ? value : value.Trim();
            }
        }

        public string LinkedInUrl
        {
            get
            {
                return _linkedInUrl;
            }
            set
            {
                _linkedInUrl = value.IsNullOrWhiteSpace() ? value : value.Trim();
            }
        }

        public string Location
        {
            get
            {
                return _location;
            }
            set
            {
                if (!_isInitializingFromBson && IsSaved && _location != value)
                {
                    //_conferenceLocationChangedMessages.Add(
                    //    new ConferenceLocationChangedMessage
                    //    {
                    //        ConferenceSlug = Slug,
                    //        ConferenceName = this.Name,
                    //        OldValue = _location,
                    //        NewValue = value
                    //    });
                }

                _location = value.IsNullOrWhiteSpace() ? value : value.Trim();
            }
        }

        public string MeetupUrl
        {
            get
            {
                return _meetupUrl;
            }
            set
            {
                _meetupUrl = value.IsNullOrWhiteSpace() ? value : value.Trim();
            }
        }

        public double[] Position { get; set; }

        public DateTime? RegistrationCloses
        {
            get
            {
                return _registrationCloses;
            }
            set
            {
                if (value == DateTime.MinValue)
                {
                    value = null;
                }

                _registrationCloses = value;
            }
        }

        public DateTime? RegistrationOpens
        {
            get
            {
                return _registrationOpens;
            }
            set
            {
                if (value == DateTime.MinValue)
                {
                    value = null;
                }

                _registrationOpens = value;
            }
        }

        public List<string> Rooms { get; set; }
        public List<string> SessionTypes { get; set; }

        public virtual ICollection<SessionEntity> Sessions { get; set; } 
        //public IEnumerable<SessionEntity> Sessions
        //{
        //    get
        //    {
        //        if (_sessions == null)
        //        {
        //            _sessions = new List<SessionEntity>();
        //        }
        //        return _sessions.AsEnumerable();
        //    }
        //    set
        //    {
        //        if (value == null)
        //        {
        //            value = new List<SessionEntity>();
        //        }

        //        _sessions = value.ToList();
        //        //foreach (SessionEntity session in _sessions)
        //        //{
        //        //    session.RoomChanged += SessionRoomChangedHandler;
        //        //    session.StartDateChanged += SessionStartDateChangedHandler;
        //        //    session.EndDateChanged += SessionEndDateChangedHandler;
        //        //}
        //    }
        //}

        public string Slug { get; set; }

        public IEnumerable<string> Subjects
        {
            get
            {
                return _subjects.AsEnumerable();
            }
            set
            {
                if (value == null)
                {
                    value = new List<string>();
                }

                _subjects = value.ToList();
            }
        }

        public string TagLine
        {
            get
            {
                return _tagLine;
            }
            set
            {
                _tagLine = value.IsNullOrWhiteSpace() ? value : value.Trim();
            }
        }

        public IEnumerable<string> Tags
        {
            get
            {
                return _tags.AsEnumerable();
            }
            set
            {
                if (value == null)
                {
                    value = new List<string>();
                }

                _tags = value.ToList();
            }
        }

        public string TwitterHashTag
        {
            get
            {
                return _twitterHashTag;
            }
            set
            {
                _twitterHashTag = value.IsNullOrWhiteSpace() ? value : value.Trim();
            }
        }

        public string TwitterName
        {
            get
            {
                return _twitterName;
            }
            set
            {
                if (!string.IsNullOrWhiteSpace(value) && !value.StartsWith("@"))
                {
                    value = "@" + value;
                }

                _twitterName = value.IsNullOrWhiteSpace() ? value : value.Trim();
            }
        }

        public string VimeoUrl
        {
            get
            {
                return _vimeoUrl;
            }
            set
            {
                _vimeoUrl = value.IsNullOrWhiteSpace() ? value : value.Trim();
            }
        }

        public string YoutubeUrl
        {
            get
            {
                return _youtubeUrl;
            }
            set
            {
                _youtubeUrl = value.IsNullOrWhiteSpace() ? value : value.Trim();
            }
        }

        //public void AddSession(SessionEntity session)
        //{
        //    if (_sessions == null)
        //    {
        //        _sessions = new List<SessionEntity>();
        //    }

        //    session.RoomChanged += SessionRoomChangedHandler;
        //    session.StartDateChanged += SessionStartDateChangedHandler;
        //    session.EndDateChanged += SessionEndDateChangedHandler;

        //    _sessions.Add(session);

        //    if (!_isInitializingFromBson && IsSaved)
        //    {
        //        _sessionAddedMessages.Add(
        //            new SessionAddedMessage { SessionSlug = session.slug, SessionTitle = session.title });
        //    }
        //}

        //public void AddSpeakerToSession(string sessionSlug, SpeakerEntity speaker)
        //{
        //    if (Sessions == null)
        //    {
        //        throw new ArgumentException("Cannot add speaker to session. Conference " + Slug + " has no sessions.");
        //    }

        //    SessionEntity session = Sessions.SingleOrDefault(s => s.slug == sessionSlug);

        //    if (session == null)
        //    {
        //        throw new ArgumentException(
        //            "Cannot add speaker to session. Conference : " + Slug + " Session:" + sessionSlug);
        //    }

        //    session.AddSpeaker(speaker);

        //    if (!_isInitializingFromBson && IsSaved)
        //    {
        //        _speakerAddedMessages.Add(
        //            new SpeakerAddedMessage
        //            {
        //                SessionSlug = sessionSlug,
        //                SessionTitle = session.title,
        //                SpeakerSlug = speaker.slug,
        //                SpeakerName = speaker.fullName
        //            });
        //    }
        //}

        //public void BeginInit()
        //{
        //    _isInitializingFromBson = true;
        //}

        //public void EndInit()
        //{
        //    _isInitializingFromBson = false;
        //}

        //public void Publish()
        //{
        //    DatePublished = DateTime.Now;
        //    IsLive = true;

        //    _conferencePublishedMessages.Add(
        //        new ConferencePublishedMessage { ConferenceName = this.Name, ConferenceSlug = Slug });
        //}

        //public void RemoveSession(SessionEntity session)
        //{
        //    if (_sessions == null)
        //    {
        //        _sessions = new List<SessionEntity>();
        //    }
        //    _sessions.Remove(session);
        //    if (!_isInitializingFromBson && IsSaved)
        //    {
        //        _sessionRemovedMessages.Add(
        //            new SessionRemovedMessage
        //            {
        //                ConferenceName = this.Name,
        //                SessionSlug = session.slug,
        //                SessionTitle = session.title
        //            });
        //    }
        //}

        //public void RemoveSpeakerFromSession(string sessionSlug, SpeakerEntity speaker)
        //{
        //    if (Sessions == null)
        //    {
        //        return;
        //    }

        //    SessionEntity session = Sessions.SingleOrDefault(s => s.slug == sessionSlug);

        //    if (session == null)
        //    {
        //        return;
        //    }

        //    session.RemoveSpeaker(speaker);

        //    if (!_isInitializingFromBson && IsSaved)
        //    {
        //        _speakerRemovedMessages.Add(
        //            new SpeakerRemovedMessage
        //            {
        //                SessionSlug = sessionSlug,
        //                SessionTitle = session.title,
        //                SpeakerSlug = speaker.slug,
        //                SpeakerName = speaker.fullName
        //            });
        //    }
        //}

        //public void Save()
        //{
        //    bool isNew = false;
        //    if (!IsSaved)
        //    {
        //        if (Id == default(Guid))
        //        {
        //            Id = Guid.NewGuid();
        //            isNew = true;
        //        }

        //        DateAdded = DateTime.Now;
        //        IsSaved = true;
        //    }
        //    Slug = Name.GenerateSlug();
        //    _repository.Save(this);

        //    if (isNew)
        //    {
        //        _conferenceCreatedMessages.Add(
        //            new ConferenceCreatedMessage { ConferenceSlug = Slug, ConferenceName = this.Name });
        //    }
        //    else
        //    {
        //        _conferenceUpdatedMessages.Add(
        //            new ConferenceUpdatedMessage { ConferenceSlug = Slug, ConferenceName = this.Name });
        //    }

        //    PublishEvents();
        //}

        //private void PublishEvents()
        //{
        //    foreach (ConferenceStartDateChangedMessage message in _conferenceStartDateChangedMessages)
        //    {
        //        _hub.Publish(message);
        //    }
        //    _conferenceStartDateChangedMessages.Clear();

        //    foreach (ConferenceCreatedMessage message in _conferenceCreatedMessages)
        //    {
        //        _hub.Publish(message);
        //    }
        //    _conferenceCreatedMessages.Clear();

        //    foreach (ConferenceEndDateChangedMessage message in _conferenceEndDateChangedMessages)
        //    {
        //        _hub.Publish(message);
        //    }
        //    _conferenceEndDateChangedMessages.Clear();

        //    foreach (ConferenceLocationChangedMessage message in _conferenceLocationChangedMessages)
        //    {
        //        _hub.Publish(message);
        //    }
        //    _conferenceLocationChangedMessages.Clear();

        //    foreach (ConferencePublishedMessage message in _conferencePublishedMessages)
        //    {
        //        _hub.Publish(message);
        //    }
        //    _conferencePublishedMessages.Clear();

        //    foreach (ConferenceUpdatedMessage message in _conferenceUpdatedMessages)
        //    {
        //        _hub.Publish(message);
        //    }
        //    _conferenceUpdatedMessages.Clear();

        //    foreach (SessionAddedMessage message in _sessionAddedMessages)
        //    {
        //        _hub.Publish(message);
        //    }
        //    _sessionAddedMessages.Clear();

        //    foreach (SessionRemovedMessage message in _sessionRemovedMessages)
        //    {
        //        _hub.Publish(message);
        //    }
        //    _sessionRemovedMessages.Clear();

        //    foreach (SessionRoomChangedMessage message in _sessionRoomChangedMessages)
        //    {
        //        _hub.Publish(message);
        //    }
        //    _sessionRoomChangedMessages.Clear();

        //    foreach (SessionStartDateChangedMessage message in _sessionStartDateChangedMessages)
        //    {
        //        _hub.Publish(message);
        //    }
        //    _sessionStartDateChangedMessages.Clear();

        //    foreach (SessionEndDateChangedMessage message in _sessionEndDateChangedMessages)
        //    {
        //        _hub.Publish(message);
        //    }
        //    _sessionEndDateChangedMessages.Clear();

        //    foreach (SpeakerAddedMessage message in _speakerAddedMessages)
        //    {
        //        _hub.Publish(message);
        //    }
        //    _speakerAddedMessages.Clear();

        //    foreach (SpeakerRemovedMessage message in _speakerRemovedMessages)
        //    {
        //        _hub.Publish(message);
        //    }
        //    _speakerRemovedMessages.Clear();
        //}

        //private void SessionEndDateChangedHandler(SessionEntity sessionEntity, EndDateChangedArgs e)
        //{
        //    if (!_isInitializingFromBson && IsSaved)
        //    {
        //        _sessionEndDateChangedMessages.Add(
        //            new SessionEndDateChangedMessage
        //            {
        //                ConferenceSlug = Slug,
        //                SessionSlug = e.SessionSlug,
        //                SessionTitle = sessionEntity.title,
        //                OldValue = e.OldValue,
        //                NewValue = e.NewValue
        //            });
        //    }
        //}

        //private void SessionRoomChangedHandler(SessionEntity sessionEntity, RoomChangedArgs e)
        //{
        //    if (!_isInitializingFromBson && IsSaved)
        //    {
        //        _sessionRoomChangedMessages.Add(
        //            new SessionRoomChangedMessage
        //            {
        //                ConferenceSlug = Slug,
        //                SessionSlug = e.SessionSlug,
        //                SessionTitle = sessionEntity.title,
        //                OldValue = e.OldValue,
        //                NewValue = e.NewValue
        //            });
        //    }
        //}

        //private void SessionStartDateChangedHandler(SessionEntity sessionEntity, StartDateChangedArgs e)
        //{
        //    if (!_isInitializingFromBson && IsSaved)
        //    {
        //        _sessionStartDateChangedMessages.Add(
        //            new SessionStartDateChangedMessage
        //            {
        //                ConferenceSlug = Slug,
        //                SessionSlug = e.SessionSlug,
        //                SessionTitle = sessionEntity.title,
        //                OldValue = e.OldValue,
        //                NewValue = e.NewValue
        //            });
        //    }
        //}

    }
}