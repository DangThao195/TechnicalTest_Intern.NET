using QuizApp.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizApp.Entity
{
    public class AnswerResult
    {
        [Key]
        public Guid ResultId { get; set; }
        public int TotalTime { get; set; }
        public int NumberCorrectAnswers { get; set; }
        public bool PassStatus { get; set; } = false;
        public ICollection<AnswerResult_Question> Result_Questions { get; set; } = new List<AnswerResult_Question>();
    }
}
