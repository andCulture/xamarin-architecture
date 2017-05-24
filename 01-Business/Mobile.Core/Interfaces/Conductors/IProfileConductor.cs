using Mobile.Core.Interfaces.Entities;
using Mobile.Core.Models.Views;

namespace Mobile.Core.Interfaces.Conductors
{
	public interface IProfileConductor<TUser> : IConductor
		where TUser : IUser
	{
		UserView CreateUser(TUser user);
		UserView GetUserProfile(int id);
	}
}
