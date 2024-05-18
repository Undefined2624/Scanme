using System;
using System.Collections.Generic;

namespace QRMascotas.context;

public partial class Vacuna
{
    public int IdVacuna { get; set; }

    public string Nombre { get; set; } = null!;

    public int? IdEspecie { get; set; }

    public virtual Especy? IdEspecieNavigation { get; set; }
}
