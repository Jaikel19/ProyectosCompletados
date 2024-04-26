using DataAccess.Dao;
using DataAccess.Mapper;
using DTO;
//using Newtonsoft.Json;

namespace AppLogic
{
    public class EventManager
    {
        private SQLDao _sql;
        public EventManager()
        {

            _sql = new SQLDao();
        }
        public List<Evento> GetAllEvents()
        {
            var table = _sql.GetInformation(new List<Parameters>(), "spGetEvents");

            if (table != null)
            {
                List<Evento> res = ConvertToTable.BuildTable<Evento>(table);

                return res;
            }
            else
            {
                return null;
            }
        }
        public Evento CreateEvent(Evento evento)
        {
            try
            {
                List<Parameters> parameters = new List<Parameters>();

                parameters.Add(new Parameters("Nombre", evento.Nombre));
                parameters.Add(new Parameters("Descripcion", evento.Descripcion));
                parameters.Add(new Parameters("EventoImagen", evento.EventoImagen));
                parameters.Add(new Parameters("Slogan", evento.Slogan));
                parameters.Add(new Parameters("Modalidad", evento.Modalidad));
                parameters.Add(new Parameters("Fecha", evento.Fecha));
                parameters.Add(new Parameters("Hora", evento.Hora));
                parameters.Add(new Parameters("ContactoOrganizador", evento.ContactoOrganizador));
                parameters.Add(new Parameters("Restricciones", evento.Restricciones));
                parameters.Add(new Parameters("Escenario", evento.Escenario));
                parameters.Add(new Parameters("Capacidad", evento.Capacidad));
                parameters.Add(new Parameters("TipoBoleto", ConvertToTable.ToDataTable(evento.TipoBoletoEventos.Select(x => new
                {
                    TipoboletoId = x.Id,
                    Costo = x.Precio,
                    Cantidad = x.Cantidad,
                    Cortesia = x.Cortesia
                }).ToList())));

                parameters.Add(new Parameters("IdUsuario", evento.IdUsuario));
                parameters.Add(new Parameters("Direccion", evento.Direccion));

                var table = _sql.GetInformation(parameters, "spCreateEvento");


                return null;
            }
            catch (Exception ex)
            {

                string errorMessage = ex.Message;
                throw;
            }
        }

        public List<Evento> GetEventsByUser(int? IdUsuario)
        {
            List<Parameters> parameters = new List<Parameters>();
            parameters.Add(new Parameters("IdUsuario", IdUsuario));

            var table = _sql.GetInformation(parameters, "spGetEventByUser");

            if (table != null)
            {
                List<Evento> res = ConvertToTable.BuildTable<Evento>(table);

                return res;
            }
            else
            {
                return null;
            }

        }


        public List<AsientoPorUsuario> GetAsientoPorUsuario(int IdEvento)
        {
            List<Parameters> parameters = new List<Parameters>();
            parameters.Add(new Parameters("IdEvento", IdEvento));

            var table = _sql.GetInformation(parameters, "spGetAsientoPorUsuario");

            if (table != null)
            {
                List<AsientoPorUsuario> res = ConvertToTable.BuildTable<AsientoPorUsuario>(table);

                return res;
            }
            else
            {
                return null;
            }

        }

        public List<Evento> GetEventsById(int IdEvento)
        {
            List<Parameters> parameters = new List<Parameters>();
            parameters.Add(new Parameters("IdEvento", IdEvento));

            var table = _sql.GetInformation(parameters, "spGetEventById");

            if (table != null)
            {
                List<Evento> res = ConvertToTable.BuildTable<Evento>(table);

                return res;
            }
            else
            {
                return null;
            }

        }

        public List<TipoBoletoEvento> GetBoletoByEventoId(int IdEvento)
        {
            List<Parameters> parameters = new List<Parameters>();
            parameters.Add(new Parameters("IdEvento", IdEvento));

            var table = _sql.GetInformation(parameters, "GetBoletoByEventoId");

            if (table != null)
            {
                List<TipoBoletoEvento> res = ConvertToTable.BuildTable<TipoBoletoEvento>(table);

                return res;
            }
            else
            {
                return null;
            }

        }


        public List<Evento> GetEventsByName(string Nombre)
        {
            List<Parameters> parameters = new List<Parameters>();
            parameters.Add(new Parameters("Nombre", Nombre));

            var table = _sql.GetInformation(parameters, "spGetEventByName");

            if (table != null)
            {
                List<Evento> res = ConvertToTable.BuildTable<Evento>(table);

                return res;
            }
            else
            {
                return null;
            }

        }


        public List<TipoBoletoEvento> GetBoletoEvento(int IdEvento)
        {
            List<Parameters> parameters = new List<Parameters>();
            parameters.Add(new Parameters("IdEvento", IdEvento));

            var table = _sql.GetInformation(parameters, "spGetBoletosEvento");

            if (table != null)
            {
                List<TipoBoletoEvento> res = ConvertToTable.BuildTable<TipoBoletoEvento>(table);

                return res;
            }
            else
            {
                return null;
            }

        }

        public List<TipoBoletoEvento> GetBoletos()
        {
            List<Parameters> parameters = new List<Parameters>();
            var table = _sql.GetInformation(parameters, "spTipoBoleto");

            List<TipoBoletoEvento> res = ConvertToTable.BuildTable<TipoBoletoEvento>(table);

            return res;
        }

        public List<DetallesBoleto> GetDetalleBoleto(int IdBoleto)
        {
            List<Parameters> parameters = new List<Parameters>();
            parameters.Add(new Parameters("IdBoleto", IdBoleto));
            var table = _sql.GetInformation(parameters, "spGetDetalleBoleto");

            List<DetallesBoleto> res = ConvertToTable.BuildTable<DetallesBoleto>(table);

            return res;
        }

        public NuevoBoleto CreateBoletos(Boleto boleto)
        {
            try
            {
                List<Parameters> parameters = new List<Parameters>();

                parameters.Add(new Parameters("Estado", boleto.Estado));
                parameters.Add(new Parameters("IdEvento", boleto.IdEvento));
                parameters.Add(new Parameters("IdUsuario", boleto.IdUsuario));
                parameters.Add(new Parameters("TipoBoleto", ConvertToTable.ToDataTable(boleto.TipoBoletosPorUsuario.Select(x => new
                {
                    TipoboletoId = x.IdTipoBoleto,
                    Costo = x.Precio,
                    Cantidad = x.Cantidad,
                    Cortesia = false
                }).ToList())));

                parameters.Add(new Parameters("Asientos", ConvertToTable.ToDataTable(boleto.asientosPorBoleto.Select(x => new
                {
                    IdEvento = x.IdEvento,
                    IdTipoBoleto = x.IdTipoBoleto,
                    Asiento = x.Asiento                    
                }).ToList())));


                var table = _sql.GetInformation(parameters, "spCreateBoleto");

                List<NuevoBoleto> res = ConvertToTable.BuildTable<NuevoBoleto>(table);
                return res.FirstOrDefault();
            }
            catch (Exception ex)
            {

                string errorMessage = ex.Message;
                throw;
            }
        }
        public string ChangeBoletoState(int boletoId)
        {
            try
            {
                var parametros = new List<Parameters>
        {
            new Parameters("BoletoId", boletoId)
        };

                _sql.ExecuteProcedure(parametros, "spUpdateBoletoEstado");

                return "Estado del boleto actualizado";
            }
            catch (Exception ex)
            {
                return "Error al actualizar el estado del boleto: " + ex.Message;
            }
        }

    }
}