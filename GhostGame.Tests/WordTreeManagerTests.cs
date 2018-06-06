using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;

namespace GhostGame.Tests
{
    [TestClass]
    public class WordTreeManagerTests
    {
        private const string TestWord = "test";

        private static IConfiguration _configurationMock;
        private static IWordsProvider _providerMock;
        private static Dictionary<char, TreeNode> _dictionary;

        [ClassInitialize]
        public static void ClassInitialize(TestContext testContext)
        {
            var configurationMockSetup = new Mock<IConfiguration>();
            configurationMockSetup.Setup(x => x["DictionaryFileLocation"]).Returns("anywhere");
            _configurationMock = configurationMockSetup.Object;

            TreeNode tNode = new TreeNode(TestWord);
            tNode.AddWord("today");
            tNode.AddWord("termics");
            tNode.AddWord("tomorrow");
            tNode.AddWord("tastings");

            TreeNode gNode = new TreeNode("game");

            _dictionary = new Dictionary<char, TreeNode>
            {
                { 't', tNode },
                { 'g', gNode }
            };

            var providerMockSetup = new Mock<IWordsProvider>();
            providerMockSetup.Setup(x => x.CreateNodesFromDictionaryFile(It.IsAny<string>())).Returns(_dictionary);
            _providerMock = providerMockSetup.Object;
        }

        [TestMethod]
        public void WordTreeManagerCanBeCreatedUsingItsConstructor()
        {
            WordTreeManager manager = new WordTreeManager(_providerMock, _configurationMock);

            Assert.IsNotNull(manager);
        }












        [TestMethod]
        public void ResetGameSetsLastSelectedNodeToNull()
        {
            WordTreeManager manager = new WordTreeManager(_providerMock, _configurationMock);
            manager.LastSelectedNode = manager.GetCurrentNode("t");

            bool reset = manager.ResetGame();

            Assert.IsTrue(reset);
            Assert.IsNull(manager.LastSelectedNode);
        }

        [TestMethod]
        public void GetCurrentNodeReturnsTheParentNodeForTheFirstCharacterIfLastProcessedIsNotDefined()
        {
            WordTreeManager manager = new WordTreeManager(_providerMock, _configurationMock);
            TreeNode currentNode = manager.GetCurrentNode(TestWord);

            Assert.IsNotNull(currentNode);
            Assert.AreEqual(TestWord.First(), currentNode.Value);
            Assert.IsTrue(currentNode.Children.Count == 3);
            Assert.IsFalse(currentNode.IsLeafNode);
        }

        [TestMethod]
        public void GetCurrentNodeReturnsTheLastProcessedNodeChildrenIfLastProcessedIsDefined()
        {
            WordTreeManager manager = new WordTreeManager(_providerMock, _configurationMock)
            {
                LastSelectedNode = _dictionary['t']
            };
            TreeNode currentNode = manager.GetCurrentNode("te");

            Assert.IsNotNull(currentNode);
            Assert.AreEqual('e', currentNode.Value);
            Assert.IsTrue(currentNode.Children.Count == 2);
            Assert.IsFalse(currentNode.IsLeafNode);
        }

        [TestMethod]
        public void GetWinningNodesShouldReturnAllNodesThatLeadToAVictory()
        {
            WordTreeManager manager = new WordTreeManager(_providerMock, _configurationMock);

            List<TreeNode> winningNodes = 
                manager.GetWinningNodes(manager.GetCurrentNode("t")).ToList();

            Assert.IsNotNull(winningNodes.Count == 1);
        }

        [TestMethod]
        public void GetWinningNodesShouldReturnAnEmptyListIfThereAreNoNodesThatLeadToVictory()
        {
            WordTreeManager manager = new WordTreeManager(_providerMock, _configurationMock);

            List<TreeNode> winningNodes =
                manager.GetWinningNodes(manager.GetCurrentNode("g")).ToList();

            Assert.IsNotNull(winningNodes.Count == 0);
        }

        [TestMethod]
        public void GetMaximumLengthNodesReturnsAListOfNodesThatLeadToTheMaximumWordLength()
        {
            WordTreeManager manager = new WordTreeManager(_providerMock, _configurationMock);

            List<TreeNode> maximumLengthNodes =
                manager.GetMaximumLengthNodes(manager.GetCurrentNode("t")).ToList();

            Assert.IsNotNull(maximumLengthNodes.Count == 2);
        }

        [TestMethod]
        public void GetNextNodeShouldReturnTheNextNodeToPlayWhenThereIsWinningOptions()
        {
            WordTreeManager manager = new WordTreeManager(_providerMock, _configurationMock);

            TreeNode nextNode = manager.GetNextNode(manager.GetCurrentNode("t"));

            Assert.IsNotNull(nextNode);
        }

        [TestMethod]
        public void GetNextNodeShouldReturnTheNextNodeToPlayWhenThereIsNoWinningOptions()
        {
            WordTreeManager manager = new WordTreeManager(_providerMock, _configurationMock);

            TreeNode nextNode = manager.GetNextNode(manager.GetCurrentNode("g"));

            Assert.IsNotNull(nextNode);
            Assert.AreEqual('a', nextNode.Value);
        }
    }
}
