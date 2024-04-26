using DataAccess;
using DTO;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppLogic
{
    public class DepartamentoManager
    {
        private readonly IMongoCollection<Departamento> _departamentoCollection;

        public DepartamentoManager(MiMongoDB database)
        {
            _departamentoCollection = database.GetCollection<Departamento>("Departamentos");
        }

        public IEnumerable<Departamento> GetAllDepartamentos()
        {
            return _departamentoCollection.Find(FilterDefinition<Departamento>.Empty).ToList();
        }

        public Departamento GetDepartamentoById(string departamentoId)
        {
            var filter = Builders<Departamento>.Filter.Eq("Id", new ObjectId(departamentoId));
            return _departamentoCollection.Find(filter).FirstOrDefault();
        }


        public IEnumerable<Computadora> GetComputadorasByDepartamentoId(string departamentoId)
        {
            var filter = Builders<Departamento>.Filter.Eq("Id", new ObjectId(departamentoId));
            var departamento = _departamentoCollection.Find(filter).FirstOrDefault();

            return departamento?.Lista_de_computadoras ?? Enumerable.Empty<Computadora>();
        }
        public IEnumerable<Celular> GetCelularesByDepartamentoId(string departamentoId)
        {
            var filter = Builders<Departamento>.Filter.Eq("Id", new ObjectId(departamentoId));
            var departamento = _departamentoCollection.Find(filter).FirstOrDefault();

            return departamento?.Lista_de_celulares ?? Enumerable.Empty<Celular>();
        }
        public IEnumerable<Impresora> GetImpresorasByDepartamentoId(string departamentoId)
        {
            var filter = Builders<Departamento>.Filter.Eq("Id", new ObjectId(departamentoId));
            var departamento = _departamentoCollection.Find(filter).FirstOrDefault();

            return departamento?.Lista_de_impresoras ?? Enumerable.Empty<Impresora>();
        }

        public void AddDepartamento(Departamento departamento)
        {
            _departamentoCollection.InsertOne(departamento);
        }

        public void UpdateDepartamento(string departamentoId, Departamento departamento)
        {
            var filter = Builders<Departamento>.Filter.Eq("Id", new ObjectId(departamentoId));
            var update = Builders<Departamento>.Update
                .Set(d => d.Nombre_departamento, departamento.Nombre_departamento)
                .Set(d => d.Lista_de_computadoras, departamento.Lista_de_computadoras)
                .Set(d => d.Lista_de_impresoras, departamento.Lista_de_impresoras)
                .Set(d => d.Lista_de_celulares, departamento.Lista_de_celulares);

            _departamentoCollection.UpdateOne(filter, update);
        }
    }
}
