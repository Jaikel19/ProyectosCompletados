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
    public class ImpresoraManager
    {
        private readonly IMongoCollection<Impresora> _ImpresoraCollection;

        public ImpresoraManager(MiMongoDB _database)
        {
            _ImpresoraCollection = _database.GetCollection<Impresora>("Impresoras");
        }

        public IEnumerable<Impresora> GetAllImpresoras()
        {
            return _ImpresoraCollection.Find(FilterDefinition<Impresora>.Empty).ToList();
        }

        public Impresora GetImpresoraById(string ImpresoraId)
        {
            var filter = Builders<Impresora>.Filter.Eq("Id", ImpresoraId);
            return _ImpresoraCollection.Find(filter).FirstOrDefault();
        }

        public IEnumerable<Impresora> GetImpresorasByLocalidad(string localidad)
        {
            var filter = Builders<Impresora>.Filter.Eq("localidad", localidad);
            return _ImpresoraCollection.Find(filter).ToList();
        }

        public void RegistrarImpresora(Impresora nuevoImpresora)
        {
            _ImpresoraCollection.InsertOne(nuevoImpresora);
        }

        public void ActualizarImpresora(string ImpresoraId, Impresora ImpresoraActualizado)
        {
            var filter = Builders<Impresora>.Filter.Eq("Id", ImpresoraId);
            var update = Builders<Impresora>.Update
                .Set("IP", ImpresoraActualizado.IP)
                .Set("Marca", ImpresoraActualizado.Marca)
                .Set("Modelo", ImpresoraActualizado.Modelo)
                .Set("Serie", ImpresoraActualizado.Serie)
                .Set("Localidad", ImpresoraActualizado.Localidad);

            _ImpresoraCollection.UpdateOne(filter, update);
        }

        public void EliminarImpresora(string impresoraId)
        {
            var filter = Builders<Impresora>.Filter.Eq("Id", impresoraId);
            _ImpresoraCollection.DeleteOne(filter);
        }

        public long ContarImpresoras()
        {
            return _ImpresoraCollection.CountDocuments(FilterDefinition<Impresora>.Empty);
        }
    }

}