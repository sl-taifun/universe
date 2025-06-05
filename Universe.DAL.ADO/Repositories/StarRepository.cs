using Dapper;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Universe.Common.Models;
using Universe.Common.Repositories;

namespace Universe.DAL.ADO.Repositories
{
    public class StarRepository : RepositoryBase, IStarRepository
    {
        public StarRepository(DbConnection dbConnection) : base(dbConnection)
        {
        }

        public bool AddPlanet(int starId, Planet planet)
        {
            throw new NotImplementedException();
        }

        public bool AddPlanets(int starId, IEnumerable<Planet> planets)
        {
            throw new NotImplementedException();
        }

        public Star Create(Star model)
        {
            string cmd = "INSERT INTO [Star] ([Name], [IsDeath]) OUTPUT [inserted].* VALUES (@Name, @IsDeath);";
            return  _dbConnection.QuerySingle<Star>(cmd, model);
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Star> GetAll()
        {
           string cmd = "SELECT * FROM [Star];";
           return _dbConnection.Query<Star>(cmd);
        }

        public Star? GetById(int id)
        {
            string cmd = "SELECT * FROM [Star] WHERE [Id] = @Id;";
            return _dbConnection.QuerySingleOrDefault<Star>(cmd, new { Id = id });
        }
        public bool Update(int id, Star model)
        {
            using DbTransaction transaction = _dbConnection.BeginTransaction();
            string sql = "UPDATE [Star] SET [Name] = @Name, [IsDeath] = @IsDeath WHERE [Id] = @Id;";
            int rowsAffected = _dbConnection.Execute(sql, new { model.Name, model.IsDeath, Id = id }, transaction);
            if (rowsAffected > 1)
            {
                transaction.Rollback();
                throw new Exception("More than one star updated with the same id.");
            }
            transaction.Commit();
            return rowsAffected ==1;
        }

        public IEnumerable<Planet> GetPlanetsByStarId(int starId)
        {
            throw new NotImplementedException();
        }

        public Star? GetStarByName(string name)
        {
            throw new NotImplementedException();
        }

        public bool RemovePlanet(int starId, int planetId)
        {
            throw new NotImplementedException();
        }

        public bool UpdateStar(Star star)
        {
            throw new NotImplementedException();
        }
    }
}
