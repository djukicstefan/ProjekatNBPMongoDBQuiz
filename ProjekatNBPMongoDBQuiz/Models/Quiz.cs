using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;
using MongoDB.Bson;
using System.Linq;

namespace ProjekatNBPMongoDBQuiz.Models
{
    public class Quiz
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Title { get; set; }
        public string Category { get; set; }
        public string UserId { get; set; }    
        public List<Question> Questions { get; set; }

        public int Validate(Quiz quiz) 
            => Enumerable.Range(0, Questions.Count).Count(x => Questions[x].Validate(quiz.Questions[x]));
    }
}
