using MongoDB.Driver;
using ProjekatNBPMongoDBQuiz.IServices;
using ProjekatNBPMongoDBQuiz.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjekatNBPMongoDBQuiz.Services
{
    public class LeaderboardService : ILeaderboardService
    {
        MongoDBContext _dbContext = new MongoDBContext();

        public async Task AddLeaderboardAsync(Leaderboard leaderboard)
            => await _dbContext.LeaderboardCollection.InsertOneAsync(leaderboard);

        public async Task DeleteLeaderboardAsync(string id)
            => await _dbContext.LeaderboardCollection.DeleteOneAsync(p => p.Id == id);

        public async Task<Leaderboard> GetLeaderboardByIdAsync(string id)
            => await _dbContext.LeaderboardCollection.FindSync(p => p.Id == id).FirstOrDefaultAsync();

        public async Task<List<Leaderboard>> GetLeaderboardsAsync()
            => await _dbContext.LeaderboardCollection.Find(_ => true).ToListAsync();

        public async Task UpdateLeaderboardAsync(Leaderboard leaderboard)
            => await _dbContext.LeaderboardCollection.ReplaceOneAsync(g => g.Id == leaderboard.Id, leaderboard);
    }
}
