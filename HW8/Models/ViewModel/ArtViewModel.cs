using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HW8.Models;
using HW8.DAL;

namespace HW8.Models.ViewModel
{
    public class ArtViewModel
    {
        public IEnumerable<ArtWork> Paintings { get; set; }
        public IEnumerable<Classification> Genres { get; set; }
        public Artist AnArtist { get; set; }
        public IEnumerable<Artist> Artists { get; set; }
        public IEnumerable<ArtWork> ArtWorks { get; set; }
        public IEnumerable<Classification> Classifications { get; set; }
        public ArtistContext Db { get; set; }
    }
}