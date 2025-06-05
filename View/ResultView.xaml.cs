using QuizApp.Interface;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace QuizApp.View
{
    public partial class ResultView : Window
    {
        private readonly IAnswerResultService _answerResultService;
        private readonly IAnswerResult_QuestionService _answerResultQuestionService;
        private readonly IQuestionService _questionService;
        private readonly IAnswerService _answerService;
        private readonly Guid _answerResultId;

        public ResultView(IAnswerResultService answerResultService, IAnswerResult_QuestionService answerResultQuestionService,
                         IQuestionService questionService, IAnswerService answerService, Guid answerResultId)
        {
            InitializeComponent();
            _answerResultService = answerResultService;
            _answerResultQuestionService = answerResultQuestionService;
            _questionService = questionService;
            _answerService = answerService;
            _answerResultId = answerResultId;
            LoadResult();
        }

        private async void LoadResult()
        {
            try
            {
                var answerResult = await _answerResultService.GetAnswerResultById(_answerResultId);
                bool isPass = answerResult.PassStatus;

                ResultMessageTextBlock.Text = isPass ? "Congratulations!!\nYou are amazing!!" : "Completed!\nBetter luck next time!";

                try
                {
                    string imagePath = isPass ? "pack://application:,,,/Resources/pass.jpg" : "pack://application:,,,/Resources/fail.jpg";
                    ResultIcon.Source = new BitmapImage(new Uri(imagePath)); 
                }
                catch (Exception imageEx)
                {
                    ResultIcon.Source = null;
                    MessageBox.Show($"Failed to load image: {imageEx.Message}\nPlease check if the image exists in Resources folder with Build Action set to Resource.",
                        "Image Load Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                }

                CorrectAnswersTextBlock.Text = $"{answerResult.NumberCorrectAnswers}/10 correct answers";
                TotalTimeTextBlock.Text = $"in {answerResult.TotalTime} seconds.";

             
                ResultMessageTextBlock.TextAlignment = TextAlignment.Center;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading result: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void PlayAgainButton_Click(object sender, RoutedEventArgs e)
        {
            MainView mainView = new MainView(_questionService, _answerService, _answerResultService, _answerResultQuestionService);
            mainView.Show();
            this.Close();
        }

        private void ReviewButton_Click(object sender, RoutedEventArgs e)
        {
            ReviewView reviewView = new ReviewView(_answerResultService, _answerResultQuestionService, _questionService, _answerService, _answerResultId);
            reviewView.Show();
            this.Close();
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}