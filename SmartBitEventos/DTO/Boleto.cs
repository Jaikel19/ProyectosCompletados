namespace DTO
{
    public class Boleto
    {
        public Boleto()
        {
            TipoBoletosPorUsuario = new List<TipoBoletoPorUsuario>();
            asientosPorBoleto = new List<AsientoPorUsuario>();
        }

        public int Id { get; set; }
        public bool Estado { get; set; }
        public int IdEvento { get; set; }
        public int IdUsuario { get; set; }
        public List<TipoBoletoPorUsuario> TipoBoletosPorUsuario { get; set; }
        public List<AsientoPorUsuario> asientosPorBoleto { get; set; }
    }
}
