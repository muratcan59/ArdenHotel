using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Mail;

namespace ArdenHotel.Controllers
{
    public class ContactController : Controller
    {
        [Route("contact")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Send(string name, string email, string subject, string message)
        {
            try
            {
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress("muratcan.dongel@exedra.com.tr");
                mail.To.Add(email);
                mail.Subject = subject;
                mail.IsBodyHtml = true;
                mail.Body = name + " " + message;

                SmtpClient sc = new SmtpClient();
                sc.Host = "smtp.office365.com";
                sc.Port = 587;
                sc.EnableSsl = true;
                sc.DeliveryMethod = SmtpDeliveryMethod.Network;
                sc.UseDefaultCredentials = false;
                sc.Credentials = new NetworkCredential("muratcan.dongel@exedra.com.tr", "mcTR5957..");
                sc.Send(mail);
                return Redirect("~/contact");
            }
            catch (System.Exception ex)
            {

                throw new System.Exception(ex.Message);
            }
        }
    }
}
