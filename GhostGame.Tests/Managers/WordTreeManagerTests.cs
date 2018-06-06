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
            gNode.AddWord("go");
            gNode.AddWord("get");

            TreeNode wNode = new TreeNode("we");

            _dictionary = new Dictionary<char, TreeNode>
            {
                { 't', tNode },
                { 'g', gNode },
                { 'w', wNode }
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
        public void GetNextMovementReturnsAComputerWonResponseIfWordDoesNotExist()
        {
            WordTreeManager manager = new WordTreeManager(_providerMock, _configurationMock);
            GhostGameResponseDto response = manager.GetNextMovement("x");

            Assert.IsNotNull(response);
            Assert.AreEqual(GameStatus.ComputerWon, response.GameStatus);
            Assert.IsFalse(string.IsNullOrEmpty(response.Message));
            Assert.IsFalse(string.IsNullOrEmpty(response.CurrentWord));
        }

        [TestMethod]
        public void GetNextMovementReturnsAComputerWonResponseIfWordCompletesATreeBranch()
        {
            WordTreeManager manager = new WordTreeManager(_providerMock, _configurationMock);
            manager.LastSelectedNode = manager.GetCurrentNode("g");
            GhostGameResponseDto response = manager.GetNextMovement("go");

            Assert.IsNotNull(response);
            Assert.AreEqual(GameStatus.ComputerWon, response.GameStatus);
            Assert.IsFalse(string.IsNullOrEmpty(response.Message));
            Assert.IsFalse(string.IsNullOrEmpty(response.CurrentWord));
        }

        [TestMethod]
        public void GetNextMovementReturnsAHumanWonMessageIfTheOnlyOptionLeftIsEndingAWord()
        {
            WordTreeManager manager = new WordTreeManager(_providerMock, _configurationMock);
            GhostGameResponseDto response = manager.GetNextMovement("w");

            Assert.IsNotNull(response);
            Assert.AreEqual(GameStatus.HumanPlayerWon, response.GameStatus);
            Assert.IsFalse(string.IsNullOrEmpty(response.Message));
            Assert.IsFalse(string.IsNullOrEmpty(response.CurrentWord));
        }

        [TestMethod]
        public void GetNextMovementReturnsAPlayingMessageIfGameContinues()
        {
            WordTreeManager manager = new WordTreeManager(_providerMock, _configurationMock);
            GhostGameResponseDto response = manager.GetNextMovement("t");

            Assert.IsNotNull(response);
            Assert.AreEqual(GameStatus.Playing, response.GameStatus);
            Assert.IsFalse(string.IsNullOrEmpty(response.Message));
            Assert.IsFalse(string.IsNullOrEmpty(response.CurrentWord));
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

            TreeNode nextNode = manager.GetNextNode(manager.GetCurrentNode("w"));

            Assert.IsNotNull(nextNode);
            Assert.AreEqual('e', nextNode.Value);
        }
    }
}
