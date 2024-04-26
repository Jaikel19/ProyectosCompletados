using DataAccess;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppLogic
{
    public class UsuarioManager
    {
        private readonly MiMongoDB _database;

        public UsuarioManager(MiMongoDB database)
        {
            _database = database;
        }

        public Usuario AutenticarUsuario(string username, string password)
        {
            return _database.AutenticarUsuario(username, password);
        }
    }
}

