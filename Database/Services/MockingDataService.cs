using Common.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Services
{
    public static class MockingDataService
    {
        public static List<User> MockCompleteUsers()
        {
            List<User> users = MockUsers();
            List<Club> clubs = MockClubs();
            List<Manager> managers = MockManagers();
            List<Player> players = MockPlayers();
            List<FormationDTO> formations = MockFormations();

            foreach (Club club in clubs)
            {
                club.Manager = managers.First(manager => manager.ClubId == club.Id);
                club.Players = players.Where(player => player.ClubId == club.Id).ToList();
                FormationDTO clubFormation = formations.First(formation => formation.ClubId == club.Id);
                club.Formation = new Formation(clubFormation.Formation, 11, clubFormation.Bench);
            }

            foreach (User user in users)
            {
                user.Club = clubs.First(club => club.OwnerId == user.Id);
            }

            return users;
        }

        public static List<User> MockUsers()
        {
            string currentPath = Directory.GetCurrentDirectory();
            string mockDataDirectoryPath = @$"{currentPath}\MockedData\User_Mock_Data.json";
            string jsonData = File.ReadAllText(mockDataDirectoryPath);

            return JsonConvert.DeserializeObject<List<User>>(jsonData);
        }

        public static List<Club> MockClubs()
        {
            string currentPath = Directory.GetCurrentDirectory();
            string mockDataDirectoryPath = @$"{currentPath}\MockedData\Club_Mock_Data.json";
            string jsonData = File.ReadAllText(mockDataDirectoryPath);

            return JsonConvert.DeserializeObject<List<Club>>(jsonData);
        }

        public static List<Manager> MockManagers()
        {
            string currentPath = Directory.GetCurrentDirectory();
            string mockDataDirectoryPath = @$"{currentPath}\MockedData\Manager_Mock_Data.json";
            string jsonData = File.ReadAllText(mockDataDirectoryPath);

            return JsonConvert.DeserializeObject<List<Manager>>(jsonData);
        }

        public static List<Player> MockPlayers()
        {
            string currentPath = Directory.GetCurrentDirectory();
            string mockDataDirectoryPath = @$"{currentPath}\MockedData\Player_Mock_Data.json";
            string jsonData = File.ReadAllText(mockDataDirectoryPath);

            return JsonConvert.DeserializeObject<List<Player>>(jsonData);
        }

        public static List<FormationDTO> MockFormations()
        {
            string currentPath = Directory.GetCurrentDirectory();
            string mockDataDirectoryPath = @$"{currentPath}\MockedData\Formation_Mock_Data.json";
            string jsonData = File.ReadAllText(mockDataDirectoryPath);

            return JsonConvert.DeserializeObject<List<FormationDTO>>(jsonData);
        }
    }
}
