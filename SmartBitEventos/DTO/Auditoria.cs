namespace DTO
{
    public class Auditoria
    {
        public int Id { get; set; }
        public string Accion { get; set; } = "";
        public string Descripcion { get; set; } = "";
        public DateTime FechaHora { get; set; }
    }


}
