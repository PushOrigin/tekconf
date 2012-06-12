using System;
using System.Drawing;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.Linq;
using System.Threading;
using System.Collections.Generic;

namespace ArtekSoftware.Conference.Mobile.iOS
{
	public partial class RootViewController : UITableViewController
	{
		private RemoteData.Shared.RemoteData _client;
		private string _baseUrl = "http://conference.azurewebsites.net/api/";

		static bool UserInterfaceIdiomIsPhone {
			get { return UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Phone; }
		}

		public RootViewController ()
			: base (UserInterfaceIdiomIsPhone ? "RootViewController_iPhone" : "RootViewController_iPad", null)
		{
			if (!UserInterfaceIdiomIsPhone) {
				this.ClearsSelectionOnViewWillAppear = false;
				this.ContentSizeForViewInPopover = new SizeF (320f, 600f);
			}
			
			// Custom initialization
		}
		
		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			_client = new RemoteData.Shared.RemoteData(_baseUrl);

			var loading = new UIAlertView(" Downloading Conferences", "Please wait...", null, null, null);

			loading.Show();

			var indicator = new UIActivityIndicatorView( UIActivityIndicatorViewStyle.WhiteLarge); 
			indicator.Center = new System.Drawing.PointF( loading.Bounds.Width / 2, loading.Bounds.Size.Height - 40); 
			indicator.StartAnimating(); 
			loading.AddSubview( indicator);

			_client.GetConferences(conferences => 
			                         { 
										InvokeOnMainThread(() => 
				                   		{ 
											TableView.Source = new ConferenceTableViewSource(conferences); 
											TableView.ReloadData(); 
											loading.DismissWithClickedButtonIndex( 0, true); 
										});
									});

			// Perform any additional setup after loading the view, typically from a nib.
			
			//this.TableView.Source = new DataSource (this);
			
			if (!UserInterfaceIdiomIsPhone)
				this.TableView.SelectRow (
					NSIndexPath.FromRowSection (0, 0),
					false,
					UITableViewScrollPosition.Middle
				);
		}
		
		public override bool ShouldAutorotateToInterfaceOrientation (UIInterfaceOrientation toInterfaceOrientation)
		{
			// Return true for supported orientations
			if (UserInterfaceIdiomIsPhone) {
				return (toInterfaceOrientation != UIInterfaceOrientation.PortraitUpsideDown);
			} else {
				return true;
			}
		}
		
		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();
			
			// Release any cached data, images, etc that aren't in use.
		}
		
		public override void ViewDidUnload ()
		{
			base.ViewDidUnload ();
			
			// Clear any references to subviews of the main view in order to
			// allow the Garbage Collector to collect them sooner.
			//
			// e.g. myOutlet.Dispose (); myOutlet = null;
			
			ReleaseDesignerOutlets ();
		}

public class ConferenceTableViewSource : UITableViewSource 
		{ 
			private readonly IList<RemoteData.Shared.Conference> _conferences; 
			private const string ConferenceCell = "ConferenceCell"; 
			public ConferenceTableViewSource(IList<RemoteData.Shared.Conference> conferences) 
			{ 
				_conferences = conferences; 
			} 
			public override int RowsInSection(UITableView tableView, int section) 
			{ 
				return _conferences.Count; 
			} 
			public override float GetHeightForRow(UITableView tableView, NSIndexPath indexPath) 
			{ 
				return 60; 
			} 
			public override UITableViewCell GetCell( UITableView tableView, NSIndexPath indexPath) 
			{ 
				var cell = tableView.DequeueReusableCell(ConferenceCell) ?? new UITableViewCell(UITableViewCellStyle.Subtitle, ConferenceCell); 
				var conference = _conferences[indexPath.Row]; 
				cell.TextLabel.Text = conference.Name; 
				cell.DetailTextLabel.Text = conference.Start.ToLocalTime().ToString(); 
				return cell; 
			} 

			public override void RowSelected( UITableView tableView, NSIndexPath indexPath) 
			{ 
				var selectedConference = _conferences[ indexPath.Row]; 
				new UIAlertView("View Conference", selectedConference.Name, null, "Ok", null).Show(); 
			} 
		} 
	 






		class DataSource : UITableViewSource
		{
			RootViewController controller;

			public DataSource (RootViewController controller)
			{
				this.controller = controller;
			}
			
			// Customize the number of sections in the table view.
			public override int NumberOfSections (UITableView tableView)
			{
				return 1;
			}
			
			public override int RowsInSection (UITableView tableview, int section)
			{
				return 1;
			}
			
			// Customize the appearance of table view cells.
			public override UITableViewCell GetCell (UITableView tableView, MonoTouch.Foundation.NSIndexPath indexPath)
			{
				string cellIdentifier = "Cell";
				var cell = tableView.DequeueReusableCell (cellIdentifier);
				if (cell == null) {
					cell = new UITableViewCell (UITableViewCellStyle.Default, cellIdentifier);
					if (UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Phone) {
						cell.Accessory = UITableViewCellAccessory.DisclosureIndicator;
					}
				}

				// Configure the cell.
				//cell.TextLabel.Text = NSBundle.MainBundle.LocalizedString (
				//	"Detail",
				//	"Detail"
				//);
				return cell;
			}

			/*
			// Override to support conditional editing of the table view.
			public override bool CanEditRow (UITableView tableView, MonoTouch.Foundation.NSIndexPath indexPath)
			{
				// Return false if you do not want the specified item to be editable.
				return true;
			}
			*/
			
			/*
			// Override to support editing the table view.
			public override void CommitEditingStyle (UITableView tableView, UITableViewCellEditingStyle editingStyle, NSIndexPath indexPath)
			{
				if (editingStyle == UITableViewCellEditingStyle.Delete) {
					// Delete the row from the data source.
					controller.TableView.DeleteRows (new NSIndexPath[] { indexPath }, UITableViewRowAnimation.Fade);
				} else if (editingStyle == UITableViewCellEditingStyle.Insert) {
					// Create a new instance of the appropriate class, insert it into the array, and add a new row to the table view.
				}
			}
			*/
			
			/*
			// Override to support rearranging the table view.
			public override void MoveRow (UITableView tableView, NSIndexPath sourceIndexPath, NSIndexPath destinationIndexPath)
			{
			}
			*/
			
			/*
			// Override to support conditional rearranging of the table view.
			public override bool CanMoveRow (UITableView tableView, NSIndexPath indexPath)
			{
				// Return false if you do not want the item to be re-orderable.
				return true;
			}
			*/
			
			public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
			{
				if (UserInterfaceIdiomIsPhone) {
					var DetailViewController = new DetailViewController ();
					// Pass the selected object to the new view controller.
					controller.NavigationController.PushViewController (
						DetailViewController,
						true
					);
				} else {
					// Navigation logic may go here -- for example, create and push another view controller.
				}
			}
		}
	}
}

