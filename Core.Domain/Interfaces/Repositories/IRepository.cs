using Core.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Core.Domain.Interfaces.Repositories {
    public interface IRepository<T> where T : BaseEntity<T>, new() {
        void Save(T item);
        void SaveAll(List<T> items);
        void Delete(int id);
        void Delete(List<T> itemsToDelete);

        List<T> GetAll();
        T Get(int id);
        List<T> GetTop(Expression<Func<T, bool>> where, int numberToReturn);
        List<T> Where(Expression<Func<T, bool>> where);
    }
}
