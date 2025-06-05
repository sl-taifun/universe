using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Universe.Common.Models;
using Universe.Common.Repositories;

namespace Universe.DAL.ADO.Repositories
{
    public class PlanetRepository : RepositoryBase,IPlanetRepository
    {
       

        public PlanetRepository(DbConnection dbConnection)
            : base(dbConnection)
        {
        }
       

        
        public Planet Create(Planet model)
        {
            using DbCommand cmd = _dbConnection.CreateCommand();
            cmd.CommandText = "INSERT INTO [Planet] ([Name], [Satelite] , [Gravity]) OUTPUT [inserted].* VALUES (@Name, @Satelite, @Gravity);";
           
            AddCommandParameters(cmd, "@Name", model.Name);
            AddCommandParameters(cmd, "@Satelite", model.Satelite);
            AddCommandParameters(cmd, "@Gravity", model.Gravity);

            using DbDataReader reader = cmd.ExecuteReader();

            if (!reader.Read())
            {
                throw new Exception("Fail on create planet");
            } 
            return MapToPlanet(reader);

        }

        protected Planet MapToPlanet(IDataRecord record)
        {
            return new Planet()
            {
                Id = record.GetInt32(record.GetOrdinal("Id")),
                Name = record.GetString(record.GetOrdinal("Name")),
                Satelite = (int)record["Satelite"],
                Gravity = Convert.ToDouble(record["Gravity"]),
                Stars = null
            };
        }

        public IEnumerable<Planet> GetAll()
        {
            using DbCommand cmd = _dbConnection.CreateCommand();
            cmd.CommandText = "SELECT * FROM [Planet];";
            using DbDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                yield return MapToPlanet(reader);
            } 
        }

        public Planet? GetById(int id)
        {
            using DbCommand cmd = _dbConnection.CreateCommand();
            cmd.CommandText = "SELECT * FROM [Planet] WHERE Id = @Id;";
            AddCommandParameters(cmd, "@Id", id);

            using DbDataReader reader = cmd.ExecuteReader();

            Planet? data = null;
            if (reader.Read())
            {
                data = MapToPlanet(reader);
            }

            if (reader.Read())
            {
               throw new Exception("More than one planet found with the same id.");
            }
            return data;
        }

        public bool Update(int id, Planet model)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
