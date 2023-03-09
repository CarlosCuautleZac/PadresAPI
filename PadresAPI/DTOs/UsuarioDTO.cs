namespace PadresAPI.DTOs
{
    public class UsuarioDTO
    {
        public int Id { get; set; }
        public string NombreUsuario { get; set; } = "";
        public string Password { get; set; } = "";
        public int Rol { get; set; }
    }
}
