using TekConf.Core.Services;

namespace TekConf.UI.iPhone
{
    public class iPhoneNetworkConnection : INetworkConnection
    {
        public bool IsNetworkConnected()
        {
            return true; //TODO
        }

        public string NetworkDownMessage
        {
            get
            {
                return "";
            }
        }
    }
}