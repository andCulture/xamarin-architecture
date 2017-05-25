using System;
using Autofac;
using Mobile.Core.Interfaces.Conductors;
using Mobile.iOS.ViewControllers.Base;
using Mobile.Presentation.Shared.ApplicationObjects;
using Mobile.Services.Realm;

namespace Mobile.iOS.ViewControllers.Login
{
	public class LoginViewController : BaseScrollViewController
	{
		#region Private Members

		private ILoginConductor<User> _conductor;

		#endregion Private Members

		#region Variables

		internal LoginLayout    Layout      { get; private set; }
        internal LoginPresenter Presenter   { get; private set; }

		#endregion Variables

		#region Constructors

		public LoginViewController() : base()
		{
		}

		#endregion Constructors

		#region Overrides

		/// <summary>
		/// Hide the status bar
		/// </summary>
		/// <returns><c>true</c>, if status bar hidden was preferred, <c>false</c> otherwise.</returns>
		public override bool PrefersStatusBarHidden()
		{
			return true;
		}

		protected override void ResolveConductors()
		{
			using (var scope = AppContainer.Container.BeginLifetimeScope())
      		{
				_conductor = scope.Resolve<ILoginConductor<User>>();
      		}
		}

		protected override string ScreenName
		{
			get
			{
				return "Login";
			}
		}

		/// <summary>
		/// Creates view and applies layout contraints.
		/// </summary>
		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			Presenter = new LoginPresenter(new LoginPresenterSettings()
			{
				ContentView = ContentView,
                OnSubmitButtonTapped = OnLoginButtonTapped,
			});
			Layout = new LoginLayout(Presenter);
			RegisterPresenterAndLayout(Presenter, Layout);
		}

		/// <summary>
		/// Runs prior to the view appearing. This is where we perform logic that relies on subviews having sizing data.
		/// </summary>
		/// <param name="animated"></param>
		public override void ViewWillAppear(bool animated)
		{
            NavigationController?.SetNavigationBarHidden(true, false);
			base.ViewWillAppear(animated);
		}

		#endregion Overrides

		#region Private Methods

		#endregion Private Methods

		#region Event Handlers

        private void OnLoginButtonTapped(object sender, EventArgs args)
        {
            var user = _conductor.LoginUser("test", "test");
            ShowAlert($"You logged in as {user.FullName}", "Success");
        }

		#endregion Event Handlers
	}
}