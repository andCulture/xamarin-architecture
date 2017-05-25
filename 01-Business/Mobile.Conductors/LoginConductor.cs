using System.Collections.Generic;
using Mobile.Core.Interfaces.Conductors;
using Mobile.Core.Interfaces.DataAccess;
using Mobile.Core.Interfaces.Entities;
using Mobile.Core.Models;
using Mobile.Core.Models.Views;

namespace Mobile.Conductors
{
	public class LoginConductor<TUser> : BaseConductor, ILoginConductor<TUser>
		where TUser : IUser
	{
		private readonly IUserDataAccess<TUser> _userAccess;

		public LoginConductor(IUserDataAccess<TUser> userAccess) : base()
		{
			_userAccess = userAccess;
		}

		#region IProfileConductor Implementation

		public UserView LoginUser(string email, string password)
		{
            // TODO: Replace with actual logic
			var user = _userAccess.GetUserByEmail(email);
            if(user.HasErrors)
            {
                user = new UserView
                {
                    Email = email,
                    FullName = "Test User",
                    Errors = new List<Error>()
                };
            }
            return user;
		}

		#endregion IProfileConductor Implementation
	}
}
