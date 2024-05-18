using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QRMascotas.context;

namespace QRMascotas.Controllers
{
    public class VacunasController : Controller
    {
        private readonly QrmascotasContext _context;

        public VacunasController(QrmascotasContext context)
        {
            _context = context;
        }

        // GET: Vacunas
        public async Task<IActionResult> Index()
        {
            var qrmascotasContext = _context.Vacunas.Include(v => v.IdEspecieNavigation);
            return View(await qrmascotasContext.ToListAsync());
        }

        // GET: Vacunas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Vacunas == null)
            {
                return NotFound();
            }

            var vacuna = await _context.Vacunas
                .Include(v => v.IdEspecieNavigation)
                .FirstOrDefaultAsync(m => m.IdVacuna == id);
            if (vacuna == null)
            {
                return NotFound();
            }

            return View(vacuna);
        }

        // GET: Vacunas/Create
        public IActionResult Create()
        {
            ViewData["IdEspecie"] = new SelectList(_context.Especies, "IdEspecie", "IdEspecie");
            return View();
        }

        // POST: Vacunas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdVacuna,Nombre,IdEspecie")] Vacuna vacuna)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vacuna);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdEspecie"] = new SelectList(_context.Especies, "IdEspecie", "IdEspecie", vacuna.IdEspecie);
            return View(vacuna);
        }

        // GET: Vacunas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Vacunas == null)
            {
                return NotFound();
            }

            var vacuna = await _context.Vacunas.FindAsync(id);
            if (vacuna == null)
            {
                return NotFound();
            }
            ViewData["IdEspecie"] = new SelectList(_context.Especies, "IdEspecie", "IdEspecie", vacuna.IdEspecie);
            return View(vacuna);
        }

        // POST: Vacunas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdVacuna,Nombre,IdEspecie")] Vacuna vacuna)
        {
            if (id != vacuna.IdVacuna)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vacuna);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VacunaExists(vacuna.IdVacuna))
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
            ViewData["IdEspecie"] = new SelectList(_context.Especies, "IdEspecie", "IdEspecie", vacuna.IdEspecie);
            return View(vacuna);
        }

        // GET: Vacunas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Vacunas == null)
            {
                return NotFound();
            }

            var vacuna = await _context.Vacunas
                .Include(v => v.IdEspecieNavigation)
                .FirstOrDefaultAsync(m => m.IdVacuna == id);
            if (vacuna == null)
            {
                return NotFound();
            }

            return View(vacuna);
        }

        // POST: Vacunas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Vacunas == null)
            {
                return Problem("Entity set 'QrmascotasContext.Vacunas'  is null.");
            }
            var vacuna = await _context.Vacunas.FindAsync(id);
            if (vacuna != null)
            {
                _context.Vacunas.Remove(vacuna);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VacunaExists(int id)
        {
          return (_context.Vacunas?.Any(e => e.IdVacuna == id)).GetValueOrDefault();
        }
    }
}
