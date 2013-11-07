using Cirrious.MvvmCross.Plugins.Sqlite;
using TekConf.Core.Services;

namespace TekConf.UI.iPhone
{
    public class Authentication : IAuthentication
    {
        public Authentication(ISQLiteConnection connection)
        {
			
        }
        public bool IsAuthenticated { get; private set; }
        public string OAuthProvider { get; private set; }
        public string UserName { get; set; }
    }
}