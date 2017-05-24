using Mobile.iOS.ViewControllers.Base;
using UIKit;

namespace Mobile.iOS.ViewControllers.Profile
{
	internal class ProfileLayout : BaseLayout
	{
		#region Constants

		#endregion Constants

		#region Properties

		internal ProfilePresenter Presenter { get; private set; }

		#endregion Properties

		#region Constructors

		internal ProfileLayout(ProfilePresenter presenter)
		{
			Presenter = presenter;
		}

		#endregion Constructors

		#region BaseLayout Implementation

		/// <summary>
		/// Lays out the subviews inside the presenter's super view.
		/// </summary>
		internal override void ConfigureLayoutConstraints()
		{
		}

		#endregion BaseLayout Implementation

		#region Private Methods

		#endregion Private Methods
	}
}
