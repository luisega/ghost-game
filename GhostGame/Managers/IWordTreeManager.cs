using System.Collections.Generic;

namespace GhostGame
{
    public interface IWordTreeManager
    {
        GhostGameResponseDto GetNextMovement(string currentWord);

        bool ResetGame();
    }
}
