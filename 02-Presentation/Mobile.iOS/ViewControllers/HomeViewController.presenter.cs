using Mobile.iOS.Extensions;
using Mobile.iOS.Utilities;
using Mobile.ViewModels.ViewModels;
using MvvmCross.Binding.BindingContext;
using UIKit;
using XamSvg;

namespace Mobile.iOS.ViewControllers
{
    public partial class HomeViewController
    {
        #region Properties

        #region Views

		UILabel EmailLabel { get; set; }
		UILabel EmailValue { get; set; }
        UILabel FirstNameLabel { get; set; }
        UILabel FirstNameValue { get; set; }
        UILabel LastNameLabel { get; set; }
        UILabel LastNameValue { get; set; }
		UILabel TitleLabel { get; set; }

        #endregion Views

        #endregion Properties

        /// <summary>
        /// Configures the views for the settings screen.
        /// </summary>
        void InitializeViews()
        {
            ContentView.Superview.BackgroundColor = AppColors.WHITE.ToUIColor();
            ContentView.BackgroundColor = AppColors.WHITE.ToUIColor();

            EmailLabel = GetLabel();
            EmailValue = GetLabel();
            FirstNameLabel = GetLabel();
            FirstNameValue = GetLabel();
            LastNameLabel = GetLabel();
            LastNameValue = GetLabel();
            TitleLabel = GetLabel();
            // Add the subviews to the content view.
            ContentView.AddSubviews(EmailLabel, EmailValue, FirstNameLabel, FirstNameValue, LastNameLabel, LastNameValue, TitleLabel);
            SetupMvxBindings();
        }

        void SetupMvxBindings()
        {
            var bindings = this.CreateBindingSet<HomeViewController, HomeViewModel>();
            bindings.Bind(EmailLabel)
               .For(l => l.Text)
               .To(vm => vm.EmailLabelText)
               .OneTime();
            bindings.Bind(EmailValue)
               .For(l => l.Text)
               .To(vm => vm.User.Email);
			bindings.Bind(FirstNameLabel)
			   .For(l => l.Text)
			   .To(vm => vm.FirstNameLabelText)
			   .OneTime();
			bindings.Bind(FirstNameValue)
			   .For(l => l.Text)
			   .To(vm => vm.User.FirstName);
			bindings.Bind(LastNameLabel)
			   .For(l => l.Text)
			   .To(vm => vm.LastNameLabelText)
			   .OneTime();
			bindings.Bind(LastNameValue)
			   .For(l => l.Text)
			   .To(vm => vm.User.LastName);
			bindings.Bind(TitleLabel)
			  .For(l => l.Text)
			  .To(vm => vm.TitleLabelText)
			  .OneTime();           
            bindings.Apply();
        }

        UILabel GetLabel()
        {
            return new UILabel
            {
                TranslatesAutoresizingMaskIntoConstraints = false,
                Lines = 0,
                LineBreakMode = UILineBreakMode.WordWrap
            };
        }
    }
}
