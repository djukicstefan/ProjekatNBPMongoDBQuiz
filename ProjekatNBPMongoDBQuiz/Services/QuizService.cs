using MongoDB.Driver;
using ProjekatNBPMongoDBQuiz.IServices;
using ProjekatNBPMongoDBQuiz.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjekatNBPMongoDBQuiz.Services
{
    public class QuizService : IQuizService
    {
        MongoDBContext _dbContext = new MongoDBContext();

        public async Task<List<Quiz>> GetQuizzesAsync()
            => await _dbContext.QuizCollection.Find(_ => true).ToListAsync();

        public async Task<List<Quiz>> GetUserQuizzes(string userId)
            => await _dbContext.QuizCollection.FindSync(q => q.UserId == userId).ToListAsync();

        public async Task AddQuizAsync(Quiz quiz) 
            => await _dbContext.QuizCollection.InsertOneAsync(quiz);
        
        public async Task<Quiz> GetQuizByIdAsync(string id)
            => await _dbContext.QuizCollection.FindSync(p => p.Id == id).FirstOrDefaultAsync();

        public async Task UpdateQuizAsync(Quiz quiz)
            => await _dbContext.QuizCollection.ReplaceOneAsync(g => g.Id == quiz.Id, quiz);

        public async Task DeleteQuizAsync(string id) 
            => await _dbContext.QuizCollection.DeleteOneAsync(p => p.Id == id);
        
    }
}
