﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HW8.Models;
using HW8.DAL;
using HW8.Models.ViewModel;
using System.Diagnostics;
using System.Net;
using System.Data.Entity;
using Newtonsoft.Json;

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
            LoadTables();
            VM.ArtGenres = VM.Db.Genres.ToList();
            foreach(var temp in VM.ArtGenres)
            {
                Debug.WriteLine(temp);
            }
            return View(VM);
        }

        private void LoadTables()
        {
            VM.Artists = VM.Db.Artists;
            VM.ArtWorks = VM.Db.ArtWorks;
            VM.Classifications = VM.Db.Classifications;
        }

        /// <summary>
        /// View with a table of all artists, and buttons to create and edit artists
        /// </summary>
        /// <returns>Artists view</returns>
        public ActionResult Artists()
        {
            LoadTables();
            return View(VM);
        }

        /// <summary>
        /// View witth a table of all artworks
        /// </summary>
        /// <returns>Artworks view</returns>
        public ActionResult ArtWorks()
        {
            LoadTables();
            return View(VM);
        }

        /// <summary>
        /// View with a table of all classifications
        /// </summary>
        /// <returns>Classifications view</returns>
        public ActionResult Classifications()
        {
            LoadTables();
            return View(VM);
        }

        /// <summary>
        /// Page that allows user to enter information to be added to artists table
        /// </summary>
        /// <returns>Create artist view</returns>
        public ActionResult CreateArtist()
        {
            ViewBag.Title = "Add a new Artist";
            return View();
        }
        /// <summary>
        /// Grabs the information the user entered and checks to make sure it is valid
        /// before entering it into the table
        /// </summary>
        /// <param name="AnArtist">Information user entered to be entered into the artist table</param>
        /// <returns>Artists view if information was correct, or create page with errors if not</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateArtist([Bind(Include = "Name,BirthDate,BirthCity")] Artist AnArtist)
        {
            LoadTables();
            if(ModelState.IsValid)
            {
                if(AnArtist.BirthDate > DateTime.Today)
                {
                    ModelState.AddModelError("BirthDate", "Birth Date must be set in the past");
                    VM.AnArtist = AnArtist;
                    return View(VM);
                }
                VM.Db.Artists.Add(AnArtist);
                VM.Db.SaveChanges();
                return RedirectToAction("Artists");
            }
            return View(VM);
        }


        /// <summary>
        /// Details for artists that pulls all artworks, and genres of those artworks,
        /// and displays them
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public ActionResult ArtistDetails(int ID)
        {
            LoadTables();
            VM.AnArtist = VM.Db.Artists.Where(a => a.ID == ID).FirstOrDefault();
            VM.Paintings = VM.Db.ArtWorks.Where(a => a.Artist == VM.AnArtist.Name);

            return View(VM);
        }

        public ActionResult EditArtist(int? ID)
        {
            LoadTables();
            VM.AnArtist = VM.Db.Artists.Where(a => a.ID == ID).FirstOrDefault();
            if (VM.AnArtist == null)
            {
                return HttpNotFound();
            }
            ViewBag.Title = "Edit " + VM.AnArtist.Name;
            return View(VM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditArtist([Bind(Include = "Name,BirthDate,BirthCity")] Artist AnArtist)
        {
            LoadTables();
            ViewBag.Title = "Edit " + AnArtist.Name;
            if (ModelState.IsValid)
            {
                VM.Db.Entry(AnArtist).State = EntityState.Modified;
                VM.Db.SaveChanges();
                return RedirectToAction("Artists");
            }
            return View(VM);
        }

        /// <summary>
        /// delete an artist from the database
        /// </summary>
        /// <param name="ID">ID of artist to be deleted</param>
        /// <returns>the artists view page</returns>
        public ActionResult DeleteArtist(int ID)
        {
            LoadTables();
            try { 
            VM.AnArtist = VM.Db.Artists.Where(a => a.ID == ID).FirstOrDefault();
            VM.Db.Artists.Remove(VM.AnArtist);
            VM.Db.SaveChanges();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                return RedirectToAction("Artists");

            }
            return RedirectToAction("Artists");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="genre"></param>
        /// <returns></returns>
        public JsonResult Search(string genre)
        {
            List<String> temp = VM.Db.Classifications.Where(g => g.Genre == genre).Select(g => g.ArtWork).ToList();
            List<ArtTable> tempart = new List<ArtTable>();
            ArtWork t;
            ArtTable aTemp;
            foreach (String art in temp)
            {
                aTemp = new ArtTable();
                t = VM.Db.ArtWorks.Where(a => a.Title == art).FirstOrDefault();
                aTemp.Artist = t.Artist;
                aTemp.Title = t.Title;
                tempart.Add(aTemp);
            }
            string rjson = JsonConvert.SerializeObject(tempart, Formatting.Indented);
            return Json(rjson, JsonRequestBehavior.AllowGet);
        }
    }
}