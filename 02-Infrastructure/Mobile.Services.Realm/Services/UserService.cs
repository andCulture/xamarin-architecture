using Mobile.Core.Interfaces.Services.Database;
using System;
using System.Linq;

namespace Mobile.Services.Realm
{
	public class UserService : ServiceBase<User>, IUserService<User>
	{
		#region Constructor

		public UserService(Realms.Realm context) : base(context)
		{
		}

		#endregion Constructor

		#region IUserService Implementation

		public override void Delete(int id, bool isSoft = true)
		{
			var user = GetById(id);
			if (user == null)
			{
				throw new Exception("User not found");
			}
			DeleteUser(user, isSoft);
		}

		public override void DeleteAll(bool isSoft = true)
		{
			var users = GetAll();
			if (users == null)
			{
				return;
			}
			foreach (var user in users)
			{
                DeleteUser(user, isSoft);
			}
		}

		public override IQueryable<User> GetAll()
		{
			return _context.All<User>().Where(u => u.DeletedOn == null);
		}

		public override User GetById(int id)
		{
			if (_context == null)
			{
				return null;
			}
			var users = _context.All<User>().Where(u => u.Id == id && u.DeletedOn == null);
			if (users == null || users.Count() == 0)
			{
				return null;
			}
			return users.First();
		}

		public override User Save(User entity)
		{
			User user = null;
			using (var trans = _context.BeginWrite())
			{
				user = _context.Add(obj: entity, update: true);
				trans.Commit();
			}
			return user;
		}

		#endregion IUserService Implementation

		#region Private Methods

		private void DeleteUser(User user, bool isSoft)
		{
			if (isSoft)
			{
                SoftDeleteUser(user);
			}
			else
			{
				HardDeleteUser(user);
			}
		}

		private void HardDeleteUser(User user)
		{
			using (var trans = _context.BeginWrite())
			{
				_context.Remove(user);
				trans.Commit();
			}
		}

		private void SoftDeleteUser(User user)
		{
			using (var trans = _context.BeginWrite())
            {
				user.DeletedOn = DateTimeOffset.Now;
				trans.Commit();
			}
		}

		#endregion Private Methods
	}
}
