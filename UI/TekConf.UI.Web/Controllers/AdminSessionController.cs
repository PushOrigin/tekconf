using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using TekConf.RemoteData.Dtos.v1;
using TekConf.RemoteData.v1;
using TekConf.UI.Api.Services.Requests.v1;

namespace TekConf.UI.Web.Controllers
{
    public class AdminSessionController : AsyncController
    {
        private RemoteDataRepositoryAsync _repository;
        public AdminSessionController()
        {
            _repository = new RemoteDataRepositoryAsync();
        }

        #region Add Session

        public void AddSessionAsync(string conferenceSlug)
        {
            var repository = new RemoteDataRepository();

            AsyncManager.OutstandingOperations.Increment();
            repository.GetFullConference(conferenceSlug, conference =>
                                                             {
                                                                 AsyncManager.Parameters["conference"] = conference;
                                                                 AsyncManager.OutstandingOperations.Decrement();
                                                             });
        }

        public ActionResult AddSessionCompleted(FullConferenceDto conference)
        {
            var session = new AddSession() { conferenceSlug = conference.slug, start = conference.start, end = conference.end };
            session.start = conference.start;
            session.end = conference.end;

            return View(session);
        }

        [HttpPost]
        public void AddSessionToConferenceAsync(AddSession session)
        {
            var repository = new RemoteDataRepository();

            AsyncManager.OutstandingOperations.Increment();

            repository.AddSessionToConference(session, c =>
                                                           {
                                                               AsyncManager.Parameters["session"] = c;
                                                               AsyncManager.OutstandingOperations.Decrement();
                                                           });
        }

        public ActionResult AddSessionToConferenceCompleted(SessionDto session)
        {
            return RedirectToRoute("AdminAddSpeaker", new { conferenceSlug = session.conferenceSlug, sessionSlug = session.slug });
        }

        #endregion

        #region Edit Session

        public void EditSessionAsync(string conferenceSlug, string sessionSlug)
        {
            var repository = new RemoteDataRepository();

            AsyncManager.OutstandingOperations.Increment();
            repository.GetFullConference(conferenceSlug, conference =>
                                                             {
                                                                 var session = conference.sessions.FirstOrDefault(s => s.slug == sessionSlug);
                                                                 AsyncManager.Parameters["session"] = session;
                                                                 AsyncManager.OutstandingOperations.Decrement();
                                                             });
        }

        public ActionResult EditSessionCompleted(FullSessionDto session)
        {
            var addSession = Mapper.Map<AddSession>(session);

            return View(addSession);
        }

        [HttpPost]
        public void EditSessionInConferenceAsync(AddSession session)
        {
            var repository = new RemoteDataRepository();

            AsyncManager.OutstandingOperations.Increment();

            repository.EditSessionInConference(session, c =>
                                                            {
                                                                AsyncManager.Parameters["session"] = c;
                                                                AsyncManager.OutstandingOperations.Decrement();
                                                            });
        }

        public ActionResult EditSessionInConferenceCompleted(SessionDto session)
        {
            return RedirectToRoute("SessionDetail", new { conferenceSlug = session.conferenceSlug, sessionSlug = session.slug });
        }

        #endregion
    }
}