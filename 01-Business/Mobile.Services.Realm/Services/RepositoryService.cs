using System;
using System.Collections.Generic;
using System.Linq;
using Mobile.Core.Interfaces.Entities;
using Mobile.Services.Realm.Interfaces;
using Realms;

namespace Mobile.Services.Realm.Services
{
	public class RepositoryService : IRepositoryService
	{
		private readonly Realms.Realm _realm;
		private readonly RealmConfiguration _config;

		public RepositoryService()
		{
			_realm = Realms.Realm.GetInstance();
			AttachErrorHandler();
		}

		public RepositoryService(string databasePath)
		{
			_realm = Realms.Realm.GetInstance(databasePath);
			AttachErrorHandler();
		}

		public RepositoryService(RealmConfiguration config)
		{
			_config = config;
            _realm = GetInstance();
			AttachErrorHandler();
		}

		public Realms.Realm GetInstance()
		{
            if (_config != null)
            {
                return Realms.Realm.GetInstance(_config);
            }
            else
            {
                return Realms.Realm.GetInstance();
            }
		}

		private void AttachErrorHandler()
		{
			_realm.Error += OnRealmError;
		}

		public void AddOrUpdate<T>(T item) where T : RealmObject
		{
			_realm.Write(() =>
			{
				_realm.Add<T>(item, true);
			});
		}

		public void AddOrUpdate<T>(IEnumerable<T> items) where T : RealmObject
		{
			if (items.Count() == 0)
				return;

			_realm.Write(() =>
			{
				foreach (var item in items)
				{
					_realm.Add<T>(item, true);
				}
			});
		}

		public void Write(Action writeAction)
		{
			_realm.Write(writeAction);
		}

		public T Query<T>(long id) where T : RealmObject
		{
			return _realm.Find<T>(id);
		}

		public T Query<T>(string id) where T : RealmObject
		{
			if (string.IsNullOrEmpty(id))
				return null;
			return _realm.Find<T>(id);
		}

		public RealmObject Query(string className, long id)
		{
			return _realm.Find(className, id);
		}

		public RealmObject Query(string className, string id)
		{
			if (string.IsNullOrEmpty(id))
				return null;
			return _realm.Find(className, id);
		}

		public IQueryable<dynamic> QueryAll(string className)
		{
			return _realm.All(className);
		}

		public IQueryable<T> QueryAll<T>(bool includeDeleted = false) where T : RealmObject, IEntityBase
		{
            if(includeDeleted)
            {
                return _realm.All<T>();
            }
            return _realm.All<T>().Where(e => e.DeletedOn == null);
		}

		public void RemoveAll()
		{
			_realm.Write(() => { _realm.RemoveAll(); });
		}

		public void RemoveAll(string className)
		{
			_realm.Write(() => { _realm.RemoveAll(className); });
		}

		public void RemoveAll<T>() where T : RealmObject
		{
			_realm.Write(() => { _realm.RemoveAll<T>(); });
		}

		public void RemoveAll<T>(bool isSoft = true) where T : RealmObject, IEntityBase
		{
            if(isSoft) {
                var items = QueryAll<T>();
                _realm.Write(() => {
                    foreach(var i in items)
                    {
                        i.DeletedOn = DateTime.Now;
                        _realm.Add<T>(i, true);
                    }
                });
            }
            else
            {
                RemoveAll<T>();
            }
		}

		public void Remove<T>(T item) where T : RealmObject
		{
			_realm.Write(() => { _realm.Remove(item); });
		}

		public void Remove<T>(T item, bool isSoft = true) where T : RealmObject, IEntityBase
		{
            if(isSoft)
            {
                _realm.Write(() => {
                    item.DeletedOn = DateTime.Now;
                });
            }
            else
            {
                _realm.Write(() => { _realm.Remove(item); });    
            }
		}

		public void RemoveRange<T>(IQueryable<T> range) where T : RealmObject
		{
			_realm.Write(() => { _realm.RemoveRange<T>(range); });
		}

		public void Refresh()
		{
			_realm.Refresh();
		}

		public void Dispose()
		{
			_realm.Dispose();
		}

		public void DeleteRealm()
		{
			Realms.Realm.DeleteRealm(_realm.Config);
		}

		private void OnRealmError(object sender, ErrorEventArgs args)
		{
			// TODO Add analytics here
		}

		public void Dispose(bool disposing)
		{
			if (disposing)
			{
				_realm.Error -= OnRealmError;
			}
		}
	}
}
