using FSWDFinalProject.UI.MVC.Models;
using System;
using System.Net;
using System.Net.Mail;
using System.Web.Mvc;

namespace FSWDFinalProject.UI.MVC.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        [HttpGet]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpPost]
        public ActionResult Contact(ContactViewModel cvm)
        {
            if (!ModelState.IsValid)
            {
                return View(cvm);
            }

            string body = $"Message from {cvm.Name} about {cvm.Subject}. There Email: {cvm.Email} <br> Message: <br> {cvm.Message}";

            MailMessage msg = new MailMessage("admin@blainegomez.com", "blaineg3291@gmail.com", "Email from JobBoard.com", body);
            msg.IsBodyHtml = true;

            SmtpClient client = new SmtpClient("mail.blainegomez.com");
            client.Credentials = new NetworkCredential("admin@blainegomez.com", "Gomez644!");
            client.Port = 8889;

            try
            {
                client.Send(msg);
            }
            catch (Exception ex)
            {
                ViewBag.SendEmailError = $"Uh oh, something went wrong. Details: {ex.Message}";
                return View(cvm);
            }


            return View("EmailConfirmation", cvm);
        }
    }
}
