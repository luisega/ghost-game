using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace GhostGame.Tests
{
    [TestClass]
    public class TreeNodeTests
    {
        [TestMethod]
        public void TreeNodeCanBeCreatedWithAWord()
        {
            string testWord = "test";

            TreeNode node = new TreeNode(testWord);

            Assert.AreEqual(testWord.First(), node.Value);
            Assert.IsTrue(node.Children.Count == 1);
            Assert.IsFalse(node.IsLeafNode);
        }

        [TestMethod]
        public void TreeNodeCanBeCreatedWithAWordWithOnlyOneCharacter()
        {
            string testWord = "t";

            TreeNode node = new TreeNode(testWord);

            Assert.AreEqual(testWord.First(), node.Value);
            Assert.IsTrue(node.Children.Count == 0);
            Assert.IsTrue(node.IsLeafNode);
        }

        [TestMethod]
        public void AddWordAddsANewChildToTheNodeIfItsSecondCharacterIsNotRegisteredYet()
        {
            string testWord = "test";
            string newWord = "today";

            TreeNode node = new TreeNode(testWord);
            bool result = node.AddWord(newWord);

            Assert.IsTrue(result);
            Assert.AreEqual(testWord.First(), node.Value);
            Assert.IsTrue(node.Children.Count == 2);
            Assert.IsFalse(node.IsLeafNode);
        }
    }
}