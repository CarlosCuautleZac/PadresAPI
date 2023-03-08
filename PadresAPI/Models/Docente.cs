using System;
using System.Collections.Generic;

namespace PadresAPI.Models;

public partial class Docente
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string ApellidoPaterno { get; set; } = null!;

    public string ApellidoMaterno { get; set; } = null!;

    public string Correo { get; set; } = null!;

    public string Telefono { get; set; } = null!;

    public int Edad { get; set; }

    public int TipoDocente { get; set; }

    public int IdUsuario { get; set; }

    public virtual ICollection<Calificacion> Calificacions { get; } = new List<Calificacion>();

    public virtual ICollection<DocenteAlumno> DocenteAlumnos { get; } = new List<DocenteAlumno>();

    public virtual ICollection<DocenteAsignatura> DocenteAsignaturas { get; } = new List<DocenteAsignatura>();

    public virtual ICollection<DocenteGrupo> DocenteGrupos { get; } = new List<DocenteGrupo>();

    public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
}
