using QuizApp.Interface;
using QuizApp.Entity;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizApp.Service
{
    public class QuestionService : IQuestionService
    {
        private readonly QuizDbContext _context;

        public QuestionService()
        {
        }

        public QuestionService(QuizDbContext context)
        {
            _context = context;
        }

        public async Task<Question> GetQuestionById(Guid id)
        {
            return await _context.Questions
                .FirstOrDefaultAsync(q => q.QuestionId == id);
        }
        public async Task<IEnumerable<Question>> GetRandomQuestions(int count = 10)
        {
            return await _context.Questions
                .OrderBy(q => Guid.NewGuid()) // Random order
                .Take(count) // Take the specified number of questions
                .ToListAsync();
        }
    }
}
