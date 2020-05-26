using DataAccess.Contracts;
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace DataAccess.Services
{
    public class UserDbService : IUserDbService
    {
        public readonly ApplicationDbContext _applicationDbContext;

        public UserDbService(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task AddUser(User user)
        {
            await _applicationDbContext.AddAsync(user);
            await _applicationDbContext.SaveChangesAsync();
        }

        public async Task<User> GetUser(string userLogin)
        {
            return await _applicationDbContext.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Login == userLogin);
        }

        public async Task<User> GetUser(Guid userId)
        {
            return await _applicationDbContext.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Id == userId);
        }

        public async Task UpdateRefreshToken(Guid userId, Guid? refreshToken)
        {
            var user = new User() { Id = userId, RefreshToken = refreshToken };

            _applicationDbContext.Users.Attach(user);
            _applicationDbContext.Entry(user).Property(x => x.RefreshToken).IsModified = true;
            await _applicationDbContext.SaveChangesAsync();
        }
    }
}
