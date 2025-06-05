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
    public class GalaxyRepository : RepositoryBase,IGalaxyRepository
    {

        public GalaxyRepository(DbConnection dbConnection) : base(dbConnection)
        {
        }

        public bool AddStar(int galaxyId, Star star)
        {
            throw new NotImplementedException();
        }

        public Galaxy Create(Galaxy model)
        {
            using DbCommand cmd = _dbConnection.CreateCommand();
            cmd.CommandText = "INSERT INTO Galaxy (Name, Description) OUTPUT [inserted].* VALUES (@Name, @Description);";
            AddCommandParameters(cmd, "@Name", model.Name);
            AddCommandParameters(cmd, "@Description", model.Description);
            using DbDataReader reader = cmd.ExecuteReader();
            if (!reader.Read())
            {
                throw new DataException("Can't create Galaxy record");
            }
            return MapToGalaxy(reader);

        }

        public IEnumerable<Galaxy> GetAll()
        {
            using DbCommand cmd = _dbConnection.CreateCommand();
            cmd.CommandText = "SELECT * FROM [Galaxy];";
            using DbDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                yield return MapToGalaxy(reader);
            }
        }

        public Galaxy MapToGalaxy(IDataRecord record)
        {
            return new Galaxy()
            {
                Id = record.GetInt32(record.GetOrdinal("Id")),
                Name = record.GetString(record.GetOrdinal("Name")),
                Description = record.GetString(record.GetOrdinal("Description"))

            };
        }

        public Galaxy? GetById(int id)
        {
            using DbCommand cmd = _dbConnection.CreateCommand();
            cmd.CommandText = "SELECT * FROM [Galaxy] WHERE Id = @Id;";
            AddCommandParameters(cmd, "@Id", id);

            using DbDataReader reader = cmd.ExecuteReader();

            Galaxy? data = null;
            if (reader.Read())
            {
                data = MapToGalaxy(reader);
            }

            if (reader.Read())
            {
                throw new Exception("More than one galaxy found with the same id.");
            }
            return data;
        }

        public bool Update(int id, Galaxy model)
        {
            using DbTransaction transaction = _dbConnection.BeginTransaction();
            using DbCommand cmd = _dbConnection.CreateCommand();
            cmd.Transaction = transaction;
            cmd.CommandText = "UPDATE [Galaxy] SET Name = @Name, Description=@Description WHERE Id = @Id;";
            AddCommandParameters(cmd, "@Id", id);
            AddCommandParameters(cmd, "@Name", model.Name);
            AddCommandParameters(cmd, "@Description", model.Description);

            int rowsAffected = cmd.ExecuteNonQuery();
            if (rowsAffected > 1)
            {
                transaction.Rollback();

                throw new Exception("More than one galaxy updated with the same id.");  
            }
            transaction.Commit();
            return rowsAffected == 1;
        }
        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

    }
}
