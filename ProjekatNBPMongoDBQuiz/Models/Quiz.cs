using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;
using MongoDB.Bson;

namespace ProjekatNBPMongoDBQuiz.Models
{
    public class Quiz
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Title { get; set; }
        public string Category { get; set; }
        public User User { get; set; }
        public List<Question> Questions { get; set; }
        
    }
}
