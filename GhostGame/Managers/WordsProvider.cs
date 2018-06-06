using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace GhostGame
{
    public class WordsProvider: IWordsProvider
    {
        internal int MinimumWordLength { get; }

        public WordsProvider(IConfiguration configuration)
        {
            int.TryParse(configuration["MinimumWordLength"], out int minimumWordLength);
            MinimumWordLength = minimumWordLength < 4 ? 4 : minimumWordLength;
        }

        public Dictionary<char, TreeNode> CreateNodesFromDictionaryFile(string dictionaryRoute)
        {
            string word;
            StreamReader file;
            Dictionary<char, TreeNode> treeNodesRead = new Dictionary<char, TreeNode>();
            string routeToWordlistFile = string.IsNullOrEmpty(dictionaryRoute) ? 
                "wordlist.txt" : dictionaryRoute;

            try
            {
                file = new StreamReader(routeToWordlistFile);
            }
            catch (IOException)
            {
                return new Dictionary<char, TreeNode>();
            }

            while ((word = file.ReadLine()) != null)
            {
                if (word.Length < MinimumWordLength)
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
