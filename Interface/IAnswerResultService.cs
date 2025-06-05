using QuizApp.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizApp.Interface
{
    public interface IAnswerResultService
    {
        Task AddAnswerResult(AnswerResult answerResult);
        Task<AnswerResult> GetAnswerResultById(Guid id);
        Task UpdateAnswerResult(AnswerResult answerResult);

    }
}
