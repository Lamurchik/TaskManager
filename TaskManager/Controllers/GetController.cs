using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using System.Net;

namespace TaskManager.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GetController : Controller
    {
        [HttpGet( Name = "SendEmailAsync")]
        public async Task<IActionResult> SendEmailAsync(string toEmail, string subject, string body)
        {
            using (MailMessage mail = new MailMessage())
            {
                mail.From = new MailAddress("quterdraw1234@mail.ru");
                mail.To.Add(toEmail);
                mail.Subject = subject;
                mail.Body = body;
                mail.IsBodyHtml = true;

                using (SmtpClient smtpClient = new SmtpClient("smtp.example.com"))
                {
                    smtpClient.Port = 7200;
                    smtpClient.Credentials = new NetworkCredential("quterdraw1234@mail.ru", "To1oHta_iYP1");
                    smtpClient.EnableSsl = true;

                    await smtpClient.SendMailAsync(mail);
                }
            }
            return NoContent();
        }
        [HttpPost]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("No file uploaded.");
            }

            var uploadsFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "uploads");

            if (!Directory.Exists(uploadsFolderPath))
            {
                Directory.CreateDirectory(uploadsFolderPath);
            }

            var filePath = Path.Combine(uploadsFolderPath, file.FileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            return Ok(new { filePath });
        }



    }

}
