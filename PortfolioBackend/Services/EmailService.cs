using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using PortfolioBackend.Models;

namespace PortfolioBackend.Services
{
    public class EmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<bool> SendEmail(ContactFormModel contactForm)
        {
            try
            {
                var smtpClient = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    Credentials = new NetworkCredential(_configuration["EmailSettings:SenderEmail"], _configuration["EmailSettings:SenderPassword"]),
                    EnableSsl = true
                };

                var mailMessage = new MailMessage
                {
                    From = new MailAddress(_configuration["EmailSettings:SenderEmail"] ?? "default@email.com"),
                    Subject = contactForm.Subject ?? "No Subject",
                    Body = $"<h3>New Contact Form Submission</h3>" +
                           $"<p><b>Name:</b> {(contactForm.FullName ?? "Unknown")}</p>" +
                           $"<p><b>Email:</b> {(contactForm.Email ?? "No Email Provided")}</p>" +
                           $"<p><b>Phone:</b> {(contactForm.PhoneNumber ?? "No Phone Provided")}</p>" +
                           $"<p><b>Message:</b><br>{(contactForm.Message ?? "No Message")}</p>",
                    IsBodyHtml = true
                };

                mailMessage.To.Add("tivra010101@gmail.com");

                await smtpClient.SendMailAsync(mailMessage);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}

