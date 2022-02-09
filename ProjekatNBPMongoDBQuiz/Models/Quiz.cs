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
        public string Type { get; set; }
        public string AuthorId { get; set; }
        public string Description { get; set; }
        public List<Question> Questions { get; set; }
    }
}
