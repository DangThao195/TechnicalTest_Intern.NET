using QuizApp.Interface;
using QuizApp.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace QuizApp.View
{
    public partial class QuizView : Window
    {
        private readonly IQuestionService _questionService;
        private readonly IAnswerService _answerService;
        private readonly IAnswerResultService _answerResultService;
        private readonly IAnswerResult_QuestionService _answerResultQuestionService;
        private List<Question> _questions;
        private List<Answer> _currentAnswers;
        private int _currentQuestionIndex;
        private Guid _answerResultId;
        private Guid? _selectedAnswerId;
        private DateTime _questionStartTime;

        public QuizView(IQuestionService questionService, IAnswerService answerService,
                       IAnswerResultService answerResultService, IAnswerResult_QuestionService answerResultQuestionService)
        {
            InitializeComponent();
            _questionService = questionService;
            _answerService = answerService;
            _answerResultService = answerResultService;
            _answerResultQuestionService = answerResultQuestionService;
            _currentQuestionIndex = 0;
            _questions = new List<Question>();
            _currentAnswers = new List<Answer>();
            InitializeQuiz();
    
            this.KeyDown += QuizView_KeyDown;
        }

        private async void InitializeQuiz()
        {
            try
            {
                var answerResult = new AnswerResult
                {
                    ResultId = Guid.NewGuid(),
                    TotalTime = 0,
                    NumberCorrectAnswers = 0,
                    PassStatus = false
                };
                await _answerResultService.AddAnswerResult(answerResult);
                _answerResultId = answerResult.ResultId;

                _questions = (await _questionService.GetRandomQuestions(10)).ToList();
                if (_questions.Any())
                {
                    await LoadQuestion();
                }
                else
                {
                    MessageBox.Show("No questions available.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error initializing quiz: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Close();
            }
        }

        private async Task LoadQuestion()
        {
            if (_currentQuestionIndex >= _questions.Count)
            {
                ResultView resultView = new ResultView(_answerResultService, _answerResultQuestionService, _questionService, _answerService, _answerResultId);
                resultView.Show();
                Close(); 
                return;
            }

            var currentQuestion = _questions[_currentQuestionIndex];
            _currentAnswers = (await _answerService.GetAnswersByQuestionId(currentQuestion.QuestionId)).ToList();

            QuestionNumberTextBlock.Text = $"Question {_currentQuestionIndex + 1}";
            QuestionTextBlock.Text = currentQuestion.Text;

            Answer1RadioButton.IsChecked = false;
            Answer2RadioButton.IsChecked = false;
            Answer3RadioButton.IsChecked = false;
            Answer4RadioButton.IsChecked = false;
            FeedbackTextBlock.Visibility = Visibility.Hidden;
            NextButton.IsEnabled = false;
            _selectedAnswerId = null;

            var radioButtons = new[] { Answer1RadioButton, Answer2RadioButton, Answer3RadioButton, Answer4RadioButton };
            for (int i = 0; i < radioButtons.Length; i++)
            {
                radioButtons[i].Content = i < _currentAnswers.Count ? _currentAnswers[i].Text : string.Empty;
                radioButtons[i].Visibility = i < _currentAnswers.Count ? Visibility.Visible : Visibility.Hidden;
                radioButtons[i].Tag = i < _currentAnswers.Count ? _currentAnswers[i].AnswerId : Guid.Empty;
            }

            _questionStartTime = DateTime.Now;
        }

        private async void AnswerRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            var radioButton = sender as RadioButton;
            if (radioButton == null || radioButton.Tag == null) return;

            _selectedAnswerId = (Guid)radioButton.Tag;
            NextButton.IsEnabled = true;

            var currentQuestion = _questions[_currentQuestionIndex];
            var selectedAnswer = _currentAnswers.FirstOrDefault(a => a.AnswerId == _selectedAnswerId);
            bool isCorrect = selectedAnswer != null && _selectedAnswerId == currentQuestion.CorrectAnswer;

            FeedbackTextBlock.Text = isCorrect ? "Correct!" : "Incorrect!";
            FeedbackTextBlock.Foreground = isCorrect ? System.Windows.Media.Brushes.Green : System.Windows.Media.Brushes.Red;
            FeedbackTextBlock.Visibility = Visibility.Visible;

 
            var timeTaken = (int)(DateTime.Now - _questionStartTime).TotalSeconds;
            var answerResultQuestion = new AnswerResult_Question
            {
                ResultId = _answerResultId,
                QuestionId = currentQuestion.QuestionId,
                UserAnswer = selectedAnswer?.Text ?? string.Empty,
                TimeAnswered = timeTaken
            };
            await _answerResultQuestionService.AddAnswerResult_Question(answerResultQuestion);

            var answerResult = await _answerResultService.GetAnswerResultById(_answerResultId);
            answerResult.TotalTime += timeTaken;
            if (isCorrect)
            {
                answerResult.NumberCorrectAnswers++;
            }
            answerResult.PassStatus = answerResult.NumberCorrectAnswers >= (_questions.Count * 0.5); // 70% pass threshold
            await _answerResultService.UpdateAnswerResult(answerResult); // Sử dụng Update thay vì Add
        }

        private async void NextButton_Click(object sender, RoutedEventArgs e)
        {
            if (!_selectedAnswerId.HasValue)
            {
                MessageBox.Show("Please select an answer before proceeding.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            _currentQuestionIndex++;
            await LoadQuestion();
        }

        private void QuizView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter && _selectedAnswerId.HasValue)
            {
                NextButton_Click(sender, e);
            }
        }
    }
}