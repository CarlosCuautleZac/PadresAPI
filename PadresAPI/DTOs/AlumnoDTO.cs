namespace PadresAPI.DTOs
{
    public class AlumnoDTO
    {
        public int Id { get; set; }
        public string Grado { get; set; } = "";
        public string Seccion { get; set; } = "";
        public List<AsingaturaDTO> Asingaturas { get; set; } = new();
    }

    public class AsingaturaDTO
    {
        public int Id { get; set; }
        public string NombreAsignatura { get; set; } = null!;
        public string NombreDocente = null!;
        public List<CalificacionDTO> Calificaciones { get; set; } = new();
    }

    public class CalificacionDTO
    {
        public short Año { get; set; }
        public double Calificacion { get; set; }
        public int Unidad { get; set; }
    }

}
