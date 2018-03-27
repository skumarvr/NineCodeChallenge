using System.Collections.Generic;
using System.Web.Http.Results;
using NUnit.Framework;
using Moq;
using NineCodingChallenge.Business.Assessment;
using NineCodingChallenge.Web.Controllers;
using NineCodingChallenge.Models.Assessment;
using NineCodingChallenge.Web.Utils;

namespace NineCodingChallenge.UnitTest.Controllers
{
    [TestFixture]
    public class SerialControllerTest
    {
        private Mock<ISerialManager> _serialManager;
        private SerialController controller;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            _serialManager = new Mock<ISerialManager>();
        }

        [SetUp]
        public void Setup()
        {
            controller = new SerialController(_serialManager.Object) {};
        }

        /// <summary>
        /// Test case to check null Request Json
        /// </summary>
        [Test]
        public void FilterRequestJsonTest_NullInput()
        {            
            // Act
            var responseObj = controller.FilterJson(null);
            
            // Assert
            Assert.IsInstanceOf(typeof(CustomBadRequest), responseObj);
        }

        /// <summary>
        /// Test case to check null Payload object within Request Json
        /// </summary>
        [Test]
        public void FilterRequestJsonTest_PayloadNullInput()
        {
            // Act
            var responseObj = controller.FilterJson(new RequestPayload() { Payload = null });

            // Assert
            Assert.IsInstanceOf(typeof(CustomBadRequest), responseObj);
        }

        /// <summary>
        /// Test case to check not null Request json
        /// </summary>
        [Test]
        public void FilterRequestJsonTest_ValidInput()
        {
            var respPayLoad = new ResponsePayload();
            respPayLoad.Response = new List<SerialDetails>() {
                new SerialDetails() {
                    Image = "http://mybeautifulcatchupservice.com/img/shows/16KidsandCounting1280.jpg",
                    Slug = "show/16kidsandcounting",
                    Title = "16 Kids and Counting"
                }};

            // Arrange
            _serialManager.Setup(x => x.FilterSerialJson(It.IsAny<RequestPayload>())).Returns(respPayLoad);

            // Act
            var responseObj = controller.FilterJson(new RequestPayload() { Payload = new List<Serial>() });

            // Assert
            Assert.IsInstanceOf(typeof(OkNegotiatedContentResult<ResponsePayload>), responseObj);
        }
    }
}
