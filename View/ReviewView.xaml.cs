using QuizApp.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace QuizApp.View
{
    public partial class ReviewView : Window
    {
        private readonly IAnswerResultService _answerResultService;
        private readonly IAnswerResult_QuestionService _answerResultQuestionService;
        private readonly IQuestionService _questionService;
        private readonly IAnswerService _answerService;
        private readonly Guid _answerResultId;

        public ReviewView(IAnswerResultService answerResultService, IAnswerResult_QuestionService answerResultQuestionService,
                         IQuestionService questionService, IAnswerService answerService, Guid answerResultId)
        {
            InitializeComponent();
            _answerResultService = answerResultService;
            _answerResultQuestionService = answerResultQuestionService;
            _questionService = questionService;
            _answerService = answerService;
            _answerResultId = answerResultId;
            LoadReview();
        }

        private async void LoadReview()
        {
            var answerResult = await _answerResultService.GetAnswerResultById(_answerResultId);
            var answerResultQuestions = (await _answerResultQuestionService.GetAnswerResult_QuestionsByResultId(_answerResultId)).ToList();
            var questions = (await _questionService.GetRandomQuestions(10)).ToList(); 

            for (int i = 0; i < questions.Count && i < answerResultQuestions.Count; i++)
            {
                var question = questions[i];
                var userAnswerRecord = answerResultQuestions[i];
                var answers = (await _answerService.GetAnswersByQuestionId(question.QuestionId)).ToList();
                var correctAnswer = answers.FirstOrDefault(a => a.AnswerId == question.CorrectAnswer);
                var userSelectedAnswer = answers.FirstOrDefault(a => a.Text == userAnswerRecord.UserAnswer);

                var container = new Border
                {
                    BorderBrush = Brushes.White,
                    BorderThickness = new Thickness(1),
                    Margin = new Thickness(0, 10, 0, 10),
                    Padding = new Thickness(10)
                };
                var panel = new StackPanel();

                panel.Children.Add(new TextBlock
                {
                    Text = $"Question {i + 1}: {question.Text}",
                    FontSize = 16,
                    Foreground = Brushes.Yellow,
                    FontWeight = FontWeights.Bold
                });
                panel.Children.Add(new TextBlock
                {
                    Text = $"Correct Answer: {correctAnswer?.Text ?? "N/A"}",
                    FontSize = 14,
                    Foreground = Brushes.Green,
                    Margin = new Thickness(0, 5, 0, 0)
                });
                panel.Children.Add(new TextBlock
                {
                    Text = $"Your Answer: {userSelectedAnswer?.Text ?? "N/A"}",
                    FontSize = 14,
                    Foreground = userSelectedAnswer?.AnswerId == question.CorrectAnswer ? Brushes.Green : Brushes.Red,
                    Margin = new Thickness(0, 5, 0, 0)
                });
                panel.Children.Add(new TextBlock
                {
                    Text = $"Time Taken: {userAnswerRecord.TimeAnswered} seconds",
                    FontSize = 14,
                    Foreground = Brushes.White,
                    Margin = new Thickness(0, 5, 0, 0)
                });

                container.Child = panel;
                ReviewPanel.Children.Add(container);
            }
        }

        private void PlayAgainButton_Click(object sender, RoutedEventArgs e)
        {
            MainView mainView = new MainView(_questionService, _answerService, _answerResultService, _answerResultQuestionService);
            mainView.Show();
            this.Close();
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}