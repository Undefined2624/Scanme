using QRMascotas.Models;
using System;
using System.Collections.Generic;

namespace QRMascotas.context;

public partial class Mascota
{
    public int IdMascota { get; set; }

    public string IdDueno { get; set; } = null!;

    public int? IdDuenoAlternativo { get; set; }

    public string Nombre { get; set; } = null!;

    public int IdEspecie { get; set; }

    public DateTime FechaNacimiento { get; set; }

    public bool Genero { get; set; }

    public string Color { get; set; } = null!;

    public bool Esterilizado { get; set; }

    public string? DatosExtra { get; set; }

    public string? Importante { get; set; }

    public string? Qr { get; set; }

    public virtual DuenoAlternativo? IdDuenoAlternativoNavigation { get; set; }

    public virtual ApplicationUser IdDuenoNavigation { get; set; } = null!;

    public virtual Especy IdEspecieNavigation { get; set; } = null!;
}
