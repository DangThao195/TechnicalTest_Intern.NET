using QuizApp.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizApp.Interface
{
    public interface IAnswerService
    {
        Task<Answer> GetAnswerById(Guid id);
        Task<IEnumerable<Answer>> GetAnswersByQuestionId(Guid questionId);
    }
}
