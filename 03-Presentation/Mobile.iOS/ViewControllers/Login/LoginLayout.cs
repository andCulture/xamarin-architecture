using Mobile.iOS.ViewControllers.Base;
using UIKit;

namespace Mobile.iOS.ViewControllers.Login
{
	internal class LoginLayout : BaseLayout
	{
        #region Constants

        private const float FIELD_SIZE = 30f;
        private const float MARGIN = 20f;

		#endregion Constants

		#region Properties

		internal LoginPresenter Presenter { get; private set; }

		#endregion Properties

		#region Constructors

		internal LoginLayout(LoginPresenter presenter)
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
			// Logo
			Presenter.Settings.ContentView.AddConstraints(new[] {
				NSLayoutConstraint.Create(Presenter.AvatarImage, NSLayoutAttribute.Top, NSLayoutRelation.Equal, Presenter.Settings.ContentView, NSLayoutAttribute.Top, 1, 2f * MARGIN),
				NSLayoutConstraint.Create(Presenter.AvatarImage, NSLayoutAttribute.CenterX, NSLayoutRelation.Equal, Presenter.Settings.ContentView, NSLayoutAttribute.CenterX, 1, 0),
				NSLayoutConstraint.Create(Presenter.AvatarImage, NSLayoutAttribute.Height, NSLayoutRelation.Equal, null, NSLayoutAttribute.NoAttribute, 1, Presenter.AvatarImage.FillHeight),
				NSLayoutConstraint.Create(Presenter.AvatarImage, NSLayoutAttribute.Width, NSLayoutRelation.Equal, null, NSLayoutAttribute.NoAttribute, 1, Presenter.AvatarImage.FillWidth)
			});
			// Username
			Presenter.Settings.ContentView.AddConstraints(new[] {
				NSLayoutConstraint.Create(Presenter.UsernameField, NSLayoutAttribute.Top, NSLayoutRelation.Equal, Presenter.AvatarImage, NSLayoutAttribute.Bottom, 1, 2f * MARGIN),
				NSLayoutConstraint.Create(Presenter.UsernameField, NSLayoutAttribute.Height, NSLayoutRelation.Equal, null, NSLayoutAttribute.NoAttribute, 1, FIELD_SIZE),
				NSLayoutConstraint.Create(Presenter.UsernameField, NSLayoutAttribute.Left, NSLayoutRelation.Equal, Presenter.Settings.ContentView, NSLayoutAttribute.Left, 1, MARGIN),
                NSLayoutConstraint.Create(Presenter.UsernameField, NSLayoutAttribute.Right, NSLayoutRelation.Equal, Presenter.Settings.ContentView, NSLayoutAttribute.Right, 1, -MARGIN)
			});
			// Password
			Presenter.Settings.ContentView.AddConstraints(new[] {
				NSLayoutConstraint.Create(Presenter.PasswordField, NSLayoutAttribute.Top, NSLayoutRelation.Equal, Presenter.UsernameField, NSLayoutAttribute.Bottom, 1, MARGIN),
				NSLayoutConstraint.Create(Presenter.PasswordField, NSLayoutAttribute.Height, NSLayoutRelation.Equal, null, NSLayoutAttribute.NoAttribute, 1, FIELD_SIZE),
				NSLayoutConstraint.Create(Presenter.PasswordField, NSLayoutAttribute.Left, NSLayoutRelation.Equal, Presenter.Settings.ContentView, NSLayoutAttribute.Left, 1, MARGIN),
				NSLayoutConstraint.Create(Presenter.PasswordField, NSLayoutAttribute.Right, NSLayoutRelation.Equal, Presenter.Settings.ContentView, NSLayoutAttribute.Right, 1, -MARGIN)
			});
			// Login Button
			Presenter.Settings.ContentView.AddConstraints(new[] {
				NSLayoutConstraint.Create(Presenter.SubmitButton, NSLayoutAttribute.Top, NSLayoutRelation.Equal, Presenter.PasswordField, NSLayoutAttribute.Bottom, 1, MARGIN * 2f),
				NSLayoutConstraint.Create(Presenter.SubmitButton, NSLayoutAttribute.Height, NSLayoutRelation.Equal, null, NSLayoutAttribute.NoAttribute, 1, FIELD_SIZE * 1.5f),
				NSLayoutConstraint.Create(Presenter.SubmitButton, NSLayoutAttribute.Left, NSLayoutRelation.Equal, Presenter.Settings.ContentView, NSLayoutAttribute.Left, 1, MARGIN),
				NSLayoutConstraint.Create(Presenter.SubmitButton, NSLayoutAttribute.Right, NSLayoutRelation.Equal, Presenter.Settings.ContentView, NSLayoutAttribute.Right, 1, -MARGIN)
			});
		}

		#endregion BaseLayout Implementation

		#region Private Methods

		#endregion Private Methods
	}
}
