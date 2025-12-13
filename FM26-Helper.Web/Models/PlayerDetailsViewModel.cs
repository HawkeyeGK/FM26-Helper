using FM26_Helper.Shared;
using Microsoft.Extensions.Configuration;
using System.Linq;

namespace FM26_Helper.Web.Models
{
    public class PlayerDetailsViewModel
    {
        private readonly RosterRepository _rosterRepository;
        private readonly IConfiguration _configuration;

        public PlayerImportData? Player { get; private set; }
        public PlayerAnalysis? Analysis { get; private set; }
        public RosterItemViewModel? HeaderData { get; private set; }

        public PlayerDetailsViewModel(RosterRepository rosterRepository, IConfiguration configuration)
        {
            _rosterRepository = rosterRepository;
            _configuration = configuration;
        }


        public void LoadPlayer(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                Player = null;
                return;
            }

            var path = _configuration["RosterFilePath"];
            if (string.IsNullOrEmpty(path))
            {
                return;
            }

            var allPlayers = _rosterRepository.Load(path);
            Player = allPlayers.FirstOrDefault(p => p.PlayerName.Equals(name, System.StringComparison.OrdinalIgnoreCase));

            if (Player != null)
            {
                if (Player.Snapshot != null)
                {
                    Analysis = PlayerAnalyzer.Analyze(Player.Snapshot);
                    HeaderData = RosterItemViewModel.FromPlayer(Player);
                }
            }
        }
    }
}
