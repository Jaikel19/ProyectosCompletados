using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
using MongoDB.Driver;
using DTO;

namespace DataAccess
{
    public class MiMongoDB
    {
        public MongoClient _client;
        public IMongoDatabase _database;

        public MiMongoDB()
        {
            _client = new MongoClient("mongodb://localhost:27017");
            _database = _client.GetDatabase("Proyecto_PriceSmart");
        }

        public IMongoCollection<Celular> CelularesCollection => _database.GetCollection<Celular>("Celulares");

        public IMongoCollection<Usuario> UsuariosCollection => _database.GetCollection<Usuario>("Usuarios");

        public Usuario AutenticarUsuario(string username, string password)
        {
            var filter = Builders<Usuario>.Filter.Eq("Username", username) & Builders<Usuario>.Filter.Eq("Password", password);
            return UsuariosCollection.Find(filter).FirstOrDefault();
        }

        public IMongoCollection<T> GetCollection<T>(string collectionName)
        {
            return _database.GetCollection<T>(collectionName);
        }
    }
}
