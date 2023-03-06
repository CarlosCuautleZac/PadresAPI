using System;
using System.Collections.Generic;

namespace PadresAPI.Models;

public partial class Docente
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public int IdTipoDocente { get; set; }

    public int Idusuario { get; set; }

    public virtual ICollection<Calificacion> Calificacion { get; } = new List<Calificacion>();

    public virtual ICollection<DocenteAsignatura> DocenteAsignatura { get; } = new List<DocenteAsignatura>();

    public virtual ICollection<Grupo> Grupo { get; } = new List<Grupo>();

    public virtual Tipodocente IdTipoDocenteNavigation { get; set; } = null!;

    public virtual Usuario IdusuarioNavigation { get; set; } = null!;
}
