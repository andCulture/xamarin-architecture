using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Core.Application.Interfaces.Services;
using Core.Domain.Entities;
using Core.Domain.Interfaces.Repositories;
using Core.Application.Injection;
using System.Linq;
using Core.Domain.ViewModels;

namespace Core.Application.Services {
    public class UserService : ServiceBase, IUserService {
        private IRepository<User> _repo = Injector.Resolve<IRepository<User>>();

        public User Get(int userId) {
            return _repo.GetTop(s => s.UserId == userId, 1).FirstOrDefault();
        }

        public void Save(User item) {
            _repo.Save(item);
        }

        public void SaveAll(List<User> items) {
            _repo.SaveAll(items);
        }

        public void Delete(int id) {
            _repo.Delete(id);
        }

        public void Delete(List<User> itemsToDelete) {
            _repo.Delete(itemsToDelete);
        }

        public List<User> GetAll() {
            return _repo.GetAll();
        }

        public List<User> GetTop(Expression<Func<User, bool>> where, int numberToReturn) {
            return _repo.GetTop(where, numberToReturn);
        }

        public List<User> Where(Expression<Func<User, bool>> where) {
            return _repo.Where(where);
        }

        public User GetById(int id) {
            return _repo.Get(id);
        }

        public List<UserListItem> GetUserList() {
            var items = new List<UserListItem>();
            var results = _repo.GetAll();

            if (results.Count > 0) {
                foreach (var item in results) {
                    items.Add(new UserListItem(item));
                }
            }

            return items;
        }

        public void SetUserValues(int userId, bool? isFavorite = null) {
            var item = Get(userId);
            if (item == null) {
                return;
            }
            if (isFavorite.HasValue) {
                item.IsFavorite = isFavorite.Value;
            }
            _repo.Save(item);
        }
    }
}
