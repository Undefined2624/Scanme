using System;
using System.Collections.Generic;

namespace QRMascotas.context;

public partial class Raza
{
    public int IdRaza { get; set; }

    public string Nombre { get; set; } = null!;

    public int IdEspecie { get; set; }

    public virtual Especy IdEspecieNavigation { get; set; } = null!;
}
