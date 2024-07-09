using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PracenjeTroskova.Models;

namespace PracenjeTroskova.Controllers
{
    public class TransakcijeController : Controller
    {
        private readonly AplikacijaBP _context;

        public TransakcijeController(AplikacijaBP context)
        {
            _context = context;
        }

        // GET: Transakcije
        public async Task<IActionResult> Index()
        {
            var aplikacijaBP = _context.Transakcije.Include(t => t.Kategorija);
            return View(await aplikacijaBP.ToListAsync());
        }



        // GET: Transakcije/DodajIliIzmeni
        public IActionResult DodajIliIzmeni(int id = 0)
        {
            proslediKategorije();
            if(id == 0)
                return View(new Transakcija());
            else
                return View(_context.Transakcije.Find(id));
        }

        // POST: Transakcije/DodajIliIzmeni
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DodajIliIzmeni([Bind("TransakcijaID,KategorijaID,Iznos,Opis,Datum")] Transakcija transakcija)
        {
            if (ModelState.IsValid)
            {
                if(transakcija.TransakcijaID == 0)
                    _context.Add(transakcija);
                else
                    _context.Update(transakcija);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            proslediKategorije();
            return View(transakcija);
        }


        // POST: Transakcije/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var transakcija = await _context.Transakcije.FindAsync(id);
            if (transakcija != null)
            {
                _context.Transakcije.Remove(transakcija);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [NonAction]
        public void proslediKategorije()
        {
            var KolekcijaKategorija = _context.Kategorije.ToList();
            Kategorija podrazumnevanaKategorija = new Kategorija() { KategorijaID = 0, Naziv = "Izaberite kategoriju"};
            KolekcijaKategorija.Insert(0, podrazumnevanaKategorija);
            ViewBag.Kategorije = KolekcijaKategorija;
        }
    }
}
