using System;
using System.Collections.Generic;
using System.Linq;
namespace LandC_Final_Project.Model
{
    [Serializable]
    public class Team
    {
        public int TeamId { get; set; }
        public string TeamName { get; set; }
        public int GameType { get; set; }    
        public List<Player> Players { get; set; }
    }
}
