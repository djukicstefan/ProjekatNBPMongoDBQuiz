using ProjekatNBPMongoDBQuiz.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjekatNBPMongoDBQuiz.IServices
{
    public interface IUserService
    {
        public Task<List<User>> GetUsersAsync();
        public Task AddUserAsync(User user);
        public Task<User> GetUserByIdAsync(string id);
        public Task<User> GetUserAsync(string username, string password);
        public Task UpdateUserAsync(User user);
        public Task DeleteUserAsync(string id);
    }
}
