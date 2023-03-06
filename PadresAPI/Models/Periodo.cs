using System;
using System.Collections.Generic;

namespace PadresAPI.Models;

public partial class Periodo
{
    public int Id { get; set; }

    public short Año { get; set; }

    public virtual ICollection<Calificacion> Calificacion { get; } = new List<Calificacion>();
}
