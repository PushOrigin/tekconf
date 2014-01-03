using System;
using Cirrious.MvvmCross.ViewModels;
using System.Windows.Input;
using TekConf.Core.ViewModels;
using System.Collections.Generic;

namespace TekConf.Core
{
	public class RightPanelViewModel : MvxViewModel 
	{
		public RightPanelViewModel ()
		{
		}

		public ICommand ShowConferencesListCommand
		{
			get
			{
				return new MvxCommand(() => clearStackAndShow<ConferencesListViewModel>());
			}
		}

		public ICommand ShowSettingsCommand 
		{
			get { return new MvxCommand (() => clearStackAndShow<SettingsViewModel> ()); } 
		}

		private void clearStackAndShow<TViewModel>()
			where TViewModel : MvxViewModel
		{
			var presentationBundle = new MvxBundle(new Dictionary<string, string> { { PresentationBundleFlagKeys.ClearStack, "" } });
			ShowViewModel<TViewModel>(presentationBundle: presentationBundle);
		}

	}
}

