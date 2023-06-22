using crm_software_back.Models;
using crm_software_back.Data;
using Microsoft.EntityFrameworkCore;
using crm_software_back.Migrations;
using crm_software_back.Services.OptionServices;
using crm_software_back.DTOs;

namespace crm_software_back.Services.QuestionServices
{
    public class QuestionsServices : IQuestionServices
    {
        private readonly DataContext _context;
        private readonly IOptionServices _optionServices;

        public QuestionsServices(DataContext context, IOptionServices optionServices)
        {
            _context = context;
            _optionServices = optionServices;
        }

        public async Task<Question?> PostQuestion(DTOQuestion newQuestion)
        {
            var question = await _context.Questions.Where(question =>
                question.FormId == newQuestion.FormId && question.Text == newQuestion.Text
            ).FirstOrDefaultAsync();

            if (question != null)
            {
                return null;
            }

            Question addingQuestion = new Question()
            {
                FormId = newQuestion.FormId,
                Text = newQuestion.Text,
                IsRequired = newQuestion.IsRequired,
                Type = newQuestion.Type
            };

            _context.Questions.Add(addingQuestion);
            await _context.SaveChangesAsync();

            var addedQuestion = await _context.Questions.Where(question =>
                question.FormId == newQuestion.FormId && question.Text == newQuestion.Text
            ).Include(q => q.Options).FirstOrDefaultAsync();

            foreach (var option in newQuestion.Options)
            {
                option.QuestionId = addedQuestion.QuestionId;
                Option converted = new Option()
                {
                    QuestionId = addedQuestion.QuestionId,
                    Text = option.Text
                };
                addingQuestion.Options.Add(converted);
            }

            if (newQuestion.Options.Count > 0)
            {
                foreach (var option in newQuestion.Options)
                {
                    if (await _optionServices.PostOption(option) == null)
                    {
                        return null;
                    }
                }
            }

            return addedQuestion;
        }

        public async Task<List<Question>?> GetQuestions(int formId)
        {
            var question = await _context.Questions.Where(q => q.FormId == formId)
                .Include(q => q.Options).ToListAsync();

            return question;
        }

        public async Task<Question?> PutQuestion(int questionId, DTOQuestion newQuestion)
        {
            var question = await _context.Questions.Where(q => q.QuestionId == questionId)
                .Include(q => q.Options).FirstOrDefaultAsync();

            if (question == null)
            {
                return null;
            }

            question.Text = (newQuestion.Text == "") ? question.Text : newQuestion.Text;
            question.Type = (newQuestion.Type == "") ? question.Type : newQuestion.Type;
            question.IsRequired = newQuestion.IsRequired;

            if (question.Options.Count > 0 && newQuestion.Options.Count > 0)
            {
                foreach (var prevOption in question.Options)
                {
                    if (!newQuestion.Options.Any(o => o.OptionId == prevOption.OptionId))
                    {
                        if (await _optionServices.DeleteOption(prevOption.OptionId) == null)
                        {
                            return null;
                        }
                        question.Options.Remove(prevOption);
                        continue;
                    }

                    foreach (var newOption in newQuestion.Options)
                    {
                        if (prevOption.OptionId == newOption.OptionId)
                        {
                            if (await _optionServices.PutOption(newOption.OptionId, newOption) == null)
                            {
                                return null;
                            }
                            newQuestion.Options.Remove(newOption);
                            continue;
                        }
                    }

                    foreach (var newOption in newQuestion.Options)
                    {
                        if (await _optionServices.PostOption(newOption) == null)
                        {
                            return null;
                        }
                    }
                }
            }
            else if (question.Options.Count > 0)
            {
                foreach (var prevOption in question.Options)
                {
                    if (await _optionServices.DeleteOption(prevOption.OptionId) == null)
                    {
                        return null;
                    }
                }
            }
            else if (newQuestion.Options.Count > 0)
            {
                foreach (var newOption in newQuestion.Options)
                {
                    if (await _optionServices.PostOption(newOption) == null)
                    {
                        return null;
                    }
                }
            }

            await _context.SaveChangesAsync();

            return question;
        }

        public async Task<Question?> DeleteQuestion(int questionId)
        {
            var question = await _context.Questions.Where(q => q.QuestionId == questionId)
                .Include(q => q.Options).FirstOrDefaultAsync();

            if (question == null)
            {
                return null;
            }

            _context.Questions.Remove(question);
            await _context.SaveChangesAsync();

            return question;
        }
    }
}
