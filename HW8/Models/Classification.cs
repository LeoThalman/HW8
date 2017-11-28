namespace HW8.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Classification")]
    public partial class Classification
    {
        public int ID { get; set; }

        [Required]
        [StringLength(100)]
        public string ArtWork { get; set; }

        [Required]
        [StringLength(50)]
        public string Genre { get; set; }
    }
}
