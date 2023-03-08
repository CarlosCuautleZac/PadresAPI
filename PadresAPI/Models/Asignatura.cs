using System;
using System.Collections.Generic;

namespace PadresAPI.Models;

public partial class Asignatura
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public int TipoAsignatura { get; set; }

    public virtual ICollection<Calificacion> Calificacions { get; } = new List<Calificacion>();

    public virtual ICollection<DocenteAsignatura> DocenteAsignaturas { get; } = new List<DocenteAsignatura>();
}
