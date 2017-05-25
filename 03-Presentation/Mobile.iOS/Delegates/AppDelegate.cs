using Mobile.iOS.ViewControllers.Login;
using Mobile.Presentation.Shared.ApplicationObjects;
using Foundation;
using UIKit;
using Mobile.Svg;
using System.Reflection;
using Mobile.iOS.Extensions;
using Mobile.iOS.Utilities;

namespace Mobile.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the
    // User Interface of the application, as well as listening (and optionally responding) to application events from iOS.
    [Register("AppDelegate")]
    public class AppDelegate : UIApplicationDelegate
    {
        #region Overrides

        public override UIWindow Window
        {
            get;
            set;
        }

        public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
        {
            InitializeApp();
            return true;
        }

        public override void OnResignActivation(UIApplication application)
        {
            // Invoked when the application is about to move from active to inactive state.
            // This can occur for certain types of temporary interruptions (such as an incoming phone call or SMS message) 
            // or when the user quits the application and it begins the transition to the background state.
            // Games should use this method to pause the game.
        }

        public override void DidEnterBackground(UIApplication application)
        {
            // Use this method to release shared resources, save user data, invalidate timers and store the application state.
            // If your application supports background exection this method is called instead of WillTerminate when the user quits.
        }

        public override void WillEnterForeground(UIApplication application)
        {
            // Called as part of the transiton from background to active state.
            // Here you can undo many of the changes made on entering the background.
        }

        public override void OnActivated(UIApplication application)
        {
            // Restart any tasks that were paused (or not yet started) while the application was inactive. 
            // If the application was previously in the background, optionally refresh the user interface.
        }

        public override void WillTerminate(UIApplication application)
        {
            // Called when the application is about to terminate. Save data, if needed. See also DidEnterBackground.
        }

		#endregion Overrides

		#region Private Methods

        /// <summary>
        /// Initializes the app.
        /// </summary>
        private void InitializeApp()
        {
			// Initialize SVG
			XamSvg.Setup.InitSvgLib();
			//Tells XamSvg in which assembly to search for svg when "res:" is used
			XamSvg.Shared.Config.ResourceAssembly = typeof(SVGImages).GetTypeInfo().Assembly;

			// If not required for your application you can safely delete this method
			var setup = new AppSetup();
			AppContainer.Container = setup.CreateContainer();
			setup.InitObjectMapping();

            Window = new UIWindow(UIScreen.MainScreen.Bounds);
            var viewController = new LoginViewController();
			// Add the Navigation Controller and initialize it
            var navController = new UINavigationController(viewController);
            Window.RootViewController = navController;
            Window.MakeKeyAndVisible();
        }

		#endregion Private Methods
	}
}

