using ProjekatNBPMongoDBQuiz.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjekatNBPMongoDBQuiz.IServices
{
    public interface IQuizService
    {
        public Task<List<Quiz>> GetQuizzesAsync();
        public Task<List<Quiz>> GetUserQuizzes(string userId);
        public Task AddQuizAsync(Quiz quiz);
        public Task<Quiz> GetQuizByIdAsync(string id);
        public Task UpdateQuizAsync(Quiz quiz);
        public Task DeleteQuizAsync(string id);
    }
}
