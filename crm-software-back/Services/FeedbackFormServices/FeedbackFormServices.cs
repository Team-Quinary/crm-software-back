using crm_software_back.Data;
using crm_software_back.DTOs;
using crm_software_back.Models;
using crm_software_back.Services.FeedbackFormServices;
using crm_software_back.Services.OptionServices;
using crm_software_back.Services.QuestionServices;
using Microsoft.CodeAnalysis.Options;
using Microsoft.EntityFrameworkCore;

namespace crm_software_back.Services.FormServices
{
    public class FeedbackFormServices : IFeedbackFormServices
    {
        private readonly DataContext _context;
        private readonly IQuestionServices _questionService;
        private readonly IOptionServices _optionServices;

        public FeedbackFormServices(DataContext context, IQuestionServices questionServices, IOptionServices optionServices)
        {
            _context = context;
            _questionService = questionServices;
            _optionServices = optionServices;
        }

        public async Task<FeedbackForm?> PostFeedbackForm(FeedbackForm newFeedbackForm)
        {
            var feedback = await _context.FeedbackForms.Where(feedback => feedback.Name.Equals(newFeedbackForm.Name)).FirstOrDefaultAsync();

            if (feedback != null)
            {
                return null;
            }

            _context.FeedbackForms.Add(newFeedbackForm);
            await _context.SaveChangesAsync();

            return await _context.FeedbackForms.Where(feedback => feedback.Name.Equals(newFeedbackForm.Name)).FirstOrDefaultAsync();
        }

        public async Task<List<FeedbackForm>?> GetFeedbackForms()
        {
            var feedbackForms = await _context.FeedbackForms
                .Include(f => f.Questions)
                    .ThenInclude(q => q.Options)
                .ToListAsync();

            return feedbackForms;
        }

        public async Task<FeedbackForm?> PutFeedbackForm(int feedbackFormId, DTOFeedbackForm newFeedbackForm)
        {
            var form = await _context.FeedbackForms.Where(f => f.FormId == feedbackFormId)
                .Include(f => f.Questions)
                    .ThenInclude(q => q.Options)
                .FirstOrDefaultAsync();

            if (form == null)
            {
                return null;
            }

            form.Name = (newFeedbackForm.Name == "") ? form.Name : newFeedbackForm.Name;
            form.Description = (newFeedbackForm.Description == "") ? form.Description : newFeedbackForm.Description;

            if (form.Questions.Count > 0 && newFeedbackForm.Questions.Count > 0)
            {
                foreach (var prevQuestion in form.Questions)
                {
                    if (!newFeedbackForm.Questions.Any(q => q.QuestionId == prevQuestion.QuestionId))
                    {
                        if (await _questionService.DeleteQuestion(prevQuestion.QuestionId) == null)
                        {
                            return null;
                        }
                        form.Questions.Remove(prevQuestion);
                        continue;
                    }

                    foreach (var newQuestion in newFeedbackForm.Questions)
                    {
                        if (prevQuestion.QuestionId == newQuestion.QuestionId)
                        {
                            DTOQuestion dtoQuestion = new DTOQuestion()
                            {
                                FormId = newQuestion.FormId,
                                Text = newQuestion.Text,
                                IsRequired = newQuestion.IsRequired,
                                Options = newQuestion.Options,
                                Type = newQuestion.Type
                            };

                            if (await _questionService.PutQuestion(newQuestion.QuestionId, dtoQuestion) == null)
                            {
                                return null;
                            }
                            newFeedbackForm.Questions.Remove(dtoQuestion);
                            continue;
                        }
                    }

                    foreach (var newQuestion in newFeedbackForm.Questions)
                    {
                        if (await _questionService.PostQuestion(newQuestion) == null)
                        {
                            return null;
                        }
                    }
                }
            }
            else if (form.Questions.Count > 0)
            {
                foreach (var prevQuestion in form.Questions)
                {
                    if (await _questionService.DeleteQuestion(prevQuestion.QuestionId) == null)
                    {
                        return null;
                    }
                }
            }
            else if (newFeedbackForm.Questions.Count > 0)
            {
                foreach (var newQuestion in newFeedbackForm.Questions)
                {

                    if (await _questionService.PostQuestion(newQuestion) == null)
                    {
                        return null;
                    }
                }
            }

            await _context.SaveChangesAsync();

            return form;
        }

