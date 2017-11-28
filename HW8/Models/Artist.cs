namespace HW8.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Artist")]
    public partial class Artist
    {
        public int ID { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [Column(TypeName = "date")]
        [DateCorrectRange(ValidateBirthDate = true, ErrorMessage = "Birth date can't be in the future")]
        public DateTime BirthDate { get; set; }

        [Required]
        [StringLength(100)]
        public string BirthCity { get; set; }
    }
}
