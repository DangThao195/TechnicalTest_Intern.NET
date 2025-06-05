using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using QuizApp.Entity;
using QuizApp.Interface;
using QuizApp.Service;
using QuizApp.View;
using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;

namespace QuizApp
{
    public partial class App : Application
    {
        private readonly IServiceProvider _serviceProvider;
        [DllImport("kernel32.dll")]
        private static extern bool AllocConsole();

        public App()
        {
            var services = new ServiceCollection();
            services.AddDbContext<QuizDbContext>(options =>
                options.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=QuizAppDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False"));
            services.AddScoped<IQuestionService, QuestionService>();
            services.AddScoped<IAnswerService, AnswerService>();
            services.AddScoped<IAnswerResultService, AnswerResultService>();
            services.AddScoped<IAnswerResult_QuestionService, AnswerResult_QuestionService>();
            services.AddTransient<MainView>();
            _serviceProvider = services.BuildServiceProvider();
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            AllocConsole();
            base.OnStartup(e);

            await SeedDataIfEmptyAsync();

            var mainView = _serviceProvider.GetRequiredService<MainView>();
            mainView.Show();
        }

        private async Task SeedDataIfEmptyAsync()
        {
            using var scope = _serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<QuizDbContext>();

            Console.WriteLine("Bắt đầu quá trình seeding...");
            await context.Database.EnsureCreatedAsync();
            Console.WriteLine("Đã tạo cơ sở dữ liệu (nếu chưa có).");

            bool hasQuestions = await context.Questions.AnyAsync();
            bool hasAnswers = await context.Answers.AnyAsync();
            Console.WriteLine($"Có câu hỏi: {hasQuestions}, Có đáp án: {hasAnswers}");

            string basePath = AppDomain.CurrentDomain.BaseDirectory;
            string questionsPath = Path.Combine(basePath, "Questions.json");
            string answersPath = Path.Combine(basePath, "Answers.json");
            Console.WriteLine($"Đường dẫn Questions.json: {questionsPath}");
            Console.WriteLine($"Đường dẫn Answers.json: {answersPath}");

            if (!hasQuestions && File.Exists(questionsPath))
            {
                try
                {
                    string questionsJson = await File.ReadAllTextAsync(questionsPath);
                    var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                    var questions = JsonSerializer.Deserialize<List<Question>>(questionsJson, options);
                    Console.WriteLine($"Đã deserialize {questions?.Count} câu hỏi.");
                    if (questions?.Any() == true)
                    {
                        foreach (var question in questions)
                        {
                            if (!await context.Questions.AnyAsync(q => q.QuestionId == question.QuestionId))
                            {
                                context.Questions.Add(question);
                                Console.WriteLine($"Đã thêm câu hỏi: {question.Text}");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Lỗi khi đọc Questions.json: {ex.Message}");
                }
            }
            else if (!File.Exists(questionsPath))
            {
                Console.WriteLine("Không tìm thấy Questions.json tại: " + questionsPath);
            }

            if (!hasAnswers && File.Exists(answersPath))
            {
                try
                {
                    string answersJson = await File.ReadAllTextAsync(answersPath);
                    var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                    var answers = JsonSerializer.Deserialize<List<Answer>>(answersJson, options);
                    Console.WriteLine($"Đã deserialize {answers?.Count} đáp án.");
                    if (answers?.Any() == true)
                    {
                        foreach (var answer in answers)
                        {
                            if (!await context.Answers.AnyAsync(a => a.AnswerId == answer.AnswerId))
                            {
                                context.Answers.Add(answer);
                                Console.WriteLine($"Đã thêm đáp án: {answer.Text} cho QuestionId {answer.QuestionId}");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Lỗi khi đọc Answers.json: {ex.Message}");
                }
            }
            else if (!File.Exists(answersPath))
            {
                Console.WriteLine("Không tìm thấy Answers.json tại: " + answersPath);
            }

            try
            {
                await context.SaveChangesAsync();
                Console.WriteLine("Seeding dữ liệu thành công.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi lưu vào cơ sở dữ liệu: {ex.Message}");
            }
        }
    }
}