using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizApp.Entity
{
    public class Question
    {
        [Key]
        public Guid QuestionId { get; set; }
        public string Text { get; set; } = string.Empty;
        public Guid CorrectAnswer { get; set; }
        public ICollection<Answer> Answers { get; set; } = new List<Answer>();

    }
}
