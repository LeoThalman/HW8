using System;
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

        /// <summary>
        /// returns the homepage, which has buttons on it allowing the user to load all
        /// artwork of a specific genre
        /// </summary>
        /// <returns>The home page</returns>
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

        /// <summary>
        /// loads in the information for the tables from the database
        /// </summary>
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
        /// Details for artists
        /// </summary>
        /// <param name="ID"> ID of Artist</param>
        /// <returns>A View displaying the Artist Details</returns>
        public ActionResult ArtistDetails(int ID)
        {
            LoadTables();
            VM.AnArtist = VM.Db.Artists.Where(a => a.ID == ID).FirstOrDefault();
            VM.Paintings = VM.Db.ArtWorks.Where(a => a.Artist == VM.AnArtist.Name);
            VM.Genres = VM.Db.Classifications.Where(a => a.ArtWork1.Artist == VM.AnArtist.Name);
            return View(VM);
        }

        /// <summary>
        /// Allows user to edit artist details, takes the artist ID from the user and then allows them
        /// to edit that artist's information, all fields must be filed
        /// </summary>
        /// <param name="ID">ID of the artist in the table</param>
        /// <returns>A view that has input fields to edit table information</returns>
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

        /// <summary>
        /// Takes the information from the view and makes sure it's all filled out.
        /// If it is then edits that artist in the table, otherwise sends user back to the edit
        /// view and requests that all information be filled out
        /// </summary>
        /// <param name="AnArtist">The information to be edited on the table</param>
        /// <returns>Goes back to artists if correct, otherwise back to edit view</returns>
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
        /// takes an genre from the view and searches for all artwork of that genre,
        /// then returns a json object of both artwork and artist.
        /// </summary>
        /// <param name="genre">genre to look up and return</param>
        /// <returns>all artwork of specified genre</returns>
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