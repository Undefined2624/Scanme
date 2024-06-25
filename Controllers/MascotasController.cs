using System.Runtime.InteropServices;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using QRMascotas.ClasesModelo;
using QRMascotas.context;
using Microsoft.AspNetCore.Hosting;


namespace QRMascotas.Controllers
{
    public class MascotasController : Controller
    {
        private readonly QrmascotasContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;

        public MascotasController(QrmascotasContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
        }

        // GET: Mascotas
        public async Task<IActionResult> Index()
         {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Obtener el ID del usuario autenticado

            var qrmascotasContext = _context.Mascotas
                .Include(m => m.IdDuenoNavigation)
                .Include(m=> m.IdDuenoAlternativoNavigation)
                .Include(m => m.IdEspecieNavigation)
                .Where(m => m.IdDueno == userId); // Filtrar las mascotas por el ID del usuario

            var mascotaxList = qrmascotasContext.Select(Mascotax.ChangeMascota).ToList();

            return View(mascotaxList);

        }

        public IActionResult InformacionAdicional(int mascotaId)
        {
            var mascota = _context.Mascotas.FirstOrDefault(m => m.IdMascota == mascotaId);
            return PartialView("InformacionAdicional", mascota);
        }


        // GET: Mascotas/Create
        public IActionResult Create()
        {
            ViewData["IdDuenoAlternativo"] = new SelectList(_context.DuenoAlternativos, "IdDuenoAlternativo", "Nombre");
            ViewData["IdEspecie"] = new SelectList(_context.Especies, "IdEspecie", "Nombre");
            return View();
        }

        // POST: Mascotas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdMascota,IdDueno,IdDuenoAlternativo,Nombre,IdEspecie,FechaNacimiento,Genero,Color,Esterilizado,DatosExtra,Importante,Qr,ImagenUrl")] Mascota mascota, IFormFile ImagenUrl)
        {
            ModelState.Remove("IdDuenoNavigation");
            ModelState.Remove("IdEspecieNavigation");
            ModelState.Remove("IdDueno");

            if (ModelState.IsValid)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                mascota.IdDueno = userId;

                if (ImagenUrl != null && ImagenUrl.Length > 0)
                {
                    //A continuación se le dará un nombre único a la imagen: 

                    string NombreImagen = Guid.NewGuid().ToString() + Path.GetExtension(ImagenUrl.FileName);
                    var imagePath = Path.Combine(_hostEnvironment.WebRootPath, "Images", NombreImagen);

                    
                    using (var fileStream = new FileStream(imagePath, FileMode.Create))
                    {
                        await ImagenUrl.CopyToAsync(fileStream);
                         mascota.ImagenUrl = "/Images/" + NombreImagen;
                    }
                }

                _context.Add(mascota);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["IdDuenoAlternativo"] = new SelectList(_context.DuenoAlternativos, "IdDuenoAlternativo", "Nombre", mascota.IdDuenoAlternativo);
            ViewData["IdEspecie"] = new SelectList(_context.Especies, "IdEspecie", "Nombre", mascota.IdEspecie);
            return View(mascota);
        }

        // GET: Mascotas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Mascotas == null)
            {
                return NotFound();
            }

            var mascota = await _context.Mascotas.FindAsync(id);
            if (mascota == null)
            {
                return NotFound();
            }

            ViewData["IdDuenoAlternativo"] = new SelectList(_context.DuenoAlternativos, "IdDuenoAlternativo", "Nombre", mascota.IdDuenoAlternativo);
            ViewData["IdEspecie"] = new SelectList(_context.Especies, "IdEspecie", "Nombre", mascota.IdEspecie);

            // Para Género
            ViewBag.Genero = new SelectList(new[]
            {
                new { Id = true, Nombre = "Macho" },
                new { Id = false, Nombre = "Hembra" }
            }, "Id", "Nombre", mascota.Genero);

            // Para Esterilizado
            ViewBag.Esterilizado = new SelectList(new[]
            {
                new { Id = true, Nombre = "Sí" },
                new { Id = false, Nombre = "No" }
            }, "Id", "Nombre", mascota.Esterilizado);


            return View(mascota);
        }

        // POST: Mascotas/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdMascota,IdDueno,IdDuenoAlternativo,Nombre,IdEspecie,FechaNacimiento,Genero,Color,Esterilizado,DatosExtra,Importante,Qr")] Mascota mascota, IFormFile ImagenUrl)
        {
            if (id != mascota.IdMascota)
            {
                return NotFound();
            }

            ModelState.Remove("IdDuenoNavigation");
            ModelState.Remove("IdEspecieNavigation");

            if (ModelState.IsValid)
            {
                try
                {
                    // Mantener el IdDueno existente
                    var existingMascota = await _context.Mascotas.AsNoTracking().FirstOrDefaultAsync(m => m.IdMascota == id);
                    if (existingMascota != null)
                    {
                        mascota.IdDueno = existingMascota.IdDueno;
                        mascota.IdDuenoAlternativo = existingMascota.IdDuenoAlternativo;
                    }

                    if (ImagenUrl != null && ImagenUrl.Length > 0)
                    {
                        var imagePath = Path.Combine(_hostEnvironment.WebRootPath, "Images", ImagenUrl.FileName);
                        using (var fileStream = new FileStream(imagePath, FileMode.Create))
                        {
                            await ImagenUrl.CopyToAsync(fileStream);
                            mascota.ImagenUrl = "/Images/" + ImagenUrl.FileName;
                        }
                    }
                    _context.Update(mascota);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MascotaExists(mascota.IdMascota))
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

            ViewData["IdDuenoAlternativo"] = new SelectList(_context.DuenoAlternativos, "IdDuenoAlternativo", "Nombre", mascota.IdDuenoAlternativo);
            ViewData["IdEspecie"] = new SelectList(_context.Especies, "IdEspecie", "Nombre", mascota.IdEspecie);
                        
            return View(mascota);
        }

        // GET: Mascotas/Delete/5
        public async Task<IActionResult> Delete(int? MascotaId)
        {
            if (MascotaId == null || _context.Mascotas == null)
            {
                return NotFound();
            }

            var mascota = await _context.Mascotas
                .Include(m => m.IdDuenoAlternativoNavigation)
                .Include(m => m.IdEspecieNavigation)
                .FirstOrDefaultAsync(m => m.IdMascota == MascotaId);
            if (mascota == null)
            {
                return NotFound();
            }

            return View(mascota);
        }

        // POST: Mascotas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int IdMascota)
        {
            if (_context.Mascotas == null)
            {
                return Problem("Entity set 'QrmascotasContext.Mascotas'  is null.");
            }
            var mascota = await _context.Mascotas.FindAsync(IdMascota);
            if (mascota != null)
            {
                _context.Mascotas.Remove(mascota);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MascotaExists(int id)
        {
            return (_context.Mascotas?.Any(e => e.IdMascota == id)).GetValueOrDefault();
        }

        // GET: Mascotas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Mascotas == null)
            {
                return NotFound();
            }

            var mascota = await _context.Mascotas
                .Include(m => m.IdDuenoAlternativoNavigation)
                .Include(m => m.IdEspecieNavigation)
                .Include(m => m.IdDuenoNavigation)
                .FirstOrDefaultAsync(m => m.IdMascota == id);
            if (mascota == null)
            {
                return NotFound();
            }

            return View(mascota);
        }


    }
}
