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

        public bool AddPlanet(int starId, int planetId)
        {
            string cmd = "INSERT INTO [Rel__Star_Planet]([StarId],[PlanetId]) VALUES (@StarId, @PlanetId)";
            int rowsAffected = _dbConnection.Execute(cmd, new 
            { 
                StarId = starId, 
                PlanetId = planetId 
            });

            return rowsAffected == 1;
        }

        public bool AddPlanets2(int starId, IEnumerable<int> planetIds)
        {
            const string sql = "INSERT INTO Rel__Star_Planet (StarId, PlanetId) VALUES (@StarId, @PlanetId)";

            var parameters = planetIds.Select(planetId => new
            {
                StarId = starId,
                PlanetId = planetId
            });

            try
            {
                _dbConnection.Execute(sql, parameters);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding planets: {ex.Message}");
                return false;
            }
        }

        public bool AddPlanets(int starId, IEnumerable<int> planetIds)
        {
            var currentPlanetIds = planetIds.ToList();
            var sqlInputQuery = new List<string>();
            var parameters = new DynamicParameters();

            for (int i = 0; i < currentPlanetIds.Count; i++)
            {
                sqlInputQuery.Add($"(@StarId, @PlanetId{i})");
                parameters.Add($"PlanetId{i}", currentPlanetIds[i]);
            }

            parameters.Add("@StarId", starId);

            string sql = $"INSERT INTO Rel__Star_Planet (StarId, PlanetId) VALUES {string.Join(", ", sqlInputQuery)}";

            try
            {
                _dbConnection.Execute(sql, parameters);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding planets: {ex.Message}");
                return false;
            }
        }
        public bool AddPlanets4(int starId, IEnumerable<int> planetIds)
        {
           

            List<int> currentPlanetId = planetIds.ToList();
            List<string> sqlInputQuery = new List<string>();
            Dictionary<string,int> sqlInputValue = new Dictionary<string, int>();

            for(int i = 0; i < currentPlanetId.Count; i++)
            {
                string parameterName = $"@PlanetId{i}";
                sqlInputQuery.Add($"({starId}, {parameterName})");
                sqlInputValue.Add(parameterName, currentPlanetId[i]);
            }

            string sql = $"INSERT INTO StarPlanet (StarId, PlanetId) VALUES {string.Join(",",sqlInputQuery)}";

            try
            {
                _dbConnection.Execute(sql);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding planets: {ex.Message}");
                return false;
            }
        }

        public Star Create(Star model)
        {
            string cmd = "INSERT INTO [Star] ([Name], [IsDeath], [GalaxyId]) OUTPUT [inserted].* VALUES (@Name, @IsDeath, @GalaxyId);";
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

        public bool RemovePlanet(int starId, int planetId)
        {
            string sql = "DELETE FROM [Rel__Star_Planet] WHERE StarId = @StarId AND PlanetId = @PlanetId";
            int rowsAffected = _dbConnection.Execute(sql, new { StarId = starId, PlanetId = planetId });

            try
            {
                _dbConnection.Execute(sql, new { StarId = starId, PlanetId = planetId });
                return true;
            }
            catch 
            {
                return false;
            }
        }

    }
}
