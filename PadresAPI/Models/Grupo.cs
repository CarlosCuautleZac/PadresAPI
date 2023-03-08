using System;
using System.Collections.Generic;

namespace PadresAPI.Models;

public partial class Grupo
{
    public int Id { get; set; }

    public string Grado { get; set; } = null!;

    public string Seccion { get; set; } = null!;

    public virtual ICollection<Alumno> Alumnos { get; } = new List<Alumno>();

    public virtual ICollection<DocenteGrupo> DocenteGrupos { get; } = new List<DocenteGrupo>();
}
