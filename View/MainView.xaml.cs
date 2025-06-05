using QuizApp.Interface;
using System.Windows;

namespace QuizApp.View
{
    public partial class MainView : Window
    {
        private readonly IQuestionService _questionService;
        private readonly IAnswerService _answerService;
        private readonly IAnswerResultService _answerResultService;
        private readonly IAnswerResult_QuestionService _answerResultQuestionService;

        public MainView(IQuestionService questionService, IAnswerService answerService,
                        IAnswerResultService answerResultService, IAnswerResult_QuestionService answerResultQuestionService)
        {
            InitializeComponent();
            _questionService = questionService;
            _answerService = answerService;
            _answerResultService = answerResultService;
            _answerResultQuestionService = answerResultQuestionService;
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            QuizView quizView = new QuizView(_questionService, _answerService, _answerResultService, _answerResultQuestionService);
            quizView.Show();
            this.Close();
        }
    }
}