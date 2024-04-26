namespace DTO
{
    public class LoggedUser
    {
        public LoggedUser()
        {
                
        }
        public LoggedUser(Usuario usuario, List<Accesos> accesos)
        {
            Usuario = usuario;
            Accesos = accesos;
        }
        public Usuario Usuario { get; set; }
        public List<Accesos> Accesos { get; set; }
    }
}
