using LandC_Final_Project.Enum;

namespace LandC_Final_Project.Model
{
    [Serializable]
    public class Game
    {
        public GameType.GameId GameType;
        public List<Player> Players { get; set; }

    }
}
