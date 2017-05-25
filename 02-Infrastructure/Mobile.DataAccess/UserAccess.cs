using System;
using System.Collections.Generic;
using AutoMapper;
using Mobile.Core.Enumerations;
using Mobile.Core.Interfaces.DataAccess;
using Mobile.Core.Interfaces.Entities;
using Mobile.Core.Interfaces.Services.Database;
using Mobile.Core.Models;
using Mobile.Core.Models.Views;

namespace Mobile.DataAccess
{
	public class UserAccess<TUser> : BaseAccess, IUserDataAccess<TUser>
		where TUser : IUser
	{
		private readonly IMapper _mappingEngine;
		private readonly IUserService<TUser> _userDatabaseService;

		public UserAccess(IUserService<TUser> userDatabaseService, IMapper mappingEngine) : base()
		{
			_userDatabaseService = userDatabaseService;
			_mappingEngine = mappingEngine;
		}

		#region IUserDataAccess Implementation

		public UserView CreateUser(TUser user)
		{
			var vUser = new UserView
			{
				Errors = new List<Error>()
			};
			var userEntity = _userDatabaseService.Save(user);
			if (userEntity == null)
			{
				vUser.Errors.Add(new Error
				{
					ErrorType = ErrorType.Error,
					Key = "UserAccess.CreateUser",
					Message = $"Error saving user."
				});
				return vUser;
			}
			return _mappingEngine.Map<UserView>(userEntity);
		}

		/// <summary>
		/// Gets a user by id.
		/// </summary>
		/// <returns>The user view model</returns>
		/// <param name="id">User id</param>
		public UserView GetUser(int id)
		{
			var vUser = new UserView
			{
				Errors = new List<Error>()
			};

			var userEntity = _userDatabaseService.GetById(id);
			// Validate the result
			if (userEntity == null)
			{
				vUser.Errors.Add(new Error
				{
					ErrorType = ErrorType.Error,
					Key = "UserAccess.GetUser",
					Message = $"No user found with id: {id}."
				});
				return vUser;
			}
			// Set user view properties.
			vUser.Email = userEntity.Email;
			vUser.FullName = $"{userEntity.FirstName} {userEntity.LastName}";
			return vUser;
		}

        public UserView GetUserByEmail(string email)
        {
			var vUser = new UserView
			{
				Errors = new List<Error>()
			};

			var userEntity = _userDatabaseService.GetByEmail(email);
			// Validate the result
			if (userEntity == null)
			{
				vUser.Errors.Add(new Error
				{
					ErrorType = ErrorType.Error,
					Key = "UserAccess.GetUserByEmail",
					Message = $"No user found with email: {email}."
				});
				return vUser;
			}
			// Set user view properties.
			vUser.Email = userEntity.Email;
			vUser.FullName = $"{userEntity.FirstName} {userEntity.LastName}";
			return vUser;
        }

		#endregion IUserDataAccess Implementation
	}
}
