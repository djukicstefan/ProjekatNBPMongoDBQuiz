using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjekatNBPMongoDBQuiz.Models
{
    public class LeaderboardItem
    {
        public User User { get; set; }
        public int Score { get; set; }
        public long Time { get; set; }
    }
}
