using DatingApp.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatingApp.API.Data
{
    public interface IDatingRepository
    {
        //Generic classes for adding a user
        void Add<T>(T entity) where T : class;

        void Delete<T>(T entity) where T : class;

        //Will save or not changes
        Task<bool> SaveAll();

        Task<IEnumerable<User>> getUsers();

        Task<User> getUser(int id);
    }
}
