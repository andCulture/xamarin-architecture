using Mobile.Core.Interfaces.Entities;

namespace Mobile.Core.Interfaces.Services.Database
{
	public interface IUserService<T> : IServiceBase<T>
		where T : IUser
	{
        T GetByEmail(string email);
	}
}
