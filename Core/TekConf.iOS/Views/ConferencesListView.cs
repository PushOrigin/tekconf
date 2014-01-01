using System;
using Cirrious.MvvmCross.Touch.Views;
using MonoTouch.UIKit;
using System.Drawing;
using MonoTouch.ObjCRuntime;
using TekConf.Core.ViewModels;
using Cirrious.MvvmCross.Binding.BindingContext;
using Cirrious.MvvmCross.Binding.Touch.Views;

namespace TekConf.iOS
{
	public class ConferencesListView : MvxTableViewController
	{
		public ConferencesListView ()
		{
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			// ios7 layout
			if (RespondsToSelector(new Selector("edgesForExtendedLayout")))
				EdgesForExtendedLayout = UIRectEdge.None;

			var source = new MvxSimpleTableViewSource (TableView, typeof(ConferenceCell));
			TableView.RowHeight = 250;
			TableView.Source = source;

			var set = this.CreateBindingSet<ConferencesListView, ConferencesListViewModel>();
			set.Bind (source).To (vm => vm.Conferences);
			set.Bind (source).For (s => s.SelectionChangedCommand).To (vm => vm.ShowDetailCommand);
			set.Apply();

			TableView.ReloadData ();

			//View = new UIView(){ BackgroundColor = UIColor.White};
			//base.ViewDidLoad();
			//var label = new UILabel(new RectangleF(10, 10, 300, 40));
			//Add(label);
			//var textField = new UITextField(new RectangleF(10, 50, 300, 40));
			//Add(textField);

			//var set = this.CreateBindingSet<FirstView, Core.ViewModels.FirstViewModel>();
			//var set = this.CreateBindingSet<ConferencesListView, ConferencesListViewModel>();
			//set.Apply();

			//set.Bind(label).To(vm => vm.Hello);
			//set.Bind(textField).To(vm => vm.Hello);
			//set.Apply();
		}

	}
}

