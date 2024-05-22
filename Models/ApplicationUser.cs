using Microsoft.AspNetCore.Identity;
using QRMascotas.context;

namespace QRMascotas.Models
{
    public class ApplicationUser : IdentityUser
    {
        public required string Nombre { get; set; }
        public required string ApellidoP { get; set; }
        public required string ApellidoM { get; set; }
        public required string Direccion { get; set; }
        public required string Contacto { get; set; }
        public required ICollection<Mascota> Mascota { get; set; }
    }
}
