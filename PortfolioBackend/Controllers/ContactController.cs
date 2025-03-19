using Microsoft.AspNetCore.Mvc;
using PortfolioBackend.Models;
using PortfolioBackend.Services;
using System.Threading.Tasks;

namespace PortfolioBackend.Controllers
{
    [Route("api/contact")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly EmailService _emailService;

        public ContactController(EmailService emailService)
        {
            _emailService = emailService;
        }

        [HttpPost]
        public async Task<IActionResult> SendEmail([FromBody] ContactFormModel contactForm)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid form submission");

            var isSent = await _emailService.SendEmail(contactForm);

            if (isSent)
                return Ok(new { message = "Email sent successfully" });
            else
                return StatusCode(500, new { message = "Failed to send email" });
        }
    }
}

