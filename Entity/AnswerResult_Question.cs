using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizApp.Entity
{
    public class AnswerResult_Question
    {
        public string UserAnswer { get; set; } = string.Empty;
        public int TimeAnswered { get; set; } = 0;
        [ForeignKey("AnswerResult")]
        public Guid ResultId { get; set; }
        [ForeignKey("Question")]
        public Guid QuestionId { get; set; }
    }
}
