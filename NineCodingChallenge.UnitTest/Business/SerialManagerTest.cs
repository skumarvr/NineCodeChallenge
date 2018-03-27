using System.Collections.Generic;
using NUnit.Framework;
using NineCodingChallenge.Business.Assessment.Impl;
using NineCodingChallenge.Models.Assessment;

namespace NineCodingChallenge.UnitTest.Business
{
    [TestFixture]
    public class SerialManagerTest
    {
        private SerialManager manager;

        [SetUp]
        public void Setup()
        {
            manager = new SerialManager();
        }

        /// <summary>
        /// Test case to check null Serial Json
        /// </summary>
        [Test]
        public void FilterSerialJsonManagerTest_NullInput()
        {
            // Act
            var responseObj = manager.FilterSerialJson(null);

            // Assert
            Assert.AreEqual(responseObj, null);
        }

        /// <summary>
        /// Test case to filter valid Serial string
        /// </summary>
        [Test]
        public void FilterSerialJsonManagerTest_ValidInput()
        { 
            // Act
            var responseObj = manager.FilterSerialJson(new RequestPayload()
            {
                Payload = new List<Serial>() {
                    new Serial() {
                        Drm = true,
                        EpisodeCount = 3,
                        Image = new Image() {
                            ShowImage = "http://mybeautifulcatchupservice.com/img/shows/16KidsandCounting1280.jpg"
                        },
                        Slug = "show/16kidsandcounting",
                        Title = "16 Kids and Counting"
                    }
                } 
            });

            // Assert
            Assert.AreEqual(responseObj.Response.Count, 1);
        }

        [Test]
        public void FilterSerialJsonManagerTest_ValidInput_DrmIsFalse()
        {
            // Act
            var responseObj = manager.FilterSerialJson(new RequestPayload()
            {
                Payload = new List<Serial>() {
                    new Serial() {
                        Drm = false,
                        EpisodeCount = 3,
                        Image = new Image() {
                            ShowImage = "http://mybeautifulcatchupservice.com/img/shows/16KidsandCounting1280.jpg"
                        },
                        Slug = "show/16kidsandcounting",
                        Title = "16 Kids and Counting"
                    }
                }
            });

            // Assert
            Assert.AreEqual(responseObj.Response.Count, 0);
        }

        [Test]
        public void FilterSerialJsonManagerTest_ValidInput_DrmIsTrue_EpisodeCountIsZero()
        {
            // Act
            var responseObj = manager.FilterSerialJson(new RequestPayload()
            {
                Payload = new List<Serial>() {
                    new Serial() {
                        Drm = true,
                        EpisodeCount = 0,
                        Image = new Image() {
                            ShowImage = "http://mybeautifulcatchupservice.com/img/shows/16KidsandCounting1280.jpg"
                        },
                        Slug = "show/16kidsandcounting",
                        Title = "16 Kids and Counting"
                    }
                }
            });

            // Assert
            Assert.AreEqual(responseObj.Response.Count, 0);
        }

        [Test]
        public void FilterSerialJsonManagerTest_ValidInput_Two()
        {
            var requestPayload = new RequestPayload() {
                Payload = new List<Serial>() {
                    new Serial() {
                        Drm = true,
                        EpisodeCount = 0,
                        Image = new Image() {
                            ShowImage = "http://mybeautifulcatchupservice.com/img/shows/16KidsandCounting1280.jpg"
                        },
                        Slug = "show/16kidsandcounting_0",
                        Title = "16 Kids and Counting : 0"
                    },
                    new Serial() {
                        Drm = true,
                        EpisodeCount = 3,
                        Image = new Image() {
                            ShowImage = "http://mybeautifulcatchupservice.com/img/shows/16KidsandCounting1280.jpg"
                        },
                        Slug = "show/16kidsandcounting_1",
                        Title = "16 Kids and Counting : 1"
                    }
                }
            };


            // Act
            var responseObj = manager.FilterSerialJson(requestPayload);

            // Assert
            Assert.AreEqual(responseObj.Response.Count, 1);
            Assert.AreEqual(requestPayload.Payload[1].Title, responseObj.Response[0].Title);
        }
    }
}
