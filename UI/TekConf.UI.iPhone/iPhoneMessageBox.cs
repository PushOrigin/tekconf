using MonoTouch.UIKit;
using TekConf.Core.Services;

namespace TekConf.UI.iPhone
{
    public class iPhoneMessageBox : IMessageBox
    {
        public void Show(string message)
        {
            var uiAlertView = new UIAlertView();
            uiAlertView.Message = message;
            uiAlertView.Show();
        }
    }
}