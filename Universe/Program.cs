// See https://aka.ms/new-console-template for more information
using Microsoft.Data.SqlClient;
using System.Data.Common;
using Universe.Common.Models;
using Universe.Common.Repositories;
using Universe.DAL.ADO.Repositories;

Console.WriteLine("Demo universe");

string connectionString = "Data Source=PC05\\DEVOPSDB;Initial Catalog=UniverseDB;Integrated Security=True;Trust Server Certificate=True;Database=UniverseDB";

//TODO  Tester la DB via ADO
using (DbConnection dbConnection = new SqlConnection(connectionString))
{
    dbConnection.Open();
    IGalaxyRepository galaxyRepository = new GalaxyRepository(dbConnection);
    IStarRepository starRepository = new StarRepository(dbConnection);
    IPlanetRepository planetRepository = new PlanetRepository(dbConnection);

    //galaxyRepository.Create(new Galaxy { Name = "Voie Lactée", Description = "Home sweet home" });
    IEnumerable<Galaxy> galaxies = galaxyRepository.GetAll();

    foreach (Galaxy galaxy in galaxies)
    {
        Console.WriteLine($"Galaxy: {galaxy.Id} - {galaxy.Name} - {galaxy.Description}");
    }

    //Console.WriteLine("Ajouter une etoile dans la galaxie Voie Lactée");
    Galaxy voieLactee =galaxyRepository.GetById(1) ?? throw new Exception("Galaxy not found");
    Star sun = starRepository.Create(new Star { Name = "Soleil",IsDeath = false, GalaxyId =voieLactee.Id });

    //Console.WriteLine($" - Etoile {sun.Name} crée");

    Planet p1 = planetRepository.Create(new Planet { Name = "Terre", Satelite = 1 , Gravity = 9.81  });
    Planet p2 = planetRepository.Create(new Planet { Name = "Mercure", Satelite = 0, Gravity = 3.7 });
    Planet p3 = planetRepository.Create(new Planet { Name = "Pluton", Satelite = 5, Gravity = 0.625 });
    Planet p4 = planetRepository.Create(new Planet { Name = "Saturne", Satelite = 274, Gravity = 10.44 });

    Console.WriteLine($"Planets created: {p1.Name}, {p2.Name}, {p3.Name}, {p4.Name}");

    Console.WriteLine($"Ajouter le lien entre les planetes et son etoile");
    starRepository.AddPlanet(sun.Id,p1.Id);

    starRepository.AddPlanets(sun.Id, new List<int> { p2.Id, p3.Id, p4.Id });
}




//TODO  Tester la DB via EF Core
