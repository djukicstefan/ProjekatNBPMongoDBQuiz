using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ProjekatNBPMongoDBQuiz.Models
{    
    public class Question
    {                
        public string Text { get; set; }              
        public List<Answer> Answers { get; set; }

        public bool Validate(Question question) 
            => Answers.Select(x => x.Correct).SequenceEqual(question.Answers.Select(x => x.Correct));
    }
}
