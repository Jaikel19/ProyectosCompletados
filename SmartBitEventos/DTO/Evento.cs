using System;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DTO
{
    public class Evento
    {
        public Evento()
        {
            TipoBoletoEventos = new List<TipoBoletoEvento>();
            EventoAsientos = new List<AsientoPorUsuario>();
        }
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string EventoImagen { get; set; }
        public string Slogan { get; set; }
        public string Modalidad { get; set; }
        public string Direccion { get; set; }
        public string Fecha { get; set; }
        public string Hora { get; set; }
        public List<TipoBoletoEvento> TipoBoletoEventos { get; set; }
        [Display(Name = "Organizador")]
        public string ContactoOrganizador { get; set; }
        public string Restricciones { get; set; }
        public string Escenario { get; set; }
        public int Capacidad { get; set; }
        public int IdUsuario { get; set; } //fk
        public string Latitud { get; set; }
        public string Longitud { get; set; }
        public string TipoBoletosStr  { get; set; }
        [JsonIgnore]
        public IFormFile Imagen { get; set; }
        public List<AsientoPorUsuario> EventoAsientos { get; set; }
    }
}

