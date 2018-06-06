using System.Collections.Generic;

namespace GhostGame
{
    public interface IWordsProvider
    {
        Dictionary<char, TreeNode> CreateNodesFromDictionaryFile(string dictionaryRoute);
    }
}
