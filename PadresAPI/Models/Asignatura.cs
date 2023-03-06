using System;
using System.Collections.Generic;

namespace PadresAPI.Models;

public partial class Asignatura
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public int IdTipo { get; set; }

    public virtual ICollection<Calificacion> Calificacion { get; } = new List<Calificacion>();

    public virtual ICollection<DocenteAsignatura> DocenteAsignatura { get; } = new List<DocenteAsignatura>();

    public virtual TipoAsignatura IdTipoNavigation { get; set; } = null!;
}
