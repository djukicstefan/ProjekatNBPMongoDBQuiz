using ProjekatNBPMongoDBQuiz.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjekatNBPMongoDBQuiz.IServices
{
    interface IQuestionService
    {
        public Task<List<Question>> GetQuestionsAsync();
        public Task AddQuestionAsync(Question question);
        public Task<Question> GetQuestionByIdAsync(string id);        
        public Task UpdateQuestionAsync(Question question);
        public Task DeleteQuestionAsync(string id);
    }
}
