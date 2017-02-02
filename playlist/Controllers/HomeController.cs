using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace TestTwo_20151.ViewModels
{
    public class HomeController : Controller
    {

        private RepoImage man = new RepoImage();
        // GET: Home
        public ActionResult Index()
        {
            return View(man.GetImagesForList());
        }

        // GET: Home/Details/5
        public ActionResult Details(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ImageFull movie = man.GetImageFileById(id);
            if (movie == null)
            {
                return HttpNotFound();
            }
            return View(movie);
        }
    }
}