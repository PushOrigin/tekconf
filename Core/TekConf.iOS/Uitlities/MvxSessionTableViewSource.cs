using System;
using Cirrious.MvvmCross.Binding.Touch.Views;
using MonoTouch.UIKit;
using System.Collections.Generic;
using TekConf.Core.Repositories;
using System.Linq;
using MonoTouch.Foundation;

namespace TekConf.iOS
{
	public class MvxSessionTableViewSource : MvxSimpleTableViewSource
	{
		public MvxSessionTableViewSource (UITableView tableView, Type cellType) 
			: base(tableView, cellType)
		{

		}

		private List<ConferenceSessionGroup> Sessions { get { return (ItemsSource as IEnumerable<ConferenceSessionGroup>).ToList(); } }

		public override string TitleForHeader(UITableView tableView, int section)
		{
			if (Sessions == null)
				return string.Empty;

			return Sessions[section].Key;
		}

		protected override object GetItemAt(NSIndexPath indexPath)
		{
			if (Sessions == null)
				return null;
			return Sessions[indexPath.Section][indexPath.Row];
		}

		public override int NumberOfSections (UITableView tableView)
		{
			if (Sessions == null)
				return 0;
			return Sessions.Count;
		}
		public override int RowsInSection (UITableView tableview, int section)
		{
			if (Sessions == null)
				return 0;
			return Sessions [section].Count;
		}
	}
}

