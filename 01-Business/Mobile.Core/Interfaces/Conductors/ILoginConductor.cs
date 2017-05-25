using Mobile.Core.Interfaces.Entities;
using Mobile.Core.Models.Views;

namespace Mobile.Core.Interfaces.Conductors
{
	public interface ILoginConductor<TUser> : IConductor
		where TUser : IUser
	{
		UserView LoginUser(string email, string password);
	}
}
