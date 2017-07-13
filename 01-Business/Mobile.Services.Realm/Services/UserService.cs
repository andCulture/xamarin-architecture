using System.Linq;
using AutoMapper;
using Mobile.Core.Interfaces.Entities;
using Mobile.Core.Interfaces.Services.Database;

namespace Mobile.Services.Realm
{
    public class UserService : ServiceBase, IUserService
	{
		#region Constructor

		public UserService(IMapper mappingEngine) : base(mappingEngine)
		{
		}

		#endregion Constructor

		#region ServiceBase Implementation

		public override void Delete(string id, bool isSoft = true)
		{
            Delete<User>(id, isSoft);
		}

		public override void DeleteAll(bool isSoft = true)
		{
			DeleteAll<User>();
		}

		public override IQueryable<IEntityBase> GetAll(bool includeDeleted = false)
		{
            return GetAll<Core.Models.User, User>(includeDeleted);
		}

        public override IEntityBase GetById(string id)
		{
            return GetById<User, Core.Models.User>(id);
        }

		public override void Save(IEntityBase entity)
		{
			Save<User>(entity);
		}

		#endregion ServiceBase Implementation

		#region IUserService Implementation

		public IUser GetByEmail(string email)
		{
            var realmUser = _repository.QueryAll<User>().Where(u => u.Email == email).FirstOrDefault();
            return _mappingEngine.Map<Core.Models.User>(realmUser);
		}

		#endregion IUserService Implementation
	}
}
