using System;
using MonoTouch.UIKit;
using SlidingPanels.Lib;
using System.Drawing;
using SlidingPanels.Lib.PanelContainers;

namespace TekConf.iOS
{
	public static class ViewExtensions
	{
		public static UIBarButtonItem CreateSliderButton(string imageName, PanelType panelType, SlidingPanelsNavigationViewController navController)
		{
			UIButton button = new UIButton(new RectangleF(0, 0, 40f, 40f));
			button.SetBackgroundImage(UIImage.FromBundle(imageName), UIControlState.Normal);
			button.TouchUpInside += delegate
			{
				navController.TogglePanel(panelType);
			};

			return new UIBarButtonItem(button);
		}
	}
}

