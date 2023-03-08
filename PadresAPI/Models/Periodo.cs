using System;
using System.Collections.Generic;

namespace PadresAPI.Models;

public partial class Periodo
{
    public int Id { get; set; }

    public short Año { get; set; }

    public virtual ICollection<Calificacion> Calificacions { get; } = new List<Calificacion>();

    public virtual ICollection<DocenteAlumno> DocenteAlumnos { get; } = new List<DocenteAlumno>();

    public virtual ICollection<DocenteGrupo> DocenteGrupos { get; } = new List<DocenteGrupo>();
}
