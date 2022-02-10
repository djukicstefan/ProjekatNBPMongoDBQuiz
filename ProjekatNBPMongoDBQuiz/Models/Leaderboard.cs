using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;
namespace ProjekatNBPMongoDBQuiz.Models
{
    public class Leaderboard
    {
        public string Id { get; set; }
        public List<LeaderboardItem> Content { get; set; }
    }
}
