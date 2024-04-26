using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class Departamento
    {

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("nombre_departamento")]
        public string Nombre_departamento { get; set; }

        [BsonElement("lista_de_computadoras")]
        public List<Computadora> Lista_de_computadoras { get; set; }

        [BsonElement("lista_de_impresoras")]
        public List<Impresora> Lista_de_impresoras { get; set; }

        [BsonElement("lista_de_celulares")]
        public List<Celular> Lista_de_celulares { get; set; }
    }
}
