using QuizApp.Interface;
using QuizApp.Entity;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace QuizApp.Service
{
    public class AnswerService : IAnswerService
    {
        private readonly QuizDbContext _context;

        public AnswerService()
        {
        }

        public AnswerService(QuizDbContext context)
        {
            _context = context;
        }

        public async Task<Answer> GetAnswerById(Guid id)
        {
            return await _context.Answers
                .FirstOrDefaultAsync(a => a.AnswerId == id);
        }

        public async Task<IEnumerable<Answer>> GetAnswersByQuestionId(Guid questionId)
        {
            return await _context.Answers
                .Where(a => a.QuestionId == questionId)
                .ToListAsync();
        }
    }
}