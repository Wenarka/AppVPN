using AppVPN.Models;
using AppVPN.Models.Data;
using AppVPN.ViewModels.Countries;
using Humanizer.Localisation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AppVPN.Controllers
{
    public class CountriesController : Controller
    {
        private readonly AppCtx _context;

        public CountriesController(AppCtx context)
        {
            _context = context;
        }

        // GET: Countries
        public async Task<IActionResult> Index()
        {
            var appCtx = _context.Countries
                .OrderBy(f => f.CountryServer);

            return _context.Countries != null ? 
                          View(await appCtx.ToListAsync()) :
                          Problem("Entity set 'AppCtx.Countries'  is null.");
        }

        // GET: Countries/Details/5
        public async Task<IActionResult> Details(short? id)
        {
            if (id == null || _context.Countries == null)
            {
                return NotFound();
            }

            var country = await _context.Countries
                .FirstOrDefaultAsync(m => m.Id == id);
            if (country == null)
            {
                return NotFound();
            }

            return View(country);
        }

        // GET: Countries/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Countries/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateCountryViewModel model)
        {
            if (_context.Countries
                .Where(f => f.CountryServer == model.CountryServer)
                .FirstOrDefault() != null)
            {
                ModelState.AddModelError("", "Введеный сервер страны уже существует");
            }

            if (ModelState.IsValid)
            {
                Country country = new()
                {
                    CountryServer = model.CountryServer
                };

                _context.Add(country);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // GET: Countries/Edit/5
        public async Task<IActionResult> Edit(short? id)
        {
            if (id == null || _context.Countries == null)
            {
                return NotFound();
            }

            var country = await _context.Countries.FindAsync(id);
            if (country == null)
            {
                return NotFound();
            }

            EditCountryViewModel model = new()
            {
                Id = country.Id,
                CountryServer = country.CountryServer
            };

            return View(country);
        }

        // POST: Countries/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(short id, EditCountryViewModel model)
        {
            Country country = await _context.Countries.FindAsync(id);

            if (id != country.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    country.CountryServer = model.CountryServer;
                    _context.Update(country);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CountryExists(country.Id))
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
            return View(model);
        }

        // GET: Countries/Delete/5
        public async Task<IActionResult> Delete(short? id)
        {
            if (id == null || _context.Countries == null)
            {
                return NotFound();
            }

            var country = await _context.Countries
                .FirstOrDefaultAsync(m => m.Id == id);
            if (country == null)
            {
                return NotFound();
            }

            return View(country);
        }

        // POST: Countries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(short id)
        {
            if (_context.Countries == null)
            {
                return Problem("Entity set 'AppCtx.Countries'  is null.");
            }
            var country = await _context.Countries.FindAsync(id);
            if (country != null)
            {
                _context.Countries.Remove(country);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CountryExists(short id)
        {
          return (_context.Countries?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
