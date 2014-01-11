using System.Drawing;
using Cirrious.MvvmCross.Binding.BindingContext;
using Cirrious.MvvmCross.Touch.Views;
using MonoTouch.ObjCRuntime;
using MonoTouch.UIKit;
using MonoTouch.Foundation;
using SlidingPanels.Lib;
using SlidingPanels.Lib.PanelContainers;

namespace TekConf.iOS.Views
{
    [Register("FirstView")]
    public class FirstView : MvxViewController
    {
		private UIBarButtonItem CreateSliderButton(string imageName, PanelType panelType)
		{
			UIButton button = new UIButton(new RectangleF(0, 0, 40f, 40f));
			button.SetBackgroundImage(UIImage.FromBundle(imageName), UIControlState.Normal);
			button.TouchUpInside += delegate
			{
				SlidingPanelsNavigationViewController navController = NavigationController as SlidingPanelsNavigationViewController;
				navController.TogglePanel(panelType);
			};

			return new UIBarButtonItem(button);
		}

        public override void ViewDidLoad()
        {
            View = new UIView(){ BackgroundColor = UIColor.White};
            base.ViewDidLoad();

			// ios7 layout
            if (RespondsToSelector(new Selector("edgesForExtendedLayout")))
               EdgesForExtendedLayout = UIRectEdge.None;
			   
			NavigationItem.RightBarButtonItem = CreateSliderButton ("Images/Menu.png", PanelType.RightPanel);

			var _imageView = new UIImageView (new RectangleF (0, 0, 50, 50));
			var _image = UIImage.FromFile ("Images/Tabs/Boss.png");
			_imageView.Image = _image;
			View.AddSubview (_imageView);

            var label = new UILabel(new RectangleF(10, 10, 300, 40));
            Add(label);
            var textField = new UITextField(new RectangleF(10, 50, 300, 40));
            Add(textField);

            var set = this.CreateBindingSet<FirstView, Core.ViewModels.FirstViewModel>();
            set.Bind(label).To(vm => vm.Hello);
            set.Bind(textField).To(vm => vm.Hello);
            set.Apply();
        }
    }
}