using System;
using Cirrious.MvvmCross.Touch.Views;
using TekConf.Core;
using Cirrious.MvvmCross.Binding.BindingContext;

namespace TekConf.iOS
{
	public class ConferenceFavoriteSessionsView : MvxTableViewController
	{
		public ConferenceFavoriteSessionsView ()
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad();

			var source = new MvxSessionTableViewSource (TableView, typeof(FullSessionDtoCell));
			TableView.RowHeight = 200;
			TableView.Source = source;

			var set = this.CreateBindingSet<ConferenceFavoriteSessionsView, ConferenceFavoriteSessionsViewModel> ();
			set.Bind (source).To (vm => vm.FavoriteSessions);
			set.Apply();

			TableView.ReloadData ();
		}
	}
}

