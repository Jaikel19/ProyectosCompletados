using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class Computadora
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("nombre_del_equipo")]
        public string Nombre_del_equipo { get; set; }

        [BsonElement("etiqueta_de_servicio")]
        public string Etiqueta_de_servicio { get; set; }

        [BsonElement("modelo")]
        public string Modelo { get; set; }

        [BsonElement("procesador")]
        public string Procesador { get; set; }

        [BsonElement("memoria_ram")]
        public string Memoria_ram { get; set; }

        [BsonElement("sistema_operativo")]
        public string Sistema_operativo { get; set; }

        [BsonElement("tipo_de_Office")]
        public string Tipo_de_Office { get; set; }

        [BsonElement("localidad")]
        public string Localidad { get; set; }

        [BsonElement("fecha_del_modelo")]
        public string Fecha_del_modelo { get; set; }
    }
}
