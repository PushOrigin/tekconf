using System;
using Cirrious.MvvmCross.Touch.Views;
using TekConf.Core;
using Cirrious.MvvmCross.Binding.BindingContext;
using Cirrious.MvvmCross.Binding.Touch.Views;

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

			var source = new MvxSimpleTableViewSource (TableView, typeof(FullSessionDtoCell));
			TableView.RowHeight = 200;
			TableView.Source = source;

			var set = this.CreateBindingSet<ConferenceFavoriteSessionsView, ConferenceFavoriteSessionsViewModel> ();
			set.Bind (source).To (vm => vm.FavoriteSessions);
			set.Apply();

			TableView.ReloadData ();
		}
	}
}

