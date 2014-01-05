using System;
using Cirrious.MvvmCross.Touch.Views;
using MonoTouch.UIKit;
using Cirrious.MvvmCross.Binding.Touch.Views;
using MonoTouch.ObjCRuntime;
using Cirrious.MvvmCross.Binding.BindingContext;
using TekConf.Core.ViewModels;
using Cirrious.MvvmCross.Dialog.Touch;

namespace TekConf.iOS
{
	public class ConferenceSessionsView : MvxTableViewController 
	{
		public ConferenceSessionsView () //: base(UITableViewStyle.Plain)
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad();

			var source = new MvxSessionTableViewSource (TableView, typeof(SessionCell));
			TableView.RowHeight = 200;
			TableView.Source = source;

			var set = this.CreateBindingSet<ConferenceSessionsView, ConferenceSessionsViewModel>();
			//set.Bind (source).To (vm => vm.Conference.SessionsList);
			set.Bind (source).To (vm => vm.Conference.Sessions);
			set.Bind (source).For (s => s.SelectionChangedCommand).To (vm => vm.ShowSessionDetailCommand);
			set.Apply();

			TableView.ReloadData ();
		}

	}
}

