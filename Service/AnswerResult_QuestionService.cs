using QuizApp.Entity;
using QuizApp.Interface;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizApp.Service
{
    public class AnswerResult_QuestionService : IAnswerResult_QuestionService
    {
        private readonly QuizDbContext _context;

        public AnswerResult_QuestionService(QuizDbContext context)
        {
            _context = context;
        }

        public async Task AddAnswerResult_Question(AnswerResult_Question answerResult_Question)
        {
            _context.AnswerResult_Questions.Add(answerResult_Question);
            await _context.SaveChangesAsync();
        }
        public async Task<IEnumerable<AnswerResult_Question>> GetAnswerResult_QuestionsByResultId(Guid resultId)
        {
            return await _context.AnswerResult_Questions
                .Where(arq => arq.ResultId == resultId)
                .ToListAsync();
        }
    }
}
