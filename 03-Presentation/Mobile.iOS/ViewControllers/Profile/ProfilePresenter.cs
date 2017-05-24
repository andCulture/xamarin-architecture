using Mobile.iOS.ViewControllers.Base;

namespace Mobile.iOS.ViewControllers.Profile
{
	internal class ProfilePresenter : BasePresenter
	{
		#region Constants

		#endregion Constants

		#region Properties

		internal ProfilePresenterSettings Settings { get; private set; }

		#region Views

		#endregion Views

		#endregion Properties

		#region Constructor

		internal ProfilePresenter(ProfilePresenterSettings settings)
		{
			Settings = settings;
		}

		#endregion Constructor

		#region BasePresenter Implementation

		/// <summary>
		/// Configures the views for the settings screen.
		/// </summary>
		internal override void InitializeViews()
		{
			
		}

		#endregion BasePresenter Implementation

		#region Internal Methods

		#endregion Internal Methods

		#region Private Methods

		#endregion Private Methods

		#region Event Handlers

		#endregion Event Handlers
	}
}
