using System;
using Cirrious.MvvmCross.Touch.Views;
using MonoTouch.UIKit;
using System.Drawing;
using MonoTouch.ObjCRuntime;
using Cirrious.MvvmCross.Dialog.Touch;
using Cirrious.MvvmCross.Binding.BindingContext;
using TekConf.Core;
using System.Linq.Expressions;
using CrossUI.Touch.Dialog.Elements;

namespace TekConf.iOS
{
	public class RightPanelView : MvxDialogViewController 
	{
		public RightPanelView () : base(UITableViewStyle.Plain)
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			var frame = View.Frame;
			frame.Width = 250;
			View.Frame = frame;

			var binder = this.CreateBindingSet<RightPanelView, RightPanelViewModel>();
			Func<string, string, Expression<Func<RightPanelViewModel, object>>, StyledStringElement> createElement = (text, imageFileName, property) =>
			{
				var element = new StyledStringElement(text) 
				{ 
					Accessory = UITableViewCellAccessory.DisclosureIndicator,
					ShouldDeselectAfterTouch = true,
					Image = UIImage.FromFile(imageFileName),
					BackgroundColor = UIColor.White//AppStyles.MenuCellBackgroundColor
				};

				binder.Bind(element)
					.For(el => el.SelectedCommand)
					.To(property);

				return element;
			};
		
			Root = new CrossUI.Touch.Dialog.Elements.RootElement("")
			{
				new CrossUI.Touch.Dialog.Elements.Section
				{
					createElement("Conferences", "Images/Tabs/Boss.png", vm => vm.ShowConferencesListCommand),
					createElement("My Conferences", "Images/Tabs/Boss.png", vm => vm.ShowSettingsCommand),
					createElement("Settings", "Images/Tabs/Boss.png", vm => vm.ShowSettingsCommand)
				}
			};
			binder.Apply ();

			var headerView = new UIView(new RectangleF(0, 0, View.Frame.Width, 60));
			headerView.Add(new UILabel(new RectangleF(15, 0, headerView.Frame.Width - 15, headerView.Frame.Height))
				{
					Text = "TekConf",
					Font = UIFont.FromName("HelveticaNeue-Bold", 18),
					BackgroundColor = UIColor.Clear,
					TextColor = UIColor.Black
				});
			TableView.TableHeaderView = headerView;

		}

		private CrossUI.Touch.Dialog.Elements.StyledStringElement CreateItem(string text, string imageFileName)
		{
			return new CrossUI.Touch.Dialog.Elements.StyledStringElement(text) 
			{ 
				Accessory = UITableViewCellAccessory.DisclosureIndicator,
				ShouldDeselectAfterTouch = true,
				Image = UIImage.FromFile(imageFileName),
				BackgroundColor = UIColor.White
			};
		}

	}
}

