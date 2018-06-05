using System.Collections.Generic;
using System.Linq;

namespace GhostGame
{
    public class TreeNode
    {
        internal char Value { get; }
        internal Dictionary<char, TreeNode> Children { get; }

        internal bool IsLeafNode => !Children.Any();

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

            string wordToAddWithoutFirstCharacter = wordToAdd.Substring(1);
            char wordToAddSecondCharacter = wordToAddWithoutFirstCharacter.FirstOrDefault();

            //if (wordToAddFirstCharacter == Value)
            //{
            //    AddWord(wordToAddWithoutFirstCharacter);
            //    return true;
            //}

            //Children.Add(wordToAddFirstCharacter, new TreeNode(wordToAdd));
            //return true;

            if (Children.TryGetValue(wordToAddSecondCharacter, out TreeNode nextNode))
            {
                nextNode.AddWord(wordToAddWithoutFirstCharacter);
                return true;
            }

            Children.Add(wordToAddSecondCharacter, new TreeNode(wordToAddWithoutFirstCharacter));
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