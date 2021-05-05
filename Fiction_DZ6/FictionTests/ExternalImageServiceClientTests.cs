using Fiction_DZ6.Infrastructure;
using Fiction_DZ6.Services;
using Microsoft.Extensions.Options;
using Moq;
using RestSharp;
using System;
using Xunit;

namespace FictionTests
{
    public class ExternalImageServiceClientTests
    {
        [Fact]
        public void GetImage_Success_AllRestClientInvocationsCalled()
        {
            // Arrange
            var expected = new byte[0];
            var uriAddress = "http://localhost:63219/";
            Mock<IRestClient> restClient = new Mock<IRestClient>(MockBehavior.Strict);
            restClient.Setup(x => x.Execute(It.IsAny<IRestRequest>())).Returns(new RestResponse { RawBytes = expected });
            restClient.SetupSet(x => x.BaseUrl = new Uri(uriAddress));

            var imageService = new ExternalImageService
            {
                ExternalImageServiceUrl = uriAddress,
                ExternalImageServiceResource = "image",
                ExternalImageServiceQueryParameter = "imageName"
            };

            Mock<IOptions<IFictionConfiguration>> configuration = new Mock<IOptions<IFictionConfiguration>>(MockBehavior.Strict);
            configuration.Setup(x => x.Value.ImageName).Returns("Image_name.png");
            configuration.SetupGet(x => x.Value.ExternalImageService).Returns(imageService);
            var sut = new ExternalImageServiceClient(configuration.Object, restClient.Object);

            // Act
            byte[] result = sut.GetImage();

            // Assert
            Assert.Equal(expected, result);
            restClient.Verify(x => x.Execute(It.IsAny<IRestRequest>()), Times.Once);
            restClient.VerifySet(x => x.BaseUrl = new Uri(uriAddress), Times.Once);
        }

        [Fact]
        public void GetImage_RestClientFailed_ExceptionThrown()
        {
            // Arrange
            var expected = new byte[0];
            var uriAddress = "http://localhost:63219/";
            Mock<IRestClient> restClient = new Mock<IRestClient>(MockBehavior.Strict);
            restClient.Setup(x => x.Execute(It.IsAny<IRestRequest>())).Throws(new Exception());
            restClient.SetupSet(x => x.BaseUrl = new Uri(uriAddress));

            var imageService = new ExternalImageService
            {
                ExternalImageServiceUrl = uriAddress,
                ExternalImageServiceResource = "image",
                ExternalImageServiceQueryParameter = "imageName"
            };

            Mock<IOptions<IFictionConfiguration>> configuration = new Mock<IOptions<IFictionConfiguration>>(MockBehavior.Strict);
            configuration.Setup(x => x.Value.ImageName).Returns("Image_name.png");
            configuration.SetupGet(x => x.Value.ExternalImageService).Returns(imageService);
            var sut = new ExternalImageServiceClient(configuration.Object, restClient.Object);

            // Act & Assert
            Assert.Throws<Exception>(() => sut.GetImage());
        }
    }
}
