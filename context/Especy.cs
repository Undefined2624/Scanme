using System;
using System.Collections.Generic;

namespace QRMascotas.context;

public partial class Especy
{
    public int IdEspecie { get; set; }

    public string Nombre { get; set; } = null!;

    public virtual ICollection<Mascota> Mascota { get; set; } = new List<Mascota>();

    public virtual ICollection<Raza> Razas { get; set; } = new List<Raza>();

    public virtual ICollection<Vacuna> Vacunas { get; set; } = new List<Vacuna>();
}
