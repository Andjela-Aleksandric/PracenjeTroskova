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
    public class KategorijeController : Controller
    {
        private readonly AplikacijaBP _context;

        public KategorijeController(AplikacijaBP context)
        {
            _context = context;
        }

        // GET: Kategorije
        public async Task<IActionResult> Index()
        {
            return View(await _context.Kategorije.ToListAsync());
        }

        // GET: Kategorije/DodajIliIzmeni
        public IActionResult DodajIliIzmeni(int id=0)
        {
            if(id == 0)
            {
                return View(new Kategorija());
            }
            else
            {
                return View(_context.Kategorije.Find(id));
            }
            
        }

        // POST: Kategorije/DodajIliIzmeni
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DodajIliIzmeni([Bind("KategorijaID,Naziv,Ikonica,Tip")] Kategorija kategorija)
        {
            if (ModelState.IsValid)
            {
                if (kategorija.KategorijaID == 0)//insert operacija
                {
                    _context.Add(kategorija);
                }
                else //update operacija
                {
                    _context.Update(kategorija);
                }
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(kategorija);
        }

        

        // POST: Kategorije/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var kategorija = await _context.Kategorije.FindAsync(id);
            if (kategorija != null)
            {
                _context.Kategorije.Remove(kategorija);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    
    }
}
