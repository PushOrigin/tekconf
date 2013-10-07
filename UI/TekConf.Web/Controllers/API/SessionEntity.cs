namespace TekConf.Web.Controllers.API
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using TekConf.Common.Entities;

    public class SessionEntity : IEntity
    {
        public event Common.Entities.SessionEntity.RoomChangedHandler RoomChanged;
        public event Common.Entities.SessionEntity.StartDateChangedHandler StartDateChanged;
        public event Common.Entities.SessionEntity.EndDateChangedHandler EndDateChanged;

        public delegate void RoomChangedHandler(Common.Entities.SessionEntity m, RoomChangedArgs e);
        public delegate void StartDateChangedHandler(Common.Entities.SessionEntity m, StartDateChangedArgs e);
        public delegate void EndDateChangedHandler(Common.Entities.SessionEntity m, EndDateChangedArgs e);

        private IList<SpeakerEntity> _speakers = new List<SpeakerEntity>();
        private string _title;
        private string _difficulty;
        private string _description;
        private string _twitterHashTag;
        private string _sessionType;

        [Key]
        public int SessionId { get; set; }
        public string Slug { get; set; }
        public string Title
        {
            get { return _title; }
            set { _title = value.IsNullOrWhiteSpace() ? value : value.Trim(); }
        }

        private DateTime _startDate;
        public DateTime Start
        {
            get { return _startDate; }
            set
            {
                if (_startDate != value)
                {
                    if (StartDateChanged != null)
                    {
                        var args = new StartDateChangedArgs(this.Slug, _startDate, value);

                        //StartDateChanged(this, args);
                    }
                }
                _startDate = value;
            }
        }

        private DateTime _endDate;
        public DateTime End
        {
            get { return _endDate; }
            set
            {
                if (_endDate != value)
                {
                    if (EndDateChanged != null)
                    {
                        var args = new EndDateChangedArgs(this.Slug, _startDate, value);

                        //EndDateChanged(this, args);
                    }
                }
                _endDate = value;
            }
        }

        private string _room;
        public string Room
        {
            get { return _room; }
            set
            {
                if (_room != value)
                {
                    if (RoomChanged != null)
                    {
                        var roomChanged = new RoomChangedArgs(this.Slug, _room, value);

                        //RoomChanged(this, roomChanged);
                    }
                }
                _room = value.IsNullOrWhiteSpace() ? value : value.Trim();
            }
        }

        public string Difficulty
        {
            get { return _difficulty; }
            set { _difficulty = value.IsNullOrWhiteSpace() ? value : value.Trim(); }
        }

        public string Description
        {
            get { return _description; }
            set { _description = value.IsNullOrWhiteSpace() ? value : value.Trim(); }
        }

        public string TwitterHashTag
        {
            get { return _twitterHashTag; }
            set { _twitterHashTag = value.IsNullOrWhiteSpace() ? value : value.Trim(); }
        }

        public string SessionType
        {
            get { return _sessionType; }
            set { _sessionType = value.IsNullOrWhiteSpace() ? value : value.Trim(); }
        }

        public List<string> Links { get; set; }
        public List<string> Tags { get; set; }
        public List<string> Subjects { get; set; }
        public List<string> Resources { get; set; }
        public List<string> Prerequisites { get; set; }
        public int ConferenceId { get; set; }
        public virtual ConferenceEntity Conference { get; set; }

        internal void AddSpeaker(SpeakerEntity speaker)
        {
            if (_speakers == null)
                _speakers = new List<SpeakerEntity>();

            _speakers.Add(speaker);
        }

        internal void RemoveSpeaker(SpeakerEntity speaker)
        {
            if (_speakers == null)
                return;

            _speakers.Remove(speaker);
        }



        //public IEnumerable<SpeakerEntity> Speakers
        //{
        //    get
        //    {
        //        if (_speakers == null) _speakers = new List<SpeakerEntity>();
        //        return _speakers.AsEnumerable();
        //    }
        //    set
        //    {
        //        if (value == null)
        //            value = new List<SpeakerEntity>();

        //        _speakers = value.ToList();
        //    }
        //}
    }
}