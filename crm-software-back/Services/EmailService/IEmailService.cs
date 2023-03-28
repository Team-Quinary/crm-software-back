using crm_software_back.Models;

namespace crm_software_back.Services.EmailService
{
    public interface IEmailService
    {
        void SendEmail(EmailModel emailModel);
    }
}


