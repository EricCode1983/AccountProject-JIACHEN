using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace TestProject.Controllers
{
    public class HomeController : Controller
    {

      
        public async Task<ActionResult> Index()
        {
            //Task<string> t1 = GetExpensiveStringAsync();
            //Task<string> t2 = GetAnotherExpensiveStringAsync();

            //await Task.WhenAll(t1, t2);

            //string s1 = t1.Result;
            //string s2 = t1.Result;

            //string sum = s1 + s2;


            return View();
        }



        private static Task<string> GetExpensiveStringAsync()
        {
            return Task<string>.Factory.StartNew(() => GetExpensiveString());
        }

        private static Task<string> GetAnotherExpensiveStringAsync()
        {
            return Task<string>.Factory.StartNew(() => GetAnotherExpensiveString());
        }


        private static string GetExpensiveString()
        {
            for (int i = 0; i < 1; i++)
                Thread.Sleep(1000); // allow other threads to jump in

            return DateTime.Now.ToLongTimeString();
        }


        private static string GetAnotherExpensiveString()
        {
            for (int i = 0; i < 1; i++)
                Thread.Sleep(1000); // allow other threads to jump in

            return DateTime.Now.ToLongTimeString();
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}