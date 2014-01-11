using System;
using Cirrious.MvvmCross.Dialog.Touch;
using MonoTouch.UIKit;
using TekConf.Core;
using Cirrious.MvvmCross.Binding.Touch.Views;
using Cirrious.MvvmCross.Binding.BindingContext;

namespace TekConf.iOS
{
	public class ConferenceFavoriteListView : MvxDialogViewController
	{
		public ConferenceFavoriteListView () : base(UITableViewStyle.Plain)
		{
		}

		private ConferenceFavoriteListViewModel VM { get { return this.ViewModel as ConferenceFavoriteListViewModel; } } 

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			var source = new MvxSimpleTableViewSource (TableView, typeof(ConferenceCell));
			TableView.RowHeight = 250;
			TableView.Source = source;

			var set = this.CreateBindingSet<ConferenceFavoriteListView, ConferenceFavoriteListViewModel>();
			set.Bind (source).To (vm => vm.Favorites);
			set.Bind (source).For (s => s.SelectionChangedCommand).To (vm => vm.ConferencesListVm.ShowDetailTabCommand);
			set.Apply();

			//this.NavigationItem.SetLeftBarButtonItem(new UIBarButtonItem(UIBarButtonSystemItem.Refresh, OnRefresh), true);
			//NavigationItem.RightBarButtonItem = ViewExtensions.CreateSliderButton ("Images/menu.png", PanelType.RightPanel, NavigationController as SlidingPanelsNavigationViewController );

			TableView.ReloadData ();
		}
	}
}

