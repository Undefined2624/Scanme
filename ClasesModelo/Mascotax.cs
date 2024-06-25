using QRMascotas.context;
using System.ComponentModel.DataAnnotations;

namespace QRMascotas.ClasesModelo
{
    public class Mascotax : Mascota
    {
        [Display(Name = "¿Está esterilizado?")]
        public string EsterilizadoTexto => Esterilizado ? "Sí" : "No";

        [Display(Name = "Género")]
        public string GeneroTexto => Genero ? "Macho" : "Hembra";

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Fecha de nacimiento 🎂")]
        public  DateTime FechaNac => FechaNacimiento.Date;
        
        public string? NombreDuenoA => IdDuenoAlternativoNavigation != null ? $"{IdDuenoAlternativoNavigation.Nombre} {IdDuenoAlternativoNavigation.ApellidoP} {IdDuenoAlternativoNavigation.ApellidoM}" : null;
       
        public string NombreDueno => IdDuenoNavigation.Nombre + " " + IdDuenoNavigation.ApellidoP + " " + IdDuenoNavigation.ApellidoM;

        [Display(Name = "Especie")]
        public string NombreEspecie => IdEspecieNavigation.Nombre;    

        public static Mascotax ChangeMascota (Mascota Mascota)
        {
            return new Mascotax
            {
                IdMascota = Mascota.IdMascota,
                IdDueno = Mascota.IdDueno,
                IdDuenoAlternativo = Mascota.IdDuenoAlternativo,
                Nombre = Mascota.Nombre,
                IdEspecie = Mascota.IdEspecie,
                FechaNacimiento = Mascota.FechaNacimiento,
                Genero = Mascota.Genero,
                Color = Mascota.Color,
                Esterilizado = Mascota.Esterilizado,
                DatosExtra = Mascota.DatosExtra,
                Importante = Mascota.Importante,
                Qr = Mascota.Qr,
                ImagenUrl = Mascota.ImagenUrl,
                IdDuenoAlternativoNavigation = Mascota.IdDuenoAlternativoNavigation,
                IdDuenoNavigation = Mascota.IdDuenoNavigation,
                IdEspecieNavigation = Mascota.IdEspecieNavigation
            };
        }
 

    }
}
