using System;
using System.Collections.Generic;

namespace QRMascotas.context;

public partial class Dueno
{
    public int IdDueno { get; set; }

    public string Nombre { get; set; } = null!;

    public string ApellidoP { get; set; } = null!;

    public string ApellidoM { get; set; } = null!;

    public string Contacto { get; set; } = null!;

    public string Direccion { get; set; } = null!;

    public virtual ICollection<Mascota> Mascota { get; set; } = new List<Mascota>();
}
