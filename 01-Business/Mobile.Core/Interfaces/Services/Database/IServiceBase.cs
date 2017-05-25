using Mobile.Core.Interfaces.Entities;
using System.Linq;

namespace Mobile.Core.Interfaces.Services.Database
{
	public interface IServiceBase<T> where T : IEntityBase
	{
		IQueryable<T> 		GetAll();
		T 					GetById(int id);
		T 					Save(T entity);
		void 				Delete(int id, bool isSoft = true);
		void 				DeleteAll(bool isSoft = true);
	}
}
