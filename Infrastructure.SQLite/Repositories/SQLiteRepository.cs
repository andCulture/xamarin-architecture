using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.IO;
using System.Linq;
using Core.Domain.Interfaces.Repositories;
using Core.Domain.Entities.Base;
using Core.Application.Interfaces.Services;
using SQLite;
using Core.Application.Injection;

namespace Infrastructure.SQLite.Repositories {
    public class SQLiteRepository<T> : IRepository<T> where T : BaseEntity<T>, new() {
        private IConfigService _configService = Injector.Resolve<IConfigService>();

        public virtual string DatabasePath {
            get {
                return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "Andculture.Xamarin-Architecture.db3");
            }
        }

        protected SQLiteConnection GetConnection() {
            var returnValue = new SQLiteConnection(DatabasePath);
            returnValue.CreateTable<T>();
            return returnValue;
        }

        public void Delete(List<T> itemsToDelete) {
            if (itemsToDelete != null) {
                var ids = string.Join(",", itemsToDelete.Select(x => x.Id).ToList());

                using (var connection = GetConnection()) {
                    DeleteBatch(connection, ids);
                }
            }
        }

        public void Delete(int id) {
            using (var connection = GetConnection()) {
                var item = connection.Get<T>(id);

                if (item == null) {
                    throw new ArgumentException(string.Format("Item with Id '{0}' not found", id));
                }
                connection.Delete(item);
            }
        }

        public T Get(int id) {
            using (var connection = GetConnection()) {
                return connection.Get<T>(id);
            }
        }

        public List<T> GetAll() {
            using (var connection = GetConnection()) {
                return connection.Table<T>().ToList();
            }
        }

        public List<T> GetTop(Expression<Func<T, bool>> where, int numberToReturn) {
            using (var connection = GetConnection()) {
                return connection.Table<T>().Where(where).OrderBy(x => x.Id).Take(numberToReturn).ToList();
            }
        }

        public void Save(T item) {
            if (item == null) {
                throw new ArgumentException("Item is null");
            }

            if (item.IsNew()) {
                Insert(item);
            } else {
                Update(item);
            }
        }

        public void SaveAll(List<T> items) {
            if (items == null) {
                throw new ArgumentException("No Items provided");
            }

            if (items.Any(x => x.IsNew())) {
                InsertAll(items.Where(x => x.IsNew()).ToList());
            }
            if (items.Any(x => !x.IsNew())) {
                UpdateAll(items.Where(x => !x.IsNew()).ToList());
            }
        }

        public List<T> Where(Expression<Func<T, bool>> where) {
            using (var connection = GetConnection()) {
                return connection.Table<T>().Where(where).ToList();
            }
        }

        #region Private Methods
        private int DeleteBatch(SQLiteConnection conn, string commaSeparatedPKs) {
            var map = conn.GetMapping(typeof(T));
            var pk = map.PK;
            if (pk == null) {
                throw new NotSupportedException("Cannot delete " + map.TableName + ": it has no PK");
            }

            var q = string.Format("delete from \"{0}\" where \"{1}\" in ({2})", map.TableName, pk.Name, commaSeparatedPKs);

            return conn.Execute(q);
        }

        private void Insert(T item) {
            if (item == null || item == default(T)) {
                throw new ArgumentException("Item is null");
            }

            using (var connection = GetConnection()) {
                connection.Insert(item);
            }
        }

        private void InsertAll(List<T> items) {
            if (items == null || !items.Any()) {
                throw new ArgumentException("No Items provided");
            }

            using (var connection = GetConnection()) {
                connection.InsertAll(items);
            }
        }

        private void Update(T item) {
            if (item == null || item == default(T)) {
                throw new ArgumentException("Item is null");
            }

            using (var connection = GetConnection()) {
                connection.Update(item);
            }
        }

        private void UpdateAll(List<T> items) {
            if (items == null || !items.Any()) {
                throw new ArgumentException("No Items provided");
            }

            using (var connection = GetConnection()) {
                connection.UpdateAll(items);
            }
        }
        #endregion
    }
}
