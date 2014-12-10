using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog1.Models
{
    public class PayPalModel
    {
        public PayPalModel(bool useSandbox)
        {
            cmd = "_xclick";
            business = ConfigurationManager.AppSettings["business"];
            cancel_return = 
                ConfigurationManager.AppSettings["cancel_return"];
            @return = ConfigurationManager.AppSettings["return"];
            
            if (useSandbox)
                actionURL = ConfigurationManager.AppSettings["test_url"];
            else
                actionURL = ConfigurationManager.AppSettings["Prod_url"];

            notify_url = ConfigurationManager.AppSettings["notify_url"];
            currency_code = 
                ConfigurationManager.AppSettings["currency_code"];
        }

        public string cmd { get; set; }
        public string business { get; set; }
        public string no_shipping { get; set; }
        public string @return { get; set; }
        public string cancel_return { get; set; }
        public string notify_url { get; set; }
        public string currency_code { get; set; }
        public string item_name { get; set; }
        public string amount { get; set; }
        public string actionURL { get; set; }
    }
}
