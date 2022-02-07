using MongoDB.Driver;
using ProjekatNBPMongoDBQuiz.IServices;
using ProjekatNBPMongoDBQuiz.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjekatNBPMongoDBQuiz.Services
{
    public class UserService : IUserService
    {
        MongoDBContext _dbContext = new MongoDBContext();
        public async Task<List<User>> GetUsersAsync()
            => await _dbContext.UserCollection.Find(_ => true).ToListAsync();

        public async Task AddUserAsync(User user)
            => await _dbContext.UserCollection.InsertOneAsync(user);

        public async Task<User> GetUserByIdAsync(string id)
            => await _dbContext.UserCollection.FindSync(p => p.Id == id).FirstOrDefaultAsync();

        public async Task<User> GetUserAsync(string username, string password)
            => await _dbContext.UserCollection.FindSync(p => p.Username == username && p.Password == password).FirstOrDefaultAsync();

        public async Task UpdateUserAsync(User user)
            => await _dbContext.UserCollection.ReplaceOneAsync(g => g.Id == user.Id, user);

        public async Task DeleteUserAsync(string id)
            => await _dbContext.UserCollection.DeleteOneAsync(p => p.Id == id);
    }
}
