using System;
using System.Linq;
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

        string _message;
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
            LoadData();
            //TODO: Add starting logic here
            return base.Initialize();
        }

        #endregion Overrides

        #region Public Properties

        public string LoginButtonText => LOGIN_TEXT;

        public IMvxCommand LoginCommand => new MvxCommand(LoginUser);

		public string Message
		{
			get
			{
				return _message;
			}
			set
			{
				SetProperty(ref _message, value);
			}
		}

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

		#region Public Methods

		public async Task LoadData()
		{
            _userDialog.ShowLoading("Processing", MaskType.Black);
            await Task.Factory.StartNew(SeedUsers);
            _userDialog.HideLoading();
		}

		#endregion Public Methods

		#region Private Methods

		void LoginUser()
        {
			// TODO: Replace with actual logic
            try
            {
                _userService.Save(new User {
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

        /// <summary>
        /// This method is intended to test/demonstrate the interaction with the database across an async thread.
        /// </summary>
        void SeedUsers()
        {
			int count = 1;
            // Ensure we have 10 users.
			while (count < 11)
			{
				_userService.Save(new User
				{
					CreatedBy = "Test",
					CreatedOn = DateTime.Now,
					Email = $"test@test{count.ToString()}.com",
					FirstName = "Test",
					Id = count.ToString(),
					LastName = $"User{count.ToString()}"
				});
				count++;
			}
            // Get all the users we just saved to ensure they are persisited in the database. 
            var users = _userService.GetAll().ToList();

            if(users == null || users.Count == 0)
            {
                return;
            }
            // Get the first user, and update their information.
            var featuredUser = users[0];
            featuredUser.LastName = "Succup";
            featuredUser.Email = "suckup@email.com";
            // Save the changes to the user
            _userService.Save(featuredUser);
            // Requery the user to ensure that the changes were persisted in the database.
            var updatedUser = _userService.GetByEmail(featuredUser.Email);

            Message = $"Join the {users.Count.ToString()} users! Take it from {updatedUser.FirstName} {updatedUser.LastName} 'I love this app!'";
        }

		#endregion Private Methods
	}
}