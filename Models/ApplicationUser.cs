using Microsoft.AspNetCore.Identity;
using QRMascotas.context;

namespace QRMascotas.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Nombre { get; set; }
        public string ApellidoP { get; set; }
        public string ApellidoM { get; set; }
        public string Direccion { get; set; }
        public string Contacto { get; set; }
        public ICollection<Mascota> Mascota { get; set; }
    }
}
