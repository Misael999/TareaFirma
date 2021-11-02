using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using FirmaBase.Models;
using System.Threading.Tasks;

namespace FirmaBase.Data
{
   public class SQLiteHelper
    {
        SQLiteAsyncConnection db;
        public SQLiteHelper(string dbPath)
        {
            db = new SQLiteAsyncConnection(dbPath);
            db.CreateTableAsync<Persona>().Wait();
        }

        public Task<int> SavePersona(Persona person)
        {
            if (person.Idpersona != 0)
            {
                return db.UpdateAsync(person);
                ;
            }
            else
            {
                return db.InsertAsync(person);
            }
        }
        /// <summary>
        /// Recuperar todos los personas
        /// </summary>
        /// <returns></returns>
        public Task<List<Persona>> GetPersonasAync()
        {
            return db.Table<Persona>().ToListAsync();
        }
        /// <summary>
        /// Recupera las personas por la identidad
        /// </summary>
        /// <param name="identidad">Identidad de la persona requerida</param>
        /// <returns></returns>
        public Task<Persona> GetPersonaByIdAsync(String nomb)
        {
            return db.Table<Persona>().Where(a => a.nombre == nomb).FirstOrDefaultAsync();
        }

        public Task<int> DropPersonaAsync(Persona person)
        {
            return db.DeleteAsync(person);
        }

    }
}
