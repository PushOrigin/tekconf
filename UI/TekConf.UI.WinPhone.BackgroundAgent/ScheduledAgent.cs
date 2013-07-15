﻿using System;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using Cirrious.CrossCore;
using Cirrious.MvvmCross.Plugins.File;
using Cirrious.MvvmCross.Plugins.File.WindowsPhone;
using Microsoft.Phone.Scheduler;
using Microsoft.Phone.Shell;
using TekConf.Core.Entities;
using TekConf.Core.Repositories;
using TekConf.RemoteData.Dtos.v1;

namespace TekConf.UI.WinPhone.BackgroundAgent
{
	using Cirrious.MvvmCross.Plugins.Sqlite;

	public class ScheduledAgent : ScheduledTaskAgent
	{
		/// <remarks>
		/// ScheduledAgent constructor, initializes the UnhandledException handler
		/// </remarks>
		static ScheduledAgent()
		{
			// Subscribe to the managed exception handler
			Deployment.Current.Dispatcher.BeginInvoke(delegate
			{
				Application.Current.UnhandledException += UnhandledException;
			});
		}

		/// Code to execute on Unhandled Exceptions
		private static void UnhandledException(object sender, ApplicationUnhandledExceptionEventArgs e)
		{
			if (Debugger.IsAttached)
			{
				// An unhandled exception has occurred; break into the debugger
				Debugger.Break();
			}
		}

		/// <summary>
		/// Agent that runs a scheduled task
		/// </summary>
		/// <param name="task">
		/// The invoked task
		/// </param>
		/// <remarks>
		/// This method is called when a periodic or resource intensive task is invoked
		/// </remarks>
		protected override void OnInvoke(ScheduledTask task)
		{
			try
			{
				var fileStore = Mvx.Resolve<IMvxFileStore>();
				var connection = Mvx.Resolve<ISQLiteConnection>();
				ILocalConferencesRepository localConferencesRepository = new LocalConferencesRepository(connection);
				//var scheduleRepository = new LocalScheduleRepository(fileStore, localConferencesRepository);
				//var conference = scheduleRepository.NextScheduledConference;
				//var nextSession = scheduleRepository.NextScheduledSession;

				if (ShellTile.ActiveTiles != null)
				{
					var appTile = ShellTile.ActiveTiles.First();

					//if (conference == null)
					//	conference = new ConferencesListViewDto((ConferenceEntity)null, fileStore);

					//if (nextSession != null)
					//{
					//	var tileData = new FlipTileData()
					//	{
					//		BackContent = nextSession.title,
					//		BackTitle =
					//			nextSession.startDescription + (string.IsNullOrWhiteSpace(nextSession.room) ? "" : Environment.NewLine) +
					//			nextSession.room,
					//		Title = "",
					//		WideBackContent = nextSession.title + (string.IsNullOrWhiteSpace(conference.name) ? "" : " - " + conference.name)
					//	};

					//	appTile.Update(tileData);
					//}
				}
				NotifyComplete();
			}
			catch (Exception)
			{
			}

		}
	}
}