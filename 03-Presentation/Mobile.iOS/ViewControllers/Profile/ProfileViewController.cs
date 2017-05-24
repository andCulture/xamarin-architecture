using Autofac;
using Mobile.Core.Interfaces.Conductors;
using Mobile.iOS.ViewControllers.Base;
using Mobile.Presentation.Shared.ApplicationObjects;
using Mobile.Services.Realm;

namespace Mobile.iOS.ViewControllers.Profile
{
	public partial class ProfileViewController : BaseScrollViewController
	{
		#region Private Members

		private IProfileConductor<User> _conductor;

		#endregion Private Members

		#region Variables

		internal ProfilePresenter Presenter { get; private set; }
		internal ProfileLayout Layout { get; private set; }

		#endregion Variables

		#region Constructors

		public ProfileViewController() : base()
		{
		}

		#endregion Constructors

		#region Overrides

		protected override void ResolveConductors()
		{
			using (var scope = AppContainer.Container.BeginLifetimeScope())
      		{
				_conductor = scope.Resolve<IProfileConductor<User>>();
      		}
		}

		protected override string ScreenName
		{
			get
			{
				return "Profile";
			}
		}

		/// <summary>
		/// Creates view and applies layout contraints.
		/// </summary>
		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			Presenter = new ProfilePresenter(new ProfilePresenterSettings()
			{
				ContentView = ContentView
			});
			Layout = new ProfileLayout(Presenter);
			RegisterPresenterAndLayout(Presenter, Layout);
		}

		/// <summary>
		/// Runs prior to the view appearing. This is where we perform logic that relies on subviews having sizing data.
		/// </summary>
		/// <param name="animated"></param>
		public override void ViewWillAppear(bool animated)
		{
			base.ViewWillAppear(animated);
		}

		#endregion Overrides

		#region Private Methods

		#endregion Private Methods

		#region Event Handlers

		#endregion Event Handlers
	}
}