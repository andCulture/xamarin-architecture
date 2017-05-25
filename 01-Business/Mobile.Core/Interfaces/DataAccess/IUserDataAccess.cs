using Mobile.Core.Interfaces.Entities;
using Mobile.Core.Models.Views;

namespace Mobile.Core.Interfaces.DataAccess
{
	public interface IUserDataAccess<TUser> : IDataAccess
		where TUser : IUser
	{
		UserView CreateUser(TUser user);
		UserView GetUser(int id);
        UserView GetUserByEmail(string email);
	}
}
