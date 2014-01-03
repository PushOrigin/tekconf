using System;
using Cirrious.MvvmCross.ViewModels;
using System.Windows.Input;
using TekConf.Core.ViewModels;

namespace TekConf.Core
{
	public class MainWindowViewModel : MvxViewModel
	{
		public MainWindowViewModel ()
		{
		}

		public ICommand ShowConferences
		{
			get
			{
				return new MvxCommand(() => ShowViewModel<ConferencesListViewModel>());
			}
		}
	}
}

