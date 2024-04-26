using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
using DTO;
using DataAccess;

namespace AppLogic
{
    public class ComputadoraManager
    {
        private readonly IMongoCollection<Computadora> _computadoraCollection;

        public ComputadoraManager(MiMongoDB _database)
        {
            _computadoraCollection = _database.GetCollection<Computadora>("Computadoras");
        }

        public IEnumerable<Computadora> GetAllComputadoras()
        {
            return _computadoraCollection.Find(FilterDefinition<Computadora>.Empty).ToList();
        }

        public Computadora GetComputadoraById(string computadoraId)
        {
            var filter = Builders<Computadora>.Filter.Eq("Id", computadoraId);
            return _computadoraCollection.Find(filter).FirstOrDefault();
        }

        public void RegistrarComputadora(Computadora nuevoComputadora)
        {
            _computadoraCollection.InsertOne(nuevoComputadora);
        }

        public void ActualizarComputadora(string computadoraId, Computadora computadoraActualizado)
        {
            var filter = Builders<Computadora>.Filter.Eq("Id", computadoraId);
            var update = Builders<Computadora>.Update
                .Set("Nombre_del_equipo", computadoraActualizado.Nombre_del_equipo)
                .Set("Etiqueta_de_servicio", computadoraActualizado.Etiqueta_de_servicio)
                .Set("Modelo", computadoraActualizado.Modelo)
                .Set("Procesador", computadoraActualizado.Procesador)
                .Set("Memoria_ram", computadoraActualizado.Memoria_ram)
                .Set("Sistema_operativo", computadoraActualizado.Sistema_operativo)
                .Set("Tipo_de_Office", computadoraActualizado.Tipo_de_Office)
                .Set("Localidad", computadoraActualizado.Localidad)
                .Set("Fecha_del_modelo", computadoraActualizado.Fecha_del_modelo);

            _computadoraCollection.UpdateOne(filter, update);
        }


    }
}