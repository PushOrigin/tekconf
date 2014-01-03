using System;
using Cirrious.MvvmCross.Touch.Views.Presenters;
using MonoTouch.UIKit;
using SlidingPanels.Lib;
using SlidingPanels.Lib.PanelContainers;
using Cirrious.MvvmCross.Touch.Views;
using Cirrious.MvvmCross.ViewModels;
using TekConf.Core;
using Cirrious.CrossCore;

namespace TekConf.iOS
{
	/// <summary>
	/// View Presenter for this application
	/// Manages automatically creating the Sliding panel view controller and its 
	/// requisite panel views as well as showing normal views
	/// </summary>
	public class MvxSlidingPanelsTouchViewPresenter : MvxTouchViewPresenter
	{
		#region Data Members

		/// <summary>
		/// the application's window.  We hang onto this so we can use it later
		/// </summary>
		private UIWindow _window;
		private IMvxTouchViewCreator _viewCreator;
		private RightPanelContainer _rightPanelContainer;
		private LeftPanelContainer _leftPanelContainer;
		private BottomPanelContainer _bottomPanelContainer;

		#endregion

		/// <summary>
		/// Gets the Sliding Panels View Controller
		/// </summary>
		/// <value>The nav controller.</value>
		public SlidingPanelsNavigationViewController SlidingPanelsController 
		{
			get
			{
				return MasterNavigationController as SlidingPanelsNavigationViewController;
			}
		}

		protected IMvxTouchViewCreator ViewCreator
		{
			get { return _viewCreator ?? (_viewCreator = Mvx.Resolve<IMvxTouchViewCreator>()); }
		}

		/// <summary>
		/// Gets the root controller for the application
		/// </summary>
		/// <value>The root controller.</value>
		public UIViewController RootController 
		{
			get;
			private set;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="MvxSlidingPanels.Touch.MvxSlidingPanelsTouchViewPresenter"/> class.
		/// </summary>
		/// <param name="applicationDelegate">Application delegate.</param>
		/// <param name="window">Window.</param>
		public MvxSlidingPanelsTouchViewPresenter(UIApplicationDelegate applicationDelegate, UIWindow window) :
		base(applicationDelegate, window)
		{
			// specialized construction logic goes here
			_window = window;
		}

		/// <summary>
		/// Override this when custom presentation logic is needed from your view models
		/// </summary>
		/// <param name="hint">Hint.</param>
		public override void ChangePresentation (Cirrious.MvvmCross.ViewModels.MvxPresentationHint hint)
		{
			base.ChangePresentation(hint);
		}

		/// <summary>
		/// Shows the first view.
		/// This method has been overridden to automatically create the sliding panels view controller.
		/// Also, for each of the sliding panels (left, right and bottom) a View/View Model is created
		/// and set into a panel container managed by the sliding panel view controller.
		/// </summary>
		/// <param name="viewController">View controller.</param>
		protected override void ShowFirstView (UIViewController viewController)
		{
			// Show the first view
			base.ShowFirstView (viewController);

			// create the Sliding Panels View Controller and make it a child controller
			// of the root controller
			RootController.AddChildViewController (SlidingPanelsController);
			RootController.View.AddSubview (SlidingPanelsController.View);

			// use the first view to create the sliding panels 
			//AddPanel<RightPanelViewModel>(PanelType.LeftPanel, viewController as MvxViewController);
			AddPanel<RightPanelViewModel>(PanelType.RightPanel, viewController as MvxViewController);
			//AddPanel<RightPanelViewModel>(PanelType.BottomPanel, viewController as MvxViewController);
		}

		/// <summary>
		/// Creates a ViewModel of the desired type (as well as the  ppropriate View) and inserts it into
		/// the correct Panel Container and inserts the panel container into the provided sliding panel
		/// view controller
		/// </summary>
		/// <param name="panelType">Panel type.</param>
		/// <param name="mvxController">Mvx controller.</param>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		protected void AddPanel<T>(PanelType panelType, MvxViewController mvxController) where T : MvxViewModel
		{

			// use the first view to create a view of the desired type
			// We only do this because there's no convenient way to create a view from inside the presenter
			var menuRequest = new MvxViewModelRequest<RightPanelViewModel>(new MvxBundle(null), null, MvxRequestedBy.UserAction);
			var viewToAdd = (UIViewController)ViewCreator.CreateView(menuRequest);		

			// Insert the view into a new container (of the right type) and insert 
			// that into the sliding panels controller
			switch (panelType)
			{
			case PanelType.LeftPanel:
				_leftPanelContainer = new LeftPanelContainer (viewToAdd);
				SlidingPanelsController.InsertPanel(_leftPanelContainer);
				break;

			case PanelType.RightPanel:
				_rightPanelContainer = new RightPanelContainer (viewToAdd);
				SlidingPanelsController.InsertPanel(_rightPanelContainer);
				break;

			case PanelType.BottomPanel:
				_bottomPanelContainer = new BottomPanelContainer (viewToAdd);
				SlidingPanelsController.InsertPanel(_bottomPanelContainer);
				break;

			default:
				throw new ArgumentException("PanelType is invalid");
			};
		}

		/// <summary>
		/// Overrides the default presenters method so that we can instead create a sliding panel
		/// view controller and use that as our application navigation view controller
		/// </summary>
		/// <returns>The navigation controller.</returns>
		/// <param name="viewController">View controller.</param>
		protected override UINavigationController CreateNavigationController (UIViewController viewController)
		{
			SlidingPanelsNavigationViewController navController = new SlidingPanelsNavigationViewController (viewController);
			RootController = new UIViewController ();
			return navController;
		}

		/// <summary>
		/// Sets the window root view controller.
		/// </summary>
		/// <param name="controller">Controller.</param>
		protected override void SetWindowRootViewController(UIViewController controller)
		{
			_window.AddSubview(RootController.View);
			_window.RootViewController = RootController;
		}

		public override void Show(MvxViewModelRequest request)
		{
			if (_rightPanelContainer != null && _rightPanelContainer.IsVisible)
				SlidingPanelsController.TogglePanel (PanelType.RightPanel);
			else if (_leftPanelContainer != null && _leftPanelContainer.IsVisible)
				SlidingPanelsController.TogglePanel (PanelType.LeftPanel);
			else if (_bottomPanelContainer != null && _bottomPanelContainer.IsVisible)
				SlidingPanelsController.TogglePanel (PanelType.BottomPanel);

			if (request.PresentationValues != null)
			{
				if (request.PresentationValues.ContainsKey(PresentationBundleFlagKeys.ClearStack))
				{
					clearStackAndNavigate(request);
					return;
				}
			}

			base.Show(request);
		}

		private void clearStackAndNavigate(MvxViewModelRequest request)
		{
			var nextViewController = (UIViewController)ViewCreator.CreateView(request);

			if (MasterNavigationController.TopViewController.GetType() != nextViewController.GetType())
			{
				MasterNavigationController.PopToRootViewController(false);
				MasterNavigationController.ViewControllers = new UIViewController[] { nextViewController };
			}
		}
	}
}

