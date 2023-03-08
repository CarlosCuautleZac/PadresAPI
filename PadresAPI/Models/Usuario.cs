using System;
using System.Collections.Generic;

namespace PadresAPI.Models;

public partial class Usuario
{
    public int Id { get; set; }

    public string Usuario1 { get; set; } = null!;

    public string Contraseña { get; set; } = null!;

    public int Rol { get; set; }

    public virtual ICollection<Director> Directors { get; } = new List<Director>();

    public virtual ICollection<Docente> Docentes { get; } = new List<Docente>();

    public virtual ICollection<Tutor> Tutors { get; } = new List<Tutor>();
}
