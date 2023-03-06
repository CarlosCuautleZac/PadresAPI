using System;
using System.Collections.Generic;

namespace PadresAPI.Models;

public partial class Grupo
{
    public int Id { get; set; }

    public int IdAlumno { get; set; }

    public int IdDocente { get; set; }

    public string? Seccion { get; set; }

    public virtual Alumno IdAlumnoNavigation { get; set; } = null!;

    public virtual Docente IdDocenteNavigation { get; set; } = null!;
}
