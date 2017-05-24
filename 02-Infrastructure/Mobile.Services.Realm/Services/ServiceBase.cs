using Mobile.Core.Interfaces.Entities;
using Mobile.Core.Interfaces.Services.Database;
using System.Linq;

namespace Mobile.Services.Realm
{
	public abstract class ServiceBase<T> : IServiceBase<T> 
		where T : IEntityBase
	{
		protected Realms.Realm _context;

	 	protected ServiceBase(Realms.Realm context)
		{
			_context = context;
		}

		public abstract void Delete(int id, bool isSoft = true);

		public abstract void DeleteAll(bool isSoft = true);

		public abstract IQueryable<T> GetAll();

		public abstract T GetById(int id);

		public abstract T Save(T entity);
	}
}
