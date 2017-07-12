using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Mobile.Core.Interfaces.Entities;
using Mobile.Core.Interfaces.Services.Database;
using Mobile.Services.Realm.Services;

namespace Mobile.Services.Realm
{
	public abstract class ServiceBase : IServiceBase
	{
		#region Protected Members

		protected readonly IMapper _mappingEngine;
		protected readonly RepositoryService _repository;

		#endregion Protected Members

		#region Constructor

		protected ServiceBase(IMapper mappingEngine)
		{
            _mappingEngine = mappingEngine;
            _repository = new RepositoryService();
		}

		#endregion Constructor

		#region Abstract Methods

		public abstract void Delete(string id, bool isSoft = true);
		public abstract void DeleteAll(bool isSoft = true);
		public abstract IQueryable<IEntityBase> GetAll(bool includeDeleted = false);
		public abstract IEntityBase GetById(string id);
		public abstract void Save(IEntityBase entity);

		#endregion Abstract Methods

		#region Protected Methods

		protected IQueryable<TModel> GetAll<TModel, TRealm>(bool includeDeleted = false)
			where TModel : IEntityBase
			where TRealm : Realms.RealmObject, IEntityBase
		{
			var models = new List<TModel>();
			var realmEntities = _repository.QueryAll<TRealm>(includeDeleted);
			if (realmEntities == null)
			{
				return models.AsQueryable();
			}
			foreach (var e in realmEntities)
			{
				models.Add(_mappingEngine.Map<TModel>(e));
			}
			return models.AsQueryable();
		}

		#endregion Protected Methods
	}
}
