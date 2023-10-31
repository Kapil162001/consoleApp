using LandC_Final_Project.DataLayer.DataContext;
using LandC_Final_Project.Entity;
using LandC_Final_Project.Model;


namespace LandC_Final_Project.DataLayer.Repository
{
    public class TeamRepository
    {
        private readonly LC_WorkshopContext _context;
        public TeamRepository()
        {
            _context = new LC_WorkshopContext();
        }
        public void SavePlayer(Game game)
        {
            for (int playerIndex = 0; playerIndex < game.Players.Count(); playerIndex++)
            {
                Entity.Player player = new Entity.Player();
                player.PlayerName = game.Players[playerIndex].Name;
                player.PlayerId = game.Players[playerIndex].PlayerId;
                _context.Players.Add(player);
                Save();
            }
        }
        public void SaveGameType(Game game)
        {
            Entity.GameType gameType = new Entity.GameType();
            gameType.GameTypeId = (int)game.GameType;
            gameType.GameName = game.GameType.ToString();
            gameType.NoOfPlayers = game.Players.Count();
            _context.GameTypes.Add(gameType);
            Save();
        }
        public void SaveTeam(List<Model.Team> teams, Game game)
        {
            for (int playerIndex = 0; playerIndex < teams.Count(); playerIndex++)
            {
                Entity.Team Team = new Entity.Team();
                Team.TeamId = teams[playerIndex].TeamId;
                Team.Name = teams[playerIndex].TeamName;
                Team.EventId = 1;
                Team.GameId = (int)game.GameType;
                _context.Teams.Add(Team);
                Save();
            }
        }
        public void SaveTeamPlayer(TeamList teamList)
        {
            TeamPlayer teamPlayer = new TeamPlayer();
            int id = 0;
            foreach (var teamIndex in teamList.Teams)
            {
                teamPlayer.TeamId = teamIndex.TeamId;
                foreach (var playerIndex in teamIndex.Players)
                {
                    teamPlayer.Id = ++id;
                    teamPlayer.PlayerId = playerIndex.PlayerId;
                    _context.TeamPlayers.Add(teamPlayer);
                    Save();
                }
            }
        }
        public TeamList GetTeams(int gameId)
        {
            var teams = (from teamPlayer in _context.TeamPlayers
                         join team in _context.Teams on teamPlayer.TeamId equals team.TeamId
                         join player in _context.Players on teamPlayer.PlayerId equals player.PlayerId
                         where team.GameId == gameId
                         select new
                         {
                             team.TeamId,
                             team.Name,
                             team.GameId,
                             player.PlayerId,
                             player.PlayerName
                         }).ToList();
            var teamGroup = teams.GroupBy(teamIndex => teamIndex.TeamId)
                 .Select(teamGroupIndex => new
                 {
                     TeamId = teamGroupIndex.Key,
                     teamGroupIndex.FirstOrDefault().Name,
                     teamGroupIndex.FirstOrDefault().GameId,
                     Players = teamGroupIndex.Select(playerIndex => new
                     {
                         playerIndex.PlayerId,
                         playerIndex.PlayerName
                     }).ToList()
                 }).ToList();
            TeamList teamLists = new TeamList();
            teamLists.TotalTeams = teamGroup.Count();
            teamLists.Teams = new List<Model.Team>();
            foreach (var teamItem in teamGroup)
            {
                Model.Team team = new Model.Team();
                team.Players = new List<Model.Player>();
                team.GameType = teamItem.GameId;
                team.TeamId = teamItem.TeamId;
                team.TeamName = teamItem.Name;
                foreach (var playerItem in teamItem.Players)
                {
                    Model.Player player = new Model.Player();
                    player.PlayerId = playerItem.PlayerId;
                    player.Name = playerItem.PlayerName;
                    team.Players.Add(player);
                }
                teamLists.Teams.Add(team);
            }
            return teamLists;
        }

        public void Save()
        {
            try
            {
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error while saving data to database : {ex.Message}");
            }
        }
    }
}
