using LandC_Final_Project.DataLayer.Repository;
using LandC_Final_Project.Entity;
using LandC_Final_Project.Enum;
using LandC_Final_Project.Model;

namespace LandC_Final_Project.ServiceLayer
{
    public class TeamService : TeamRepository
    {
        private readonly TeamRepository _repository;
        public TeamService()
        {
            _repository = new TeamRepository();
        }
        public Model.Team CreateTeam(int startIndex, int TeamSize, Game Game)
        {            
            string TeamName = "Team " + TeamNameStartIndex++.ToString();
            Model.Team Team = new Model.Team();
            Team.TeamId = TeamId++;
            Team.TeamName = TeamName;
            Team.GameType = (int)Game.GameType;
            List<Model.Player> Players = new List<Model.Player>();
            for (int playerIndex = startIndex; playerIndex < TeamSize; playerIndex++)
            {
                if (string.IsNullOrEmpty(Game.Players[playerIndex].Name))
                {
                    throw new Exception("Player name cannot be null. Please provide the correct data");
                }
                Players.Add(Game.Players[playerIndex]);

            }
            Team.Players = Players;
            return Team;
        }
        int TeamId = 1;
        int TotalPlayersInTeam = 0;
        int TotalPlayersCount = 0;
        int TotalTeams = 0;
        static char TeamNameStartIndex = 'A';
        public TeamList CreateTeamForDifferentGames(Game Game, int TotalPlayers)
        {
            TeamList teamLists = new TeamList();
            teamLists.Teams = new List<Model.Team>();
            Model.Team team = new Model.Team();
            TotalPlayersCount = Game.Players.Count();
            TotalPlayersInTeam = TotalPlayers;
            if (TotalPlayersCount % TotalPlayersInTeam == 0)
            {
                int start = 0;
                TotalTeams = TotalPlayersCount / TotalPlayersInTeam;
                teamLists.TotalTeams = TotalTeams;
                while (TotalTeams != 0)
                {
                    team = CreateTeam(start, TotalPlayersInTeam + start, Game);
                    teamLists.Teams.Add(team);
                    start = start + TotalPlayersInTeam;
                    TotalTeams--;
                }
            }
            else
            {
                throw new Exception("Insufficient player data. Player required for team creation are less");
            }
            return teamLists;
        }
        public TeamList CreateTeamList(Game Game)
        {
            int TotalPlayers = 0;
            if (Game.GameType.Equals(Enum.GameType.GameId.Badminton))
            {
                TotalPlayers = (int)TotalPlayer.TotalPlayers.Badminton;
                TeamList teamList = CreateTeamForDifferentGames(Game, TotalPlayers);
                return teamList;
            }
            else if (Game.GameType.Equals(Enum.GameType.GameId.Chess))
            {
                TotalPlayers = (int)TotalPlayer.TotalPlayers.Chess;
                TeamList teamList = CreateTeamForDifferentGames(Game, TotalPlayers);
                return teamList;
            }
            else if (Game.GameType.Equals(Enum.GameType.GameId.Cricket))
            {
                TotalPlayers = (int)TotalPlayer.TotalPlayers.Cricket;
                TeamList teamList = CreateTeamForDifferentGames(Game, TotalPlayers);
                return teamList;
            }
            return new TeamList();
        }
        public void SavePlayer(Game Game)
        {
            _repository.SavePlayer(Game);
        }
        public void SaveGameType(Game Game)
        {
            _repository.SaveGameType(Game);
        }
        public void SaveTeam(List<Model.Team> Teams, Game Game)
        {
            _repository.SaveTeam(Teams, Game);
        }
        public void SaveTeamPlayer(TeamList TeamList)
        {
            _repository.SaveTeamPlayer(TeamList);
        }
        public TeamList GetTeams(int gameId)
        {
            return _repository.GetTeams(gameId);
        }
    }
}
