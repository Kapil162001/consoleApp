using LandC_Final_Project.Model;
using LandC_Final_Project.ServiceLayer;
public class TeamController
{
    private readonly TeamService _teamService;
    public TeamController()
    {
        _teamService = new TeamService();
    }
    public TeamList TeamCreation(Game game)
    {
        var TeamList = _teamService.CreateTeamList(game);
        _teamService.SavePlayer(game);
        _teamService.SaveGameType(game);
        _teamService.SaveTeam(TeamList.Teams, game);
        _teamService.SaveTeamPlayer(TeamList);
        return _teamService.GetTeams((int)game.GameType);
    }
}
