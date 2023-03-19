using EmailService;
using Microsoft.AspNetCore.Http;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailService
{
    public class ToPair
    {
        public string Name { get; set; } = string.Empty;

        public string Address { get; set; } = string.Empty;
    }

    public class Message
    {
        public List<MailboxAddress> To { get; set; }

        public string Subject { get; set; }

        public string Content { get; set; }

        public List<IFormFile> Attachments { get; set; }

        public Message(List<ToPair> to, string subject, string content, List<IFormFile> attachments)
        {
            To = new List<MailboxAddress>();
            To.AddRange(to.Select(x => new MailboxAddress(x.Name, x.Address)));

            Subject = subject;
            Content = content;
            Attachments = attachments;
        }
    }
}

public class DTOMessage
{
    public List<string> To { get; set; } = new List<string>();

    public string Subject { get; set; } = string.Empty;

    public string Content { get; set; } = string.Empty;

    public List<IFormFile> Attachments { get; set; } = new List<IFormFile>{ };
}
