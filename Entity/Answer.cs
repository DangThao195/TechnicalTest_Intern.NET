using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuizApp.Entity
{
    public class Answer
    {
        [Key]
        public Guid AnswerId { get; set; }
        public string Text { get; set; } = string.Empty;
        [ForeignKey("Question")]
        public Guid QuestionId { get; set; }
    }
}