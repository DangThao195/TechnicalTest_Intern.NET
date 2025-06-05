using QuizApp.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizApp.Interface
{
    public interface IQuestionService
    {
        Task<Question> GetQuestionById(Guid id);
        Task<IEnumerable<Question>> GetRandomQuestions(int count = 10);
    }
}
