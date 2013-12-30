using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Microsoft.WindowsAzure.MobileServices;

namespace test
{
	public class Application
	{
		// This is the main entry point of the application.
		static void Main (string[] args)
		{
			var MobileService = new MobileServiceClient(
				"https://tekconfauth.azure-mobile.net/",
				"NeMPYjchPdsFKlUqDdyAJYZtdrOPiJ11"
			);
			// if you want to use a different Application Delegate class from "AppDelegate"
			// you can specify it here.
			UIApplication.Main (args, null, "AppDelegate");
		}
	}
}
