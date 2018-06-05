using System;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace GhostGame
{
    public class WordTreeManager : IWordTreeManager
    {
        private const string WordNotFoundMessage = "The selected word does not exist in the dictionary";
        private const string WordFinishedByComputerMessage = "The selected word has been finished by the server";
        private const string WordFinishedByUserMessage = "The selected word has been finished by the user";

        private readonly IDictionary<char, TreeNode> _parentNodes;

        public TreeNode LastSelectedNode { get; set; }

        public WordTreeManager(IConfiguration configuration)
        {
            _parentNodes = CreateNodesFromDictionaryFile(configuration["DictionaryFileLocation"]);
        }

        public GhostGameResponseDto GetNextMovement(string currentWord)
        {
            TreeNode currentNode = GetCurrentNode(currentWord);

            if (currentNode == null)
            {
                return new GhostGameResponseDto
                {
                    GameStatus = GameStatus.ComputerWon,
                    Message = WordNotFoundMessage
                };
            }
            if (currentNode.IsLeafNode)
            {
                return new GhostGameResponseDto
                {
                    GameStatus = GameStatus.ComputerWon,
                    Message = WordFinishedByUserMessage
                };
            }

            TreeNode nextNode = GetNextNode(currentNode);
            LastSelectedNode = nextNode;

            if (nextNode.IsLeafNode)
            {
                return new GhostGameResponseDto
                {
                    GameStatus = GameStatus.HumanPlayerWon,
                    NextLetter = nextNode.Value,
                    Message = WordFinishedByComputerMessage

                };
            }

            return new GhostGameResponseDto
            {
                GameStatus = GameStatus.Playing,
                NextLetter = nextNode.Value
            };
        }

        public bool ResetGame()
        {
            LastSelectedNode = null;
            return true;
        }

        internal TreeNode GetNextNode(TreeNode currentNode)
        {
            List<TreeNode> winningMovements = GetWinningNodes(currentNode).ToList();

            return winningMovements.Any() ? winningMovements.PickRandomElement() :
                GetMaximumLengthNodes(currentNode).PickRandomElement();
        }

        internal IEnumerable<TreeNode> GetWinningNodes(TreeNode node)
        {
            List<TreeNode> winningNodes = new List<TreeNode>();

            foreach (TreeNode childNode in node.Children.Values)
            {
                if (childNode.IsLeafNode)
                {
                    continue;
                }

                foreach (TreeNode grandChildNode in childNode.Children.Values)
                {
                    if (grandChildNode.IsLeafNode || GetWinningNodes(grandChildNode).Any())
                    {
                        winningNodes.Add(childNode);
                    }
                }
            }

            return winningNodes;
        }

        internal IEnumerable<TreeNode> GetMaximumLengthNodes(TreeNode node)
        {
            Dictionary<TreeNode, int> childNodesLength = new Dictionary<TreeNode, int>();

            foreach (TreeNode childNode in node.Children.Values)
            {
                childNodesLength.Add(childNode, childNode.GetMaximumWordLengthReachable());
            }

            return childNodesLength
                .Where(x => x.Value == childNodesLength.Values.Max())
                .Select(y => y.Key);
        }

        internal TreeNode GetCurrentNode(string currentWord)
        {
            TreeNode currentNode;

            if (LastSelectedNode == null)
            {
                _parentNodes.TryGetValue(currentWord.First(), out currentNode);
            }
            else
            {
                LastSelectedNode.Children.TryGetValue(currentWord.Last(), out currentNode);
            }

            return currentNode;
        }

        #region Private helpers

        private static IDictionary<char, TreeNode> CreateNodesFromDictionaryFile(string dictionaryRoute)
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

        #endregion

    }
}