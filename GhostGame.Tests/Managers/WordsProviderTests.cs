using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;

namespace GhostGame.Tests
{
    [TestClass]
    public class WordsProviderTests
    {
        private static IConfiguration _configurationMock;

        [ClassInitialize]
        public static void ClassInitialize(TestContext testContext)
        {
            var configurationMockSetup = new Mock<IConfiguration>();
            configurationMockSetup.Setup(x => x["MinimumWordLength"]).Returns("4");
            _configurationMock = configurationMockSetup.Object;
        }

        [TestMethod]
        public void WordsProviderCanBeCreatedUsingItsConstructorWhenMinimumLengthIsGreaterThan4()
        {
            WordsProvider provider = new WordsProvider(_configurationMock);

            Assert.IsNotNull(provider);
            Assert.AreEqual(4, provider.MinimumWordLength);
        }

        [TestMethod]
        public void WordsProviderCanBeCreatedUsingItsConstructorWhenMinimumLengthIsLowerThan4()
        {
            var configurationMockSetup = new Mock<IConfiguration>();
            configurationMockSetup.Setup(x => x["MinimumWordLength"]).Returns("1");

            WordsProvider provider = new WordsProvider(configurationMockSetup.Object);

            Assert.IsNotNull(provider);
            Assert.AreEqual(4, provider.MinimumWordLength);
        }

        [TestMethod]
        public void CreateNodesFromDictionaryFileReturnsEmptyDictionaryIfFileCouldNotBeLoaded()
        {
            WordsProvider provider = new WordsProvider(_configurationMock);
            Dictionary<char, TreeNode> results = provider.CreateNodesFromDictionaryFile("fakeFile.txt");

            Assert.IsNotNull(results);
            Assert.IsTrue(!results.Any());
        }
    }
}
