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
    public class DuenoAlternativoController : Controller
    {
        private readonly QrmascotasContext _context;

        public DuenoAlternativoController(QrmascotasContext context)
        {
            _context = context;
        }

        // GET: DuenoAlternativo
        public async Task<IActionResult> Index()
        {
              return _context.DuenoAlternativos != null ? 
                          View(await _context.DuenoAlternativos.ToListAsync()) :
                          Problem("Entity set 'QrmascotasContext.DuenoAlternativos'  is null.");
        }

        // GET: DuenoAlternativo/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.DuenoAlternativos == null)
            {
                return NotFound();
            }

            var duenoAlternativo = await _context.DuenoAlternativos
                .FirstOrDefaultAsync(m => m.IdDuenoAlternativo == id);
            if (duenoAlternativo == null)
            {
                return NotFound();
            }

            return View(duenoAlternativo);
        }

        // GET: DuenoAlternativo/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: DuenoAlternativo/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdDuenoAlternativo,Nombre,ApellidoP,ApellidoM,Contacto")] DuenoAlternativo duenoAlternativo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(duenoAlternativo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(duenoAlternativo);
        }

        // GET: DuenoAlternativo/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.DuenoAlternativos == null)
            {
                return NotFound();
            }

            var duenoAlternativo = await _context.DuenoAlternativos.FindAsync(id);
            if (duenoAlternativo == null)
            {
                return NotFound();
            }
            return View(duenoAlternativo);
        }

        // POST: DuenoAlternativo/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdDuenoAlternativo,Nombre,ApellidoP,ApellidoM,Contacto")] DuenoAlternativo duenoAlternativo)
        {
            if (id != duenoAlternativo.IdDuenoAlternativo)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(duenoAlternativo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DuenoAlternativoExists(duenoAlternativo.IdDuenoAlternativo))
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
            return View(duenoAlternativo);
        }

        // GET: DuenoAlternativo/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.DuenoAlternativos == null)
            {
                return NotFound();
            }

            var duenoAlternativo = await _context.DuenoAlternativos
                .FirstOrDefaultAsync(m => m.IdDuenoAlternativo == id);
            if (duenoAlternativo == null)
            {
                return NotFound();
            }

            return View(duenoAlternativo);
        }

        // POST: DuenoAlternativo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.DuenoAlternativos == null)
            {
                return Problem("Entity set 'QrmascotasContext.DuenoAlternativos'  is null.");
            }
            var duenoAlternativo = await _context.DuenoAlternativos.FindAsync(id);
            if (duenoAlternativo != null)
            {
                _context.DuenoAlternativos.Remove(duenoAlternativo);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DuenoAlternativoExists(int id)
        {
          return (_context.DuenoAlternativos?.Any(e => e.IdDuenoAlternativo == id)).GetValueOrDefault();
        }
    }
}
