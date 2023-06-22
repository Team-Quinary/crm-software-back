using crm_software_back.Models;
using crm_software_back.Data;
using Microsoft.EntityFrameworkCore;
using crm_software_back.DTOs;

namespace crm_software_back.Services.OptionServices
{
    public class OptionServices : IOptionServices
    {

        private readonly DataContext _context;

        public OptionServices(DataContext context)
        {
            _context = context;
        }

        public async Task<Option?> PostOption(DTOOption newOption)
        {
            var option = await _context.Options.Where(option => 
                option.QuestionId == newOption.QuestionId && option.Text == newOption.Text
            ).FirstOrDefaultAsync();

            if (option != null)
            {
                return null;
            }

            Option converted = new Option()
            {
                QuestionId = newOption.QuestionId,
                Text = newOption.Text
            };

            _context.Options.Add(converted);
            await _context.SaveChangesAsync();

            return await _context.Options.Where(option =>
                option.QuestionId == newOption.QuestionId && option.Text == newOption.Text
            ).FirstOrDefaultAsync();

        }

        public async Task<List<Option>?> GetOptions(int questionId)
        {
            var options = await _context.Options.Where(o => o.QuestionId == questionId).ToListAsync();

            return options;
        }

        public async Task<Option?> PutOption(int optionId, DTOOption newOption)
        {
            var option = await _context.Options.FindAsync(optionId);

            if (option == null)
            {
                return null;
            }

            option.Text = (newOption.Text == "") ? option.Text : newOption.Text;
            await _context.SaveChangesAsync();

            return option;
        }

        public async Task<Option?> DeleteOption(int optionId)
        {
            var option = await _context.Options.FindAsync(optionId);

            if (option == null)
            {
                return null;
            }

            _context.Options.Remove(option);
            await _context.SaveChangesAsync();

            return option;
        }
    }
}



