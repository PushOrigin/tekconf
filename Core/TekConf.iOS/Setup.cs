using MonoTouch.UIKit;
using Cirrious.CrossCore.Platform;
using Cirrious.MvvmCross.ViewModels;
using Cirrious.MvvmCross.Touch.Platform;
using TekConf.Core.Services;
using Cirrious.CrossCore;
using Microsoft.WindowsAzure.MobileServices;
using TekConf.Core.Interfaces;
using Cirrious.MvvmCross.Plugins.File;
using TekConf.Core.Repositories;
using Cirrious.MvvmCross.Binding;
using Cirrious.MvvmCross.Plugins.Sqlite;
using TekConf.Core.Models;
using TekConf.Core.ViewModels;
using TekConf.Core;

namespace TekConf.iOS
{
	public class Setup : MvxTouchSetup
	{
		public static MobileServiceClient MobileService;
		// = new MobileServiceClient(
		//	"https://tekconfauth.azure-mobile.net/",
		//	"NeMPYjchPdsFKlUqDdyAJYZtdrOPiJ11"
		//);

		public Setup(MvxApplicationDelegate applicationDelegate, UIWindow window)
            : base(applicationDelegate, window)
		{

		}

		protected override IMvxApplication CreateApp ()
		{
			MvxBindingTrace.TraceBindingLevel = MvxTraceLevel.Diagnostic;
			var factory = Mvx.Resolve<ISQLiteConnectionFactory>();
			var connection = factory.Create("tekconf.db");

			Mvx.RegisterSingleton(typeof(IAuthentication), new IPhoneAuthentication(connection));
			Mvx.RegisterSingleton<ISQLiteConnection>(connection);

			Mvx.RegisterType<IMessageBox, IPhoneMessageBox>();
			Mvx.RegisterType<INetworkConnection, IPhoneNetworkConnection> ();
			Mvx.RegisterType<IAuthentication, IPhoneAuthentication> ();
			Mvx.RegisterType<IAnalytics, IPhoneAnalytics> ();
			Mvx.RegisterType<IPushSharpClient, IPhonePushSharpClient> ();
			Mvx.RegisterType<ILocalConferencesRepository, LocalConferencesRepository> ();
			Mvx.RegisterType<IRemoteDataService, RemoteDataService> ();
			Mvx.RegisterType<ICacheService, CacheService> ();
			Mvx.RegisterType<IRestService, RestService> ();

			Mvx.RegisterType<ConferenceDetailViewModel, ConferenceDetailViewModel> ();
			Mvx.RegisterType<ConferenceSessionsViewModel, ConferenceSessionsViewModel> ();
			Mvx.RegisterType<ConferenceLocationViewModel, ConferenceLocationViewModel> ();

			return new Core.App();
		}
		
        protected override IMvxTrace CreateDebugTrace()
        {
            return new DebugTrace();
        }
	}
}