        public async Task<FeedbackForm?> DeleteFeedbackForm(int feedbackFormId)
        {
            var form = await _context.FeedbackForms.Where(f => f.FormId == feedbackFormId)
                .Include(f => f.Questions).FirstOrDefaultAsync();

            if (form == null)
            {
                return null;
            }

            if (form.Questions.Count > 0)
            {
                foreach (var question in form.Questions)
                {
                    if (question.Options != null && question.Options.Count > 0)
                    {
                        foreach (var option in question.Options)
                        {
                            if (await _optionServices.DeleteOption(option.OptionId) == null)
                            {
                                return null;
                            }
                        }
                    }

                    if (await _questionService.DeleteQuestion(question.QuestionId) == null)
                    {
                        return null;
                    }
                }
            }

            _context.FeedbackForms.Remove(form);
            await _context.SaveChangesAsync();

            return form;
        }

        public async Task<FeedbackForm?> SaveChanges(DTOFeedbackForm newFeedbackForm)
        {
            if (newFeedbackForm == null)
            {
                return null;
            }

            FeedbackForm currentForm = await _context.FeedbackForms.Where(f => f.FormId == newFeedbackForm.FormId)
                .Include(f => f.Questions).ThenInclude(q => q.Options).FirstOrDefaultAsync();

            if (currentForm.Questions.Count > 0)
            {
                foreach (var question in currentForm.Questions)
                {
                    if (question.Options.Count > 0)
                    {
                        _context.Options.RemoveRange(_context.Options.Where(o => o.QuestionId == question.FormId));
                        await _context.SaveChangesAsync();
                    }
                }

                _context.Questions.RemoveRange(_context.Questions.Where(q => q.FormId == currentForm.FormId));
                await _context.SaveChangesAsync();
            }

            if (await DeleteFeedbackForm(newFeedbackForm.FormId) == null)
            {
                return null;
            }

            //if (newFeedbackForm.Questions.Count > 0)
            //{
            //    foreach (var question in newFeedbackForm.Questions)
            //    {
            //        if (question.Options.Count > 0)
            //        {
            //            foreach (var option in question.Options)
            //            {
            //                if (await _optionServices.DeleteOption(option.OptionId) == null)
            //                {
            //                    return null;
            //                }
            //            }
            //        }

            //        if (await _questionService.DeleteQuestion(question.QuestionId) == null)
            //        {
            //            return null;
            //        }
            //    }
            //}

            //if (await DeleteFeedbackForm(newFeedbackForm.FormId) == null)
            //{
            //    return null;
            //}

            FeedbackForm form = new FeedbackForm()
            {
                Name = newFeedbackForm.Name,
                Description = newFeedbackForm.Description,
                Questions = new List<Question>()
            };

            if (await PostFeedbackForm(form) == null)
            {
                return null;
            }

            FeedbackForm addedForm = await _context.FeedbackForms.Where(f =>
                f.Name == newFeedbackForm.Name
            ).FirstOrDefaultAsync();

            if (newFeedbackForm.Questions.Count > 0)
            {
                foreach (var question in newFeedbackForm.Questions)
                {
                    question.FormId = addedForm.FormId;

                    if (await _questionService.PostQuestion(question) == null)
                    {
                        return null;
                    }

                    Question addedQuestion = await _context.Questions.Where(q =>
                        q.FormId == question.FormId && q.Text == question.Text
                    ).FirstOrDefaultAsync();

                    if (question.Options.Count > 0)
                    {
                        foreach (var option in question.Options)
                        {
                            option.QuestionId = addedQuestion.QuestionId;

                            if (await _optionServices.PostOption(option) == null)
                            {
                                return null;
                            }
                        }
                    }
                }
            }

            return await _context.FeedbackForms.Where(f =>
                f.Name == newFeedbackForm.Name
            ).Include(f => f.Questions).ThenInclude(q => q.Options).FirstOrDefaultAsync();
        }
    }
}
