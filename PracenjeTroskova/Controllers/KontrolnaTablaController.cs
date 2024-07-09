using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PracenjeTroskova.Models;
using System.Globalization;


namespace PracenjeTroskova.Controllers
{
    public class KontrolnaTablaController : Controller
    {
        private readonly AplikacijaBP _context;
        public KontrolnaTablaController(AplikacijaBP context)
        {
            _context = context;
        }
        public async Task<ActionResult> Index()
        {
            //Transakcije od poslednjih 7 dana

            DateTime pocetniDatum = DateTime.Today.AddDays(-6);
            DateTime krajnjiDatum = DateTime.Today;
            List<Transakcija> transakcije = await _context.Transakcije
                .Include(x => x.Kategorija)
                .Where(y => y.Datum >= pocetniDatum &&  y.Datum <= krajnjiDatum)
                .ToListAsync();

            //ukupni prihodi
            int ukupanPrihod = transakcije
                .Where(i => i.Kategorija.Tip == "Prihod")
                .Sum(j => j.Iznos);
            CultureInfo culture = new CultureInfo("en-US");
            culture.NumberFormat.CurrencySymbol = "€";
            ViewBag.UkupanPrihod = ukupanPrihod.ToString("C",culture);

            //ukupni troskovi
            int ukupanTrosak = transakcije
                .Where(i => i.Kategorija.Tip == "Trošak")
                .Sum(j => j.Iznos);       
            ViewBag.ukupanTrosak = ukupanTrosak.ToString("C",culture);

            //Prihodi - Troskovi
            int dobit = ukupanPrihod - ukupanTrosak;
            ViewBag.dobit = dobit.ToString("C0");

            // Pita grafik - troskovi po kategorijama
            ViewBag.pitaGrafik = transakcije
                .Where(i => i.Kategorija.Tip == "Trošak")
                .GroupBy(j => j.Kategorija.KategorijaID)
                .Select(k => new
                {
                    nazivKategorijeSaIkonicom = k.First().Kategorija.Ikonica + " " + k.First().Kategorija.Naziv,
                    iznos = k.Sum(j => j.Iznos),
                    formatiraniIznos = k.Sum(j => j.Iznos).ToString("C0"),
                })
                .OrderByDescending(l => l.iznos)
                .ToList();

            // Grafik - prihodi i troskovi
            // Prihodi
            List<PodaciZaGrafik> prihodi = transakcije
                .Where(i => i.Kategorija.Tip == "Prihod")
                .GroupBy(j => j.Datum)
                .Select(k => new PodaciZaGrafik()
                {
                    dan = k.First().Datum.ToString("dd-MMM"),
                    prihod = k.Sum(l => l.Iznos)
                })
                .ToList();

            // Troskovi
            List<PodaciZaGrafik> troskovi = transakcije
                .Where(i => i.Kategorija.Tip == "Trošak")
                .GroupBy(j => j.Datum)
                .Select(k => new PodaciZaGrafik()
                {
                    dan = k.First().Datum.ToString("dd-MMM"),
                    trosak = k.Sum(l => l.Iznos)
                })
                .ToList();

            // Kombinovani prihodi i troskovi
            string[] poslednjih7Dana = Enumerable.Range(0, 7)
                .Select(i => pocetniDatum.AddDays(i).ToString("dd-MMM"))
                .ToArray();

            ViewBag.podaciZaGrafik = from dan in poslednjih7Dana
                                     join prihod in prihodi on dan equals prihod.dan into spojeniDanIPrihod
                                      from prihod in spojeniDanIPrihod.DefaultIfEmpty()
                                      join trosak in troskovi on dan equals trosak.dan into spojeniDanITrosak
                                     from trosak in spojeniDanITrosak.DefaultIfEmpty()
                                      select new
                                      {
                                          dan = dan,
                                          prihod = prihod == null ? 0 : prihod.prihod,
                                          trosak = trosak == null ? 0 : trosak.trosak,
                                      };
            // Poslednje transakcije
            ViewBag.poslednjeTransakcije = await _context.Transakcije
                .Include(i => i.Kategorija)
                .OrderByDescending(j => j.Datum)
                .Take(5)
                .ToListAsync();

            return View();
        }
    }

    public class PodaciZaGrafik
    {
        public string dan;
        public int prihod;
        public int trosak;

    }
}
