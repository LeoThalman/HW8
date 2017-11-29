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
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Artist()
        {
            ArtWorks = new HashSet<ArtWork>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Key]
        [StringLength(50)]
        public string Name { get; set; }

        [Column(TypeName = "date")]
        [DateCorrectRange(ValidateBirthDate = true, ErrorMessage = "Birth Date can't be in the future")]
        public DateTime BirthDate { get; set; }

        [Required]
        [StringLength(100)]
        public string BirthCity { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ArtWork> ArtWorks { get; set; }
    }
}
