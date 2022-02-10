using MongoDB.Driver;
using ProjekatNBPMongoDBQuiz.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjekatNBPMongoDBQuiz
{
    public class MongoDBContext
    {
        private readonly IMongoDatabase _mongoDB;

        public MongoDBContext()
        {
            var client = new MongoClient("mongodb://localhost:27017");
            _mongoDB = client.GetDatabase("Baza");
        }
        
        public IMongoCollection<User> UserCollection
            => _mongoDB.GetCollection<User>("User");
        
        public IMongoCollection<Quiz> QuizCollection 
            => _mongoDB.GetCollection<Quiz>("Quiz");

        public IMongoCollection<Leaderboard> LeaderboardCollection
            => _mongoDB.GetCollection<Leaderboard>("Leaderboard");
    }
}
