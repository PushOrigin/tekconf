namespace ConferencesIO.UI.Api.Services.Requests
{
  public class SessionSpeakersRequest
  {
    public string conferenceSlug { get; set; }
    public string sessionSlug { get; set; }
    public string speakerSlug { get; set; }
  }
}