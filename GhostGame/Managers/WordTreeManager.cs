using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;

namespace GhostGame
{
    public class WordTreeManager : IWordTreeManager
    {
        private const string WordNotFoundMessage = "You lost. The selected word does not exist in the dictionary. Press 'Reset game' to try again.";
        private const string WordFinishedByComputerMessage = "You won!!! Press 'Reset game' to start a new game. ";
        private const string WordFinishedByUserMessage = "You lost. You have finished a valid word. Press 'Reset game' to try again.";

        private readonly IDictionary<char, TreeNode> _parentNodes;

        public TreeNode LastSelectedNode { get; set; }

        public WordTreeManager(IWordsProvider wordsProvider, IConfiguration configuration)
        {
            _parentNodes = wordsProvider.CreateNodesFromDictionaryFile(configuration["DictionaryFileLocation"]);
        }

        public GhostGameResponseDto GetNextMovement(string currentWord)
        {
            TreeNode currentNode = GetCurrentNode(currentWord);

            if (currentNode == null)
            {
                return new GhostGameResponseDto
                {
                    GameStatus = GameStatus.ComputerWon,
                    CurrentWord = currentWord,
                    Message = WordNotFoundMessage
                };
            }
            if (currentNode.IsLeafNode)
            {
                return new GhostGameResponseDto
                {
                    GameStatus = GameStatus.ComputerWon,
                    CurrentWord = currentWord,
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
                    CurrentWord = currentWord + nextNode.Value,
                    Message = WordFinishedByComputerMessage
                };
            }

            return new GhostGameResponseDto
            {
                GameStatus = GameStatus.Playing,
                CurrentWord = currentWord + nextNode.Value,
                Message = $"The server selected the character '{nextNode.Value.ToString().ToUpper()}'. Your turn..."
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
    }
}