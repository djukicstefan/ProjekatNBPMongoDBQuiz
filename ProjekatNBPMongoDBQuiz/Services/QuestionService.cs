using MongoDB.Driver;
using ProjekatNBPMongoDBQuiz.IServices;
using ProjekatNBPMongoDBQuiz.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjekatNBPMongoDBQuiz.Services
{
    public class QuestionService : IQuestionService
    {
        MongoDBContext _dbContext = new MongoDBContext();
        public async Task<List<Question>> GetQuestionsAsync()
            => await _dbContext.QuestionCollection.Find(_ => true).ToListAsync();

        public async Task AddQuestionAsync(Question question)
            => await _dbContext.QuestionCollection.InsertOneAsync(question);

        public async Task<Question> GetQuestionByIdAsync(string id)
            => await _dbContext.QuestionCollection.FindSync(p => p.Id == id).FirstOrDefaultAsync();

        public async Task UpdateQuestionAsync(Question question)
            => await _dbContext.QuestionCollection.ReplaceOneAsync(g => g.Id == question.Id, question);

        public async Task DeleteQuestionAsync(string id)
            => await _dbContext.QuestionCollection.DeleteOneAsync(p => p.Id == id);
    }
}
