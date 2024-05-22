using QRMascotas.context;
using System.ComponentModel.DataAnnotations;

namespace QRMascotas.ClasesModelo
{
    public class Mascotax : Mascota
    {
        public string EsterilizadoTexto => Esterilizado ? "Sí" : "No";
        public string GeneroTexto => Genero ? "Macho" : "Hembra";

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime FechaNac => FechaNacimiento.Date;

        public string NombreDuenoA => IdDuenoAlternativoNavigation.Nombre + " " + IdDuenoAlternativoNavigation.ApellidoP + " " + IdDuenoAlternativoNavigation.ApellidoM;
        public string NombreDueno => IdDuenoNavigation.Nombre + " " + IdDuenoNavigation.ApellidoP + " " + IdDuenoNavigation.ApellidoM;

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
                IdDuenoAlternativoNavigation = Mascota.IdDuenoAlternativoNavigation,
                IdDuenoNavigation = Mascota.IdDuenoNavigation,
                IdEspecieNavigation = Mascota.IdEspecieNavigation
            };
        }
 

    }
}
