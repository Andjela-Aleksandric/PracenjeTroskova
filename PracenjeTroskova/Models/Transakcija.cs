using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;

namespace PracenjeTroskova.Models
{
    public class Transakcija
    {
        [Key]
        public int TransakcijaID { get; set; }

        //spoljni kljuc KategorijaID
        [Range(1,int.MaxValue,ErrorMessage = "Izbor kategorije je obavezan")]
        public int KategorijaID { get; set; }
        public Kategorija? Kategorija { get; set; } //navigational property za spoljni kljuc

        [Range(1, int.MaxValue, ErrorMessage = "Iznos mora biti veći od nule")]
        public int Iznos { get; set; }

        [Column(TypeName = "nvarchar(75)")]
        public string? Opis { get; set; }

        public DateTime Datum { get; set; } = DateTime.Now; //podrazumevana vrednost

        [NotMapped]
        public string? NazivKategorijeSaIkonicom 
        { get 
            {
                return Kategorija == null ? "" : Kategorija.Ikonica + " " + Kategorija.Naziv;
            } 
        }

        [NotMapped]
        public string? formatiraniIznos
        {
            
            get
            {
                CultureInfo culture = new CultureInfo("en-US");
                culture.NumberFormat.CurrencySymbol = "€";
                return ((Kategorija == null || Kategorija.Tip == "Trošak") ? "-" : "+") + Iznos.ToString("C",culture);
            }
        }
    }
}
