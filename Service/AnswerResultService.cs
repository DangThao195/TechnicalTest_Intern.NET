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
    public class AnswerResultService : IAnswerResultService
    {
        private readonly QuizDbContext _context;

        public AnswerResultService(QuizDbContext context)
        {
            _context = context;
        }

        public async Task AddAnswerResult(AnswerResult answerResult)
        {
            _context.AnswerResults.Add(answerResult);
            await _context.SaveChangesAsync();
        }
        public async Task<AnswerResult> GetAnswerResultById(Guid id)
        {
            return await _context.AnswerResults
                .FirstOrDefaultAsync(ar => ar.ResultId == id);
        }

        public async Task UpdateAnswerResult(AnswerResult answerResult)
        {
            _context.AnswerResults.Update(answerResult); // Cập nhật thay vì thêm mới
            await _context.SaveChangesAsync();
        }
    }
}
