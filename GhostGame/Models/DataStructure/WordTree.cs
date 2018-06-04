using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using Microsoft.Extensions.Configuration;

namespace GhostGame
{
    public class WordTree
    {
        IDictionary<char, TreeNode> parentNodes;
        internal readonly IConfiguration Configuration;

        public WordTree(IConfiguration configuration)
        {
            parentNodes = CreateNodesFromDictionaryFile(configuration["DictionaryFileLocation"]);
        }

        private IDictionary<char, TreeNode> CreateNodesFromDictionaryFile(string dictionaryRoute)
        {
            string word;
            Dictionary<char, TreeNode> treeNodesRead = new Dictionary<char, TreeNode>();

            StreamReader file = new StreamReader(dictionaryRoute);
            while ((word = file.ReadLine()) != null)
            {
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