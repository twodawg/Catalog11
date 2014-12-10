using Catalog1.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Catalog1.Controllers
{
    public class PayPalController : Controller
    {
        // GET: PayPal
        [Authorize]
        [HttpPost]
        public ActionResult ValidateCommand(string Name, string Charge)
        {
            bool useSandbox = Convert.ToBoolean(
                ConfigurationManager.AppSettings["IsSandbox"]);

            var paypal = new PayPalModel(useSandbox);

            paypal.item_name = "mtwohey2@yahoo.com";
            paypal.amount = "80";

            return View(paypal);
        }
        public ActionResult RedirectFromPaypal()
        {
            return View();
        }
        public ActionResult CancelFromPaypal()
        {
            return View();
        }
        public ActionResult NotifyFromPaypal()
        {
            return View();
        }
    }
}