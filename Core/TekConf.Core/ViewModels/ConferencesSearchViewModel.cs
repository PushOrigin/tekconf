using System.Windows.Input;
using Cirrious.MvvmCross.ViewModels;

namespace TekConf.Core.ViewModels
{
	public class ConferencesSearchViewModel : MvxViewModel
	{
		public void Init(string fake)
		{
		}

		string searchText;
		public string SearchText {
			get {
				return searchText;
			}
			set {
				if (value != searchText)
					RaisePropertyChanged("SearchText");
				searchText = value;
			}
		}

		public ICommand SearchCommand
		{
			get
			{
				return new MvxCommand(() => 
					ShowViewModel<ConferencesListViewModel>(new { searchTerm = SearchText })
					);
			}
		}

	}
}