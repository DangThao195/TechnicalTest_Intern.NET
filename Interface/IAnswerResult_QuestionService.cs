using QuizApp.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizApp.Interface
{
    public interface IAnswerResult_QuestionService
    {
        Task AddAnswerResult_Question(AnswerResult_Question answerResult_Question);
        Task<IEnumerable<AnswerResult_Question>> GetAnswerResult_QuestionsByResultId(Guid resultId);
    }
}
