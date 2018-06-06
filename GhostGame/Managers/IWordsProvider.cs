using System.Collections.Generic;

namespace GhostGame
{
    public interface IWordsProvider
    {
        IDictionary<char, TreeNode> CreateNodesFromDictionaryFile(string dictionaryRoute);
    }
}
