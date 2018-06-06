using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace GhostGame
{
    public class GhostGameResponseDto
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public GameStatus GameStatus { get; set; }

        public string CurrentWord { get; set; }

        public string Message { get; set; }
    }

    public enum GameStatus
    {
        Playing,

        HumanPlayerWon,

        ComputerWon
    }
}
