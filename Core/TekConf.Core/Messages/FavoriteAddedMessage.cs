using Cirrious.MvvmCross.Plugins.Messenger;
using TekConf.RemoteData.Dtos.v1;

namespace TekConf.Core.Messages
{
	public class FavoriteAddedMessage : MvxMessage
	{
		public FavoriteAddedMessage(object sender, ScheduleDto schedule) : base(sender)
		{
		}
	}
}