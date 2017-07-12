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
			var user = _repository.Query<User>(id);
            _repository.Remove<User>(user, isSoft);
		}

		public override void DeleteAll(bool isSoft = true)
		{
			_repository.RemoveAll<User>(isSoft);
		}

		public override IQueryable<IEntityBase> GetAll(bool includeDeleted = false)
		{
            return GetAll<Core.Models.User, User>(includeDeleted);
		}

        public override IEntityBase GetById(string id)
		{
            var realmUser = _repository.Query<User>(id);
            return _mappingEngine.Map<Core.Models.User>(realmUser);
        }

		public override void Save(IEntityBase entity)
		{
			var realmUser = _mappingEngine.Map<User>(entity);
            _repository.AddOrUpdate(realmUser);
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
