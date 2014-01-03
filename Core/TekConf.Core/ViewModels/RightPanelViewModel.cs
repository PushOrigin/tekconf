using System;
using Cirrious.MvvmCross.ViewModels;
using System.Windows.Input;
using TekConf.Core.ViewModels;

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
				return new MvxCommand(() => ShowViewModel<ConferencesListViewModel>());
			}
		}
	}
}

