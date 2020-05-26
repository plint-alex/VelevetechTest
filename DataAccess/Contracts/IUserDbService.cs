using DataAccess.Entities;
using System;
using System.Threading.Tasks;

namespace DataAccess.Contracts
{
    public interface IUserDbService
    {
        Task AddUser(Entities.User user);

        Task<User> GetUser(string userLogin);

        Task<User> GetUser(Guid userId);

        Task UpdateRefreshToken(Guid userId, Guid? refreshToken);
    }
}
