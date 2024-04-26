using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AppLogic;
using DTO;
using Microsoft.AspNetCore.Cors;
using System.Net.Http.Json;

namespace SmartBitEventos.Controllers
{
    //adding CORS
    [EnableCors("MyCorsPolicy")]
    //Adding Action
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        [HttpGet]
        public List<Evento> GetEventValue()
        {
            EventManager em = new EventManager();

            return em.GetAllEvents();
        }


        [HttpGet]
        public List<Evento> GetEventsByUser(int? IdUsuario)
        {
            EventManager em = new EventManager();
            List<Evento> eventos = em.GetEventsByUser(IdUsuario);

            try
            {
                eventos.ForEach(eventos =>
                {
                    eventos.TipoBoletoEventos = GetBoletoEvento(eventos.Id);
                });

                return eventos;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                throw;
            }
 
        }
        [HttpGet]
        public List<TipoBoletoEvento> GetBoletoByEventoId(int IdEvento)
        {
            EventManager em = new EventManager();
            List<TipoBoletoEvento> tipoBoletoEvento = em.GetBoletoByEventoId(IdEvento);

           return tipoBoletoEvento;
        }

 

        [HttpGet]
        public List<Evento> GetEventsByName(string Nombre)
        {
            EventManager em = new EventManager();
            List<Evento> eventos = em.GetEventsByName(Nombre);

            try
            {
                eventos.ForEach(eventos =>
                {
                    eventos.TipoBoletoEventos = GetBoletoEvento(eventos.Id);
                });

                return eventos;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                throw;
            }

        }

        [HttpGet]
        public List<Evento> GetEventsByID(int IdEvento)
        {
            EventManager em = new EventManager();
            List<Evento> eventos = em.GetEventsById(IdEvento);

            try
            {
                eventos.ForEach(eventos =>
                {
                    eventos.TipoBoletoEventos = GetBoletoEvento(eventos.Id);
                    eventos.EventoAsientos = this.GetAsientoPorUsuario(eventos.Id);
                });

                return eventos;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                throw;
            }

        }

        [HttpGet]
        public List<AsientoPorUsuario> GetAsientoPorUsuario(int IdEvento)
        {
            return GetAsientos(IdEvento);
        }

        private List<AsientoPorUsuario> GetAsientos(int IdEvento)
        {
            EventManager em = new EventManager();
            List<AsientoPorUsuario> eventoAsiento = em.GetAsientoPorUsuario(IdEvento);

            return eventoAsiento;
        }

        [HttpGet]
        public List<TipoBoletoEvento> GetBoletoEvento(int IdEvento)
        {
            EventManager em = new EventManager();
            return em.GetBoletoEvento(IdEvento);
        }


        [HttpGet]
        public List<TipoBoletoEvento> GetTipoBoleto()
        {
            EventManager em = new EventManager();
            return em.GetBoletos();
        }


        [HttpGet]
        public List<DetallesBoleto> DetallesBoleto(int IdBoleto)
        {
            EventManager em = new EventManager();
            return em.GetDetalleBoleto(IdBoleto);
        }

        [HttpPost]
        public Evento CreateEvento(Evento evento)
        {
            EventManager em = new EventManager();
            em.CreateEvent(evento);
            return evento;
        }

        [HttpPost]
        public NuevoBoleto CreateBoletos(Boleto boleto)
        {
            EventManager em = new EventManager();
            var res = em.CreateBoletos(boleto);
            
            return res;
        }

        [HttpPost]
        public string ChangeBoletoState(Boleto boleto)
        {
            EventManager em = new EventManager();
            var res = em.ChangeBoletoState(boleto.Id);
            return res;
        }
    }
}
