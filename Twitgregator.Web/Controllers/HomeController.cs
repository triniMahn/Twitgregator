using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Twitgregator.DAL.Implementation;
using Twitgregator.Projections.Implementation;

namespace Twitgregator.Web.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index2()
        {
            return View();
        }

        public ActionResult Index()
        {
            TwitterRESTAPIRepository repo = new TwitterRESTAPIRepository();
            
            TwitterViewModelFactory factory = new TwitterViewModelFactory();
            object[] args = new object[] { "pay_by_phone", DateTime.Now.AddDays(-14) };
            TwitterPostListViewModel vm = (TwitterPostListViewModel)factory.createViewModel(repo, args);
            ViewBag.ViewModel = vm;

            return View();
        }

    }
}
