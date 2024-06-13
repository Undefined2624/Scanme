using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QRMascotas.ClasesModelo;
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
        public IActionResult Create(int MascotaId)
        {
            ViewBag.MascotaId = MascotaId;
            return PartialView();
        }

        // POST: DuenoAlternativo/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DuenoAlternativo duenoAlternativo, int MascotaId)

        {
            if (ModelState.IsValid)
            {
                _context.Add(duenoAlternativo);
                await _context.SaveChangesAsync();

                var mascota = await _context.Mascotas.FindAsync(MascotaId);

                if (mascota != null)
                {
                    mascota.IdDuenoAlternativo = duenoAlternativo.IdDuenoAlternativo;
                    _context.Update(mascota);

                    await _context.SaveChangesAsync();
                }

                return RedirectToAction(nameof(Index), "Mascotas");
            }

            ViewBag.MascotaId = MascotaId;
            return View(duenoAlternativo);
        }

        // GET: DuenoAlternativo/Edit/5
        public async Task<IActionResult> Edit(int? DuenoId)
        {
            if (DuenoId == null || _context.DuenoAlternativos == null)
            {
                return NotFound();
            }

            var duenoAlternativo = await _context.DuenoAlternativos.FindAsync(DuenoId);
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
        public async Task<IActionResult> Edit(int DuenoId, DuenoAlternativo duenoAlternativo)
        {
            if (duenoAlternativo.IdDuenoAlternativo != duenoAlternativo.IdDuenoAlternativo)
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
                return RedirectToAction(nameof(Index), "Mascotas");
            }

            return RedirectToAction("Index", "Mascotas");

        }

        // GET: DuenoAlternativo/Delete/5
        public async Task<IActionResult> Delete(int? DuenoId)
        {
            if (DuenoId == null || _context.DuenoAlternativos == null)
            {
                return NotFound();
            }

            var duenoAlternativo = await _context.DuenoAlternativos
                .FirstOrDefaultAsync(m => m.IdDuenoAlternativo == DuenoId);
            if (duenoAlternativo == null)
            {
                return NotFound();
            }

            return View(duenoAlternativo);
        }

        // POST: DuenoAlternativo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int DuenoId)
        {
            if (_context.DuenoAlternativos == null)
            {
                return Problem("Entity set 'QrmascotasContext.DuenoAlternativos'  is null.");
            }

            // Obtener las mascotas que tienen el DuenoAlternativo que se quiere eliminar
            var mascotas = _context.Mascotas.Where(m => m.IdDuenoAlternativo == DuenoId).ToList();

            // Desvincular las mascotas del DuenoAlternativo
            foreach (var mascota in mascotas)
            {
                mascota.IdDuenoAlternativo = null; // O asigna un valor predeterminado si es necesario
            }

            // Guardar los cambios para las mascotas
            _context.Mascotas.UpdateRange(mascotas);
            await _context.SaveChangesAsync();

            var duenoAlternativo = await _context.DuenoAlternativos.FindAsync(DuenoId);
            if (duenoAlternativo != null)
            {
                _context.DuenoAlternativos.Remove(duenoAlternativo);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index", "Mascotas");
        }

        private bool DuenoAlternativoExists(int DuenoId)
        {
          return (_context.DuenoAlternativos?.Any(e => e.IdDuenoAlternativo == DuenoId)).GetValueOrDefault();
        }
    }
}
