using System.Collections.Generic;
using System.Linq;

namespace GhostGame
{
    internal class TreeNode
    {
        internal char Value { get; }
        internal Dictionary<char, TreeNode> Children { get; private set; }

        internal bool IsLeafNode { get { return !Children.Any(); } }

        internal TreeNode(string nodeWord)
        {
            Value = nodeWord.FirstOrDefault();
            Children = new Dictionary<char, TreeNode>();

            if (nodeWord.Length > 1)
            {
                string newWord = nodeWord.Substring(1);
                Children.Add(newWord.First(), new TreeNode(newWord));
            }
        }

        internal bool AddWord(string wordToAdd)
        {
            if (wordToAdd.Length < 2)
            {
                return false;
            }

            char wordToAddFirstCharacter = wordToAdd.FirstOrDefault();
            string wordToAddWithoutFirstCharacter = wordToAdd.Substring(1);

            if (Children.TryGetValue(wordToAddFirstCharacter, out TreeNode nextNode))
            {
                nextNode.AddWord(wordToAddWithoutFirstCharacter);
                return true;
            }

            Children.Add(wordToAddFirstCharacter, new TreeNode(wordToAddWithoutFirstCharacter));
            return true;
        }

        internal int GetMaximumWordLengthReachable()
        {
            int maximumLengthReached = 0;

            foreach (TreeNode childNode in Children.Values)
            {
                int childLength = childNode.GetMaximumWordLengthReachable();
                if (maximumLengthReached < childLength)
                {
                    maximumLengthReached = childLength;
                }
            }

            return maximumLengthReached;
        }
    }
}