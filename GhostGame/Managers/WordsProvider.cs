using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace GhostGame
{
    public class WordsProvider: IWordsProvider
    {
        private readonly int _minimumWordLength;

        public WordsProvider(IConfiguration configuration)
        {
            int.TryParse(configuration["MinimumWordLength"], out int minimumWordLength);
            _minimumWordLength = minimumWordLength < 4 ? 4 : minimumWordLength;
        }

        public IDictionary<char, TreeNode> CreateNodesFromDictionaryFile(string dictionaryRoute)
        {
            string word;
            Dictionary<char, TreeNode> treeNodesRead = new Dictionary<char, TreeNode>();

            StreamReader file = new StreamReader(dictionaryRoute);
            while ((word = file.ReadLine()) != null)
            {
                if (word.Length < _minimumWordLength)
                {
                    continue;
                }

                char wordFirstCharacter = word.First();

                if (treeNodesRead.ContainsKey(wordFirstCharacter))
                {
                    treeNodesRead[wordFirstCharacter].AddWord(word);
                    continue;
                }
                treeNodesRead.Add(wordFirstCharacter, new TreeNode(word));
            }

            return treeNodesRead;
        }
    }
}
