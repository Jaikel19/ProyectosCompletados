namespace DTO
{
    public class BoletosAsignados
    {
        public int Id { get; set; }
        public string NombreEvento { get; set; } = "";
        public string Fecha { get; set; } = "";
        public string Hora { get; set; } = "";
        public int Cantidad { get; set; }
        public Double Precio { get; set; }
        public string TipoBoleto { get; set; } = "";
        public string Estado { get; set; } = "";  
    }


}
