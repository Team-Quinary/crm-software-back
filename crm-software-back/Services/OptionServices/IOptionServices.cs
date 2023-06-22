using crm_software_back.DTOs;
using crm_software_back.Models;

namespace crm_software_back.Services.OptionServices
{
    public interface IOptionServices
    {
        public Task<List<Option>?> GetOptions(int questionId);
        public Task<Option?> PostOption(DTOOption newOption);
        public Task<Option?> PutOption(int optionId, DTOOption newOption);
        public Task<Option?> DeleteOption(int optionId);
    }
}
