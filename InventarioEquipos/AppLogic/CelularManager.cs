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
    public class CelularManager
    {
        private readonly IMongoCollection<Celular> _celularCollection;

        private List<IObserver> _observers = new List<IObserver>();

        public void AgregarObserver(IObserver observer)
        {
            _observers.Add(observer);
        }

        public void NotificarObservers(int message)
        {
            foreach (var observer in _observers)
            {
                observer.Update(message);
            }
        }

        // Método para realizar la acción que notificará a los observadores
        public void RealizarAccion()
        {
            // Lógica de la acción...

            // Notificar a los observadores
            NotificarObservers(1);
        }

        public CelularManager(MiMongoDB _database)
        {
            _celularCollection = _database.GetCollection<Celular>("Celulares");
        }

        public IEnumerable<Celular> GetAllCelulares()
        {
            return _celularCollection.Find(FilterDefinition<Celular>.Empty).ToList();
        }

        public Celular GetCelularById(string celularId)
        {
            var filter = Builders<Celular>.Filter.Eq("Id", celularId);
            return _celularCollection.Find(filter).FirstOrDefault();
        }

        public IEnumerable<Celular> GetCelularesByMarca(string marca)
        {
            var filter = Builders<Celular>.Filter.Eq("marca", marca);
            return _celularCollection.Find(filter).ToList();
        }

        public IEnumerable<Celular> GetCelularesByModelo(string modelo)
        {
            var filter = Builders<Celular>.Filter.Eq("modelo", modelo);
            return _celularCollection.Find(filter).ToList();
        }

        public IEnumerable<Celular> GetCelularesBySIM(string sim)
        {
            var filter = Builders<Celular>.Filter.Eq("SIM", sim);
            return _celularCollection.Find(filter).ToList();
        }

        public IEnumerable<Celular> GetCelularesByLocalidad(string localidad)
        {
            var filter = Builders<Celular>.Filter.Eq("localidad", localidad);
            return _celularCollection.Find(filter).ToList();
        }

        public void RegistrarCelular(Celular nuevoCelular)
        {
            _celularCollection.InsertOne(nuevoCelular);
            NotificarObservers(1);
        }

        public void ActualizarCelular(string celularId, Celular celularActualizado)
        {
            var filter = Builders<Celular>.Filter.Eq("Id", celularId);
            var update = Builders<Celular>.Update
                .Set("Marca", celularActualizado.Marca)
                .Set("Modelo", celularActualizado.Modelo)
                .Set("SIM", celularActualizado.SIM)
                .Set("Localidad", celularActualizado.Localidad);

            _celularCollection.UpdateOne(filter, update);
        }
        public void EliminarCelular(string celularId)
        {
            var filter = Builders<Celular>.Filter.Eq("Id", celularId);
            _celularCollection.DeleteOne(filter);
        }

        public long ContarCelulares()
        {
            return _celularCollection.CountDocuments(FilterDefinition<Celular>.Empty);
        }
    }
    public class IteratorConcreto<T> : IIterator<T>
    {
        private readonly List<T> _elementos;
        private int _indice = 0;

        public IteratorConcreto(List<T> elementos)
        {
            _elementos = elementos;
        }

        public T Actual()
        {
            return _elementos[_indice];
        }

        public bool Siguiente()
        {
            _indice++;
            return _indice < _elementos.Count;
        }
    }
}