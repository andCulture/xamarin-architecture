using Core.Domain.Entities;
using Core.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Core.Application.Interfaces.Services {
    public interface IUserService {
        void Save(User item);
        void SaveAll(List<User> items);
        void Delete(int id);
        void Delete(List<User> itemsToDelete);

        List<User> GetAll();
        User Get(int userId);
        User GetById(int id);
        List<User> GetTop(Expression<Func<User, bool>> where, int numberToReturn);
        List<User> Where(Expression<Func<User, bool>> where);

        List<UserListItem> GetUserList();

        void SetUserValues(int userId, bool? isFavorite = null);
    }
}
