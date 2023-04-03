using crm_software_back.Resources;

namespace crm_software_back.Models
{
    public static class EmailBody
    {
        public static string NewUserEmail(string username, string password, string firstName)
        {
            var email = Emails.Welcome;

            var formattedEmail = string.Format(email, firstName, username, password);

            return formattedEmail;
        }
    }
}

