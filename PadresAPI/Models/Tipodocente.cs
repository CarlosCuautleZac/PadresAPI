using System;
using System.Collections.Generic;

namespace PadresAPI.Models;

public partial class Tipodocente
{
    public int Id { get; set; }

    public string Tipo { get; set; } = null!;

    public virtual ICollection<Docente> Docente { get; } = new List<Docente>();
}
