using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QRMascotas.context;

namespace QRMascotas.Controllers
{
    [Authorize(Roles = "Admin")]
    public class RazasController : Controller
    {
        private readonly QrmascotasContext _context;

        public RazasController(QrmascotasContext context)
        {
            _context = context;
        }

        // GET: Razas
        public async Task<IActionResult> Index()
        {
            var qrmascotasContext = _context.Razas.Include(r => r.IdEspecieNavigation);
            return View(await qrmascotasContext.ToListAsync());
        }

        // GET: Razas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Razas == null)
            {
                return NotFound();
            }

            var raza = await _context.Razas
                .Include(r => r.IdEspecieNavigation)
                .FirstOrDefaultAsync(m => m.IdRaza == id);
            if (raza == null)
            {
                return NotFound();
            }

            return View(raza);
        }

        // GET: Razas/Create
        public IActionResult Create()
        {
            ViewData["IdEspecie"] = new SelectList(_context.Especies, "IdEspecie", "IdEspecie");
            return View();
        }

        // POST: Razas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdRaza,Nombre,IdEspecie")] Raza raza)
        {
            if (ModelState.IsValid)
            {
                _context.Add(raza);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdEspecie"] = new SelectList(_context.Especies, "IdEspecie", "IdEspecie", raza.IdEspecie);
            return View(raza);
        }

        // GET: Razas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Razas == null)
            {
                return NotFound();
            }

            var raza = await _context.Razas.FindAsync(id);
            if (raza == null)
            {
                return NotFound();
            }
            ViewData["IdEspecie"] = new SelectList(_context.Especies, "IdEspecie", "IdEspecie", raza.IdEspecie);
            return View(raza);
        }

        // POST: Razas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdRaza,Nombre,IdEspecie")] Raza raza)
        {
            if (id != raza.IdRaza)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(raza);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RazaExists(raza.IdRaza))
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
            ViewData["IdEspecie"] = new SelectList(_context.Especies, "IdEspecie", "IdEspecie", raza.IdEspecie);
            return View(raza);
        }

        // GET: Razas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Razas == null)
            {
                return NotFound();
            }

            var raza = await _context.Razas
                .Include(r => r.IdEspecieNavigation)
                .FirstOrDefaultAsync(m => m.IdRaza == id);
            if (raza == null)
            {
                return NotFound();
            }

            return View(raza);
        }

        // POST: Razas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Razas == null)
            {
                return Problem("Entity set 'QrmascotasContext.Razas'  is null.");
            }
            var raza = await _context.Razas.FindAsync(id);
            if (raza != null)
            {
                _context.Razas.Remove(raza);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RazaExists(int id)
        {
          return (_context.Razas?.Any(e => e.IdRaza == id)).GetValueOrDefault();
        }
    }
}
