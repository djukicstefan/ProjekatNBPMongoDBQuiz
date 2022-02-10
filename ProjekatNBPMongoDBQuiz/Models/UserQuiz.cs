using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjekatNBPMongoDBQuiz.Models
{
    public class UserQuiz
    {
        public Quiz Quiz { get; set; }
        public string Username { get; set; }
    }
}
