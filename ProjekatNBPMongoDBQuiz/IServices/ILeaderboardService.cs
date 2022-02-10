using ProjekatNBPMongoDBQuiz.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjekatNBPMongoDBQuiz.IServices
{
    public interface ILeaderboardService
    {
        public Task<List<Leaderboard>> GetLeaderboardsAsync();
        public Task AddLeaderboardAsync(Leaderboard leaderboard);
        public Task<Leaderboard> GetLeaderboardByIdAsync(string id);
        public Task UpdateLeaderboardAsync(Leaderboard leaderboard);
        public Task DeleteLeaderboardAsync(string id);
    }
}
