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

        [TestMethod]
        public void AddWordAddsANewChildToTheNodeIfItsSecondCharacterIsAlreadyRegistered()
        {
            string testWord = "test";
            string newWord = "team";

            TreeNode node = new TreeNode(testWord);
            bool result = node.AddWord(newWord);

            Assert.IsTrue(result);
            Assert.AreEqual(testWord.First(), node.Value);
            Assert.IsTrue(node.Children.Count == 1);
            Assert.IsTrue(node.Children.First().Value.Children.Count == 2);
            Assert.IsFalse(node.IsLeafNode);
        }

        [TestMethod]
        public void AddWordDoesNotAddANewChildToTheNodeIfItsNotLongerThan2Characters()
        {
            string testWord = "test";
            string newWord = "t";

            TreeNode node = new TreeNode(testWord);
            bool result = node.AddWord(newWord);

            Assert.IsFalse(result);
            Assert.AreEqual(testWord.First(), node.Value);
            Assert.IsTrue(node.Children.Count == 1);
            Assert.IsFalse(node.IsLeafNode);
        }

        [TestMethod]
        public void GetMaximumLengthReachableReturnsChildDepth()
        {
            string testWord = "test";

            TreeNode node = new TreeNode(testWord);
            int result = node.GetMaximumWordLengthReachable();

            Assert.AreEqual(testWord.Length, result);
        }

        [TestMethod]
        public void GetMaximumLengthReachableReturnsLongestChildDepth()
        {
            string testWord = "test";
            string newWord = "tomorrow";

            TreeNode node = new TreeNode(testWord);
            node.AddWord(newWord);
            int result = node.GetMaximumWordLengthReachable();

            Assert.AreEqual(newWord.Length, result);
        }
    }
}
