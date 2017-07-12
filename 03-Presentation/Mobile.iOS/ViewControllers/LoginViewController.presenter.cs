using Mobile.iOS.Extensions;
using Mobile.iOS.Utilities;
using Mobile.ViewModels.ViewModels;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Binding.iOS;
using UIKit;
using XamSvg;

namespace Mobile.iOS.ViewControllers
{
    public partial class LoginViewController
    {
        #region Constants

        const float TEXT_SIZE = 16f;

        #endregion Constants

        #region Properties

        #region Views

        UISvgImageView AvatarImage { get; set; }
        UITextField PasswordField { get; set; }
        UIButton SubmitButton { get; set; }
        UITextField UsernameField { get; set; }

        #endregion Views

        #endregion Properties

        /// <summary>
        /// Configures the views for the settings screen.
        /// </summary>
        void InitializeViews()
        {
            ContentView.Superview.BackgroundColor = AppColors.WHITE.ToUIColor();
            ContentView.BackgroundColor = AppColors.WHITE.ToUIColor();
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
                TextColor = AppColors.BLACK.ToUIColor(),
                TranslatesAutoresizingMaskIntoConstraints = false,
            };
            // Password field
            PasswordField = new UITextField
            {
                Font = UIFont.SystemFontOfSize(TEXT_SIZE, UIFontWeight.Regular),
                SecureTextEntry = true,
                TextColor = AppColors.BLACK.ToUIColor(),
                TranslatesAutoresizingMaskIntoConstraints = false,
            };
            // Submit Button
            SubmitButton = new UIButton(UIButtonType.RoundedRect)
            {
                TranslatesAutoresizingMaskIntoConstraints = false,
            };
            // Add the subviews to the content view.
            ContentView.AddSubviews(AvatarImage, UsernameField, PasswordField, SubmitButton);
            SetupMvxBindings();
        }

        void SetupMvxBindings()
        {
            var bindings = this.CreateBindingSet<LoginViewController, LoginViewModel>();
            bindings.Bind(UsernameField)
               .For(t => t.Text)
               .To(vm => vm.Username);
            bindings.Bind(UsernameField)
               .For(t => t.Placeholder)
               .To(vm => vm.UsernamePlaceholderText)
               .OneTime();
            bindings.Bind(PasswordField)
               .For(t => t.Text)
               .To(vm => vm.Password);
            bindings.Bind(PasswordField)
               .For(t => t.Placeholder)
               .To(vm => vm.PasswordPlaceholderText)
               .OneTime();
			bindings.Bind(SubmitButton)
			   .For(b => b.BindTitle())
			   .To(vm => vm.LoginButtonText)
			   .OneTime();
            bindings.Bind(SubmitButton)
               .For(b => b.BindTouchUpInside())
               .To(vm => vm.LoginCommand);
            bindings.Apply();
        }
    }
}
