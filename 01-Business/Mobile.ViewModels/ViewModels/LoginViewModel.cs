using System;
using System.Threading.Tasks;
using Acr.UserDialogs;
using Mobile.Core.Interfaces.Services.Database;
using Mobile.Core.Models;
using MvvmCross.Core.ViewModels;

namespace Mobile.ViewModels.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        #region Constants

        const string LOGIN_TEXT = "Login";
        const string PASSWORD_TEXT = "Password";
        const string USERNAME_TEXT = "Username";

        #endregion Constants

        #region Member Variables

		string _password;
        string _username;
		readonly IUserDialogs _userDialog;
        readonly IUserService _userService;

		#endregion Member Variables

		#region Constructors

		public LoginViewModel(IUserService userService, IUserDialogs userDialog) : base()
        {
			_userDialog = userDialog;
            _userService = userService;
        }

        #endregion Constructors

        #region Overrides

        public override Task Initialize()
        {
            //TODO: Add starting logic here
            return base.Initialize();
        }

        #endregion Overrides

        #region Public Properties

        public string LoginButtonText => LOGIN_TEXT;

        public IMvxCommand LoginCommand => new MvxCommand(LoginUser);

		public string Password
		{
			get
			{
				return _password;
			}
			set
			{
				SetProperty(ref _password, value);
			}
		}

        public string PasswordPlaceholderText => PASSWORD_TEXT;

		public string Username
		{
			get
			{
				return _username;
			}
			set
			{
				SetProperty(ref _username, value);
			}
		}

        public string UsernamePlaceholderText => USERNAME_TEXT;

		#endregion Public Properties

		#region Private Methods

        void LoginUser()
        {
			// TODO: Replace with actual logic
            try
            {
                _userService.Save(new Core.Models.User{
                    CreatedBy = "Test",
                    CreatedOn = DateTime.Now,
                    Email = "test@test.com",
                    FirstName = "Test",
                    Id = "2f89db5ae1e04679a22cd48167969a08",
                    LastName = "User"
                }); 
                var user = _userService.GetByEmail(Username);
                _userDialog.Alert($"You logged in as {user.FirstName} {user.LastName}", "Success");
            }
			catch(Exception ex)
            {
                // TODO: Implement logging service
                Errors.Add(new Error {
                    ErrorType = Core.Enumerations.ErrorType.Error,
                    Key = "LoginUser",
                    Message = $"Unable to find a user with the email {Username}."
                });
            }
        }

		#endregion Private Methods
	}
}