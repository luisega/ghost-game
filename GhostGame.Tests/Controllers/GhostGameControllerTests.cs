using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;

namespace GhostGame.Tests
{
    [TestClass]
    public class GhostGameControllerTests
    {
        private const string TestWord = "test";

        [TestMethod]
        public void GhostGameControllerCanBeCreatedUsingItsConstructor()
        {
            IWordTreeManager manager = new Mock<IWordTreeManager>().Object;

            GhostGameController controller = new GhostGameController(manager);

            Assert.IsNotNull(controller);
        }

        [TestMethod]
        public void ProcessPlayerMovementReturnsOkResponseWithAGhostGameResponseDto()
        {
            Mock<IWordTreeManager> managerMock = new Mock<IWordTreeManager>();
            managerMock.Setup(x => x.GetNextMovement(TestWord))
                .Returns(new GhostGameResponseDto {GameStatus = GameStatus.Playing});

            GhostGameController controller = new GhostGameController(managerMock.Object);
            IActionResult actionResult = controller.ProcessPlayerMovement(new GhostGameRequestDto { CurrentWord = TestWord });

            ObjectResult objectResult = actionResult as OkObjectResult;
            Assert.IsNotNull(objectResult);

            GhostGameResponseDto content = objectResult.Value as GhostGameResponseDto;
            Assert.IsNotNull(content);

            Assert.AreEqual(GameStatus.Playing, content.GameStatus);
            Assert.AreEqual(200, objectResult.StatusCode);
        }

        [TestMethod]
        public void ProcessPlayerMovementReturnsInternalServerErrorResponseIfAnUnexpectedExceptionHappens()
        {
            Mock<IWordTreeManager> managerMock = new Mock<IWordTreeManager>();
            managerMock.Setup(x => x.GetNextMovement(TestWord)).Throws(new Exception());

            GhostGameController controller = new GhostGameController(managerMock.Object);
            IActionResult actionResult = controller.ProcessPlayerMovement(new GhostGameRequestDto { CurrentWord = TestWord });

            ObjectResult objectResult = actionResult as ObjectResult;
            Assert.IsNotNull(objectResult);

            Assert.AreEqual(500, objectResult.StatusCode);
        }

        [TestMethod]
        public void ResetGameReturnsOkResponseWithTrue()
        {
            Mock<IWordTreeManager> managerMock = new Mock<IWordTreeManager>();
            managerMock.Setup(x => x.ResetGame()).Returns(true);

            GhostGameController controller = new GhostGameController(managerMock.Object);
            IActionResult actionResult = controller.ResetGame();

            ObjectResult objectResult = actionResult as OkObjectResult;
            Assert.IsNotNull(objectResult);

            Assert.IsTrue(Convert.ToBoolean(objectResult.Value));
            Assert.AreEqual(200, objectResult.StatusCode);
        }

        [TestMethod]
        public void ResetGameReturnsInternalServerErrorResponseIfAnUnexpectedExceptionHappens()
        {
            Mock<IWordTreeManager> managerMock = new Mock<IWordTreeManager>();
            managerMock.Setup(x => x.ResetGame()).Throws(new Exception());

            GhostGameController controller = new GhostGameController(managerMock.Object);
            IActionResult actionResult = controller.ResetGame();

            ObjectResult objectResult = actionResult as ObjectResult;
            Assert.IsNotNull(objectResult);

            Assert.AreEqual(500, objectResult.StatusCode);
        }
    }
}
