using DatingApp.API.Helpers;
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

        // Changing next line to use pagedlist to accept values on the api query
        // so pagination and else can be used on the api
        //Task<IEnumerable<User>> getUsers();
        Task<PagedList<User>> GetUsers(UserParams userParams);

        Task<User> GetUser(int id);

        Task<Photo> GetPhoto(int id);

        Task<Photo> GetmainPhotoForUser(int userId);

        Task<Like> GetLike(int userId, int recipientId);

        Task<Message> GetMessage(int id);

        Task<PagedList<Message>> GetMessagesForUser(MessageParams userParams);

        Task<IEnumerable<Message>> GetMessageThread(int userId, int recipientId);
    }
}
