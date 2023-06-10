using Hangfire;
using HangfireExampleApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace HangfireExampleApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult OrderDetails()
        {
            return View();
        }

        public IActionResult ConfirmOrder()
        {
            // Fire-and-Forget Job
            var jobId = BackgroundJob.Enqueue(() => CreateInvoice());
            // Continuation Job
            BackgroundJob.ContinueJobWith(jobId, () => SendOrderInvoiceMail());
            ViewBag.JobId = jobId;
            return View();
        }
        public IActionResult Subscribe()
        {
            return View();
        }
        public IActionResult SubscribeOperation()
        {
            var recurrencePattern = Cron.DayInterval(7);
            // Recurring Job
            RecurringJob.AddOrUpdate(() => SendPromotionMail(), recurrencePattern);
            return View();
        }

        public IActionResult SendScheduledMessage()
        {
            return View();
        }
        [HttpPost]
        public IActionResult SendScheduledMessage(ScheduledMessageModel model)
        {

            TimeSpan timeDifference = DateTime.Now - model.DateTime;
            int jobDelayInSeconds = (int)timeDifference.TotalSeconds;
            // Delayed Job
            var jobId = BackgroundJob.Schedule(() => SendMail(model.Message, model.To), 
                                                   TimeSpan.FromSeconds(jobDelayInSeconds));
            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        // Methods
        public void SendOrderInvoiceMail()
        {
            Console.WriteLine("Order information mail is sent.");
        }

        public void SendMail(string message, string to)
        {
            Console.WriteLine($"Your message '{message}' is sent to: {to}");
        }

        public void SendPromotionMail()
        {
            Console.WriteLine("Weekly mail is sent for subscribers.");
        }
        
        public void CreateInvoice()
        {
            Console.WriteLine("Invoice is created.");
        }
    }
}