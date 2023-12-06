using AppVPN.Models;
using AppVPN.Models.Data;
using AppVPN.ViewModels.Tarifs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace AppVPN.Controllers
{
    public class TarifsController : Controller
    {
        private readonly AppCtx _context;

        public TarifsController(AppCtx context)
        {
            _context = context;
        }

        // GET: Tarifs
        public async Task<IActionResult> Index()
        {
            var appCtx = _context.Tarifs
                .Include(s => s.Country)
                .OrderBy(f => f.TarifName);

            return _context.Tarifs != null ? 
                          View(await appCtx.ToListAsync()) :
                          Problem("Entity set 'AppCtx.Tarif'  is null.");
        }

        // GET: Tarifs/Details/5
        public async Task<IActionResult> Details(short? id)
        {
            if (id == null || _context.Tarifs == null)
            {
                return NotFound();
            }

            var tarif = await _context.Tarifs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tarif == null)
            {
                return NotFound();
            }

            return View(tarif);
        }

        // GET: Tarifs/Create
        public IActionResult Create()
        {
            ViewData["AccessCountryId"] = new SelectList(_context.Countries.OrderBy(o => o.CountryServer), "Id", "CountryServer");

            return View();
        }

        // POST: Tarifs/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateTarifViewModel model)
        {
            if (_context.Tarifs
                .Where(f => f.TarifName == model.TarifName &&
                f.AccessCountryId == model.AccessCountryId &&
                f.Duration == model.Duration).FirstOrDefault() != null)       
            {
                ModelState.AddModelError("", "Введеный тариф уже существует");
            }

            if (ModelState.IsValid)
            {
                Tarif tatif = new()
                {
                    AccessCountryId = model.AccessCountryId,
                    TarifName = model.TarifName,
                    Duration = model.Duration
                };

                _context.Add(tatif);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        // GET: Tarifs/Edit/5
        public async Task<IActionResult> Edit(short? id)
        {
            if (id == null || _context.Tarifs == null)
            {
                return NotFound();
            }

            var tarif = await _context.Tarifs.FindAsync(id);
            if (tarif == null)
            {
                return NotFound();
            }
            return View(tarif);
        }

        // POST: Tarifs/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(short id, [Bind("Id,TarifName,AccessCountry,Duration")] Tarif tarif)
        {
            if (id != tarif.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tarif);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TarifExists(tarif.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(tarif);
        }

        // GET: Tarifs/Delete/5
        public async Task<IActionResult> Delete(short? id)
        {
            if (id == null || _context.Tarifs == null)
            {
                return NotFound();
            }

            var tarif = await _context.Tarifs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tarif == null)
            {
                return NotFound();
            }

            return View(tarif);
        }

        // POST: Tarifs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(short id)
        {
            if (_context.Tarifs == null)
            {
                return Problem("Entity set 'AppCtx.Tarif'  is null.");
            }
            var tarif = await _context.Tarifs.FindAsync(id);
            if (tarif != null)
            {
                _context.Tarifs.Remove(tarif);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TarifExists(short id)
        {
          return (_context.Tarifs?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
