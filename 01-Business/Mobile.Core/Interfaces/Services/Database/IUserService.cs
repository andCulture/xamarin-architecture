﻿using Mobile.Core.Interfaces.Entities;

namespace Mobile.Core.Interfaces.Services.Database
{
	public interface IUserService : IServiceBase
	{
        IUser GetByEmail(string email);
	}
}
