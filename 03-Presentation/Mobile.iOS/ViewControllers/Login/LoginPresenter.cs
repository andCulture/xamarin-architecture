using Mobile.iOS.Extensions;
using Mobile.iOS.Utilities;
using Mobile.iOS.ViewControllers.Base;
using UIKit;
using XamSvg;

namespace Mobile.iOS.ViewControllers.Login
{
	internal class LoginPresenter : BasePresenter
	{
        #region Constants

        private const float TEXT_SIZE = 16f;

		#endregion Constants

		#region Properties

		internal LoginPresenterSettings Settings { get; private set; }

        #region Views

        internal UISvgImageView AvatarImage     { get; private set; }
        internal UITextField    PasswordField   { get; private set; }
		internal UIButton       SubmitButton    { get; private set; }
		internal UITextField    UsernameField   { get; private set; }

		#endregion Views

		#endregion Properties

		#region Constructor

		internal LoginPresenter(LoginPresenterSettings settings)
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
            Settings.ContentView.Superview.BackgroundColor = AppColors.WHITE.ToUIColor();
            Settings.ContentView.BackgroundColor = AppColors.WHITE.ToUIColor();
			// Avatar Image
			AvatarImage = new UISvgImageView("res:avatar", 80f, 80f)
			{
				TranslatesAutoresizingMaskIntoConstraints = false
			};
            // Username field
            UsernameField = new UITextField
            {
				AutocapitalizationType = UITextAutocapitalizationType.None,
                AutocorrectionType = UITextAutocorrectionType.No,
                Font = UIFont.SystemFontOfSize(TEXT_SIZE, UIFontWeight.Regular),
                Placeholder = "Username",
				TextColor = AppColors.BLACK.ToUIColor(),
                TranslatesAutoresizingMaskIntoConstraints = false,
            };
			// Password field
			PasswordField = new UITextField
			{
				Font = UIFont.SystemFontOfSize(TEXT_SIZE, UIFontWeight.Regular),
				Placeholder = "Password",
                SecureTextEntry = true,
				TextColor = AppColors.BLACK.ToUIColor(),
				TranslatesAutoresizingMaskIntoConstraints = false,
			};
            // Submit Button
            SubmitButton = new UIButton(UIButtonType.RoundedRect)
            {
                TranslatesAutoresizingMaskIntoConstraints = false,
            };
            SubmitButton.TouchUpInside += Settings.OnSubmitButtonTapped;
            SubmitButton.SetTitle("Login", UIControlState.Normal);
            // Add the subviews to the content view.
			Settings.ContentView.AddSubviews(AvatarImage, UsernameField, PasswordField, SubmitButton);
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
