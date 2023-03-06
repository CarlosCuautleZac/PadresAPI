using System;
using System.Collections.Generic;

namespace PadresAPI.Models;

public partial class Alumno
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string Direccion { get; set; } = null!;

    public virtual ICollection<AlumnoTutor> AlumnoTutor { get; } = new List<AlumnoTutor>();

    public virtual ICollection<Calificacion> Calificacion { get; } = new List<Calificacion>();

    public virtual ICollection<Grupo> Grupo { get; } = new List<Grupo>();
}
