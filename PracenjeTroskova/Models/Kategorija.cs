using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PracenjeTroskova.Models
{
    public class Kategorija
    {

        [Key]
        public int KategorijaID { get; set; }

        [Column(TypeName = "nvarchar(60)")]
        [Required(ErrorMessage = "Naziv je obavezan")]
        public string Naziv { get; set; }

        [Column(TypeName = "nvarchar(5)")]
        public string Ikonica { get; set; } = "";

        [Column(TypeName = "nvarchar(10)")]
        public string Tip { get; set; } = "Trošak"; //vecina transakcija ce biti troskovi, pa to stavljam kao default vrednost

        [NotMapped]
        public string? NazivSaIkonicom { 
            get
            {
                return this.Ikonica + " " + this.Naziv;
            }
        }
    }
}
