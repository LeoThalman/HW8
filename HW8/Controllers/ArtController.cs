using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HW8.Models;
using HW8.DAL;
using HW8.Models.ViewModel;


namespace HW8.Controllers
{
    public class ArtController : Controller
    {
        private ArtViewModel VM = new ArtViewModel()
        {
            Db = new ArtistContext()
        };

        // GET: Art
        public ActionResult Index()
        {
            LoadMenu();
            return View(VM);
        }

        private void LoadMenu()
        {
            VM.Artists = VM.Db.Artists.ToList();
            VM.ArtWorks = VM.Db.ArtWorks.ToList();
            VM.Classifications = VM.Db.Classifications.ToList();
        }

        public ActionResult Artists()
        {
            LoadMenu();
            return View(VM);
        }

        public ActionResult ArtWorks()
        {
            LoadMenu();
            return View(VM);
        }

        public ActionResult Classifications()
        {
            LoadMenu();
            return View(VM);
        }
    }
}