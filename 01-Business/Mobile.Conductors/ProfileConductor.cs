using Mobile.Core.Interfaces.Conductors;
using Mobile.Core.Interfaces.DataAccess;
using Mobile.Core.Interfaces.Entities;
using Mobile.Core.Models.Views;

namespace Mobile.Conductors
{
	public class ProfileConductor<TUser> : BaseConductor, IProfileConductor<TUser>
		where TUser : IUser
	{
		private readonly IUserDataAccess<TUser> _userAccess;

		public ProfileConductor(IUserDataAccess<TUser> userAccess) : base()
		{
			_userAccess = userAccess;
		}

		#region IProfileConductor Implementation

		public UserView CreateUser(TUser user)
		{
			return _userAccess.CreateUser(user);
		}

		public UserView GetUserProfile(int id)
		{
			return _userAccess.GetUser(id);
		}

		#endregion IProfileConductor Implementation
	}
}
