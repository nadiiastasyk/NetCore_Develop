using Fiction_DZ6.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace FictionTests
{
    public class UploadImageServiceTests
    {
        [Fact]
        public void ExecuteAsync_Success_UploadImageCalledOnce()
        {
            // Arrange
            var memorySTream = new MemoryStream();
            Mock<IFormFile> file = new Mock<IFormFile>(MockBehavior.Strict);
            file.Setup(x => x.OpenReadStream()).Returns(memorySTream);
            file.Setup(x => x.FileName).Returns("TestFileName");
            file.Setup(x => x.Length).Returns(memorySTream.Length);
            file.Setup(x => x.Name).Returns("ImageName.png");        
            var outputFile = file.Object;

            Mock<IExternalImageServiceClient> externalImageServiceClient = new Mock<IExternalImageServiceClient>(MockBehavior.Strict);
            Mock<IProcessingChannel> processingChannel = new Mock<IProcessingChannel>();
            processingChannel.Setup(x => x.TryRead(out outputFile)).Returns(true);

            UploadImageService sut = new UploadImageService(externalImageServiceClient.Object, processingChannel.Object);
            Type sutType = typeof(UploadImageService);
            MethodInfo executeAsyncMethodInfo = sutType.GetMethod("ExecuteAsync", BindingFlags.NonPublic | BindingFlags.Instance);

            // Act
            executeAsyncMethodInfo.Invoke(sut, new object[] { CancellationToken.None });

            // Assert
            externalImageServiceClient.Verify(x => x.UploadImage(file.Object), Times.Once);
        }

        [Fact]
        public void ExecuteAsync_Fail_UploadImageNeverCalled()
        {
            // Arrange
            var memorySTream = new MemoryStream();
            Mock<IFormFile> file = new Mock<IFormFile>(MockBehavior.Strict);
            file.Setup(x => x.OpenReadStream()).Returns(memorySTream);
            file.Setup(x => x.FileName).Returns("TestFileName");
            file.Setup(x => x.Length).Returns(memorySTream.Length);
            file.Setup(x => x.Name).Returns("ImageName.png");
            var outputFile = file.Object;

            Mock<IExternalImageServiceClient> externalImageServiceClient = new Mock<IExternalImageServiceClient>(MockBehavior.Strict);
            Mock<IProcessingChannel> processingChannel = new Mock<IProcessingChannel>();
            processingChannel.Setup(x => x.TryRead(out outputFile)).Returns(false);

            UploadImageService sut = new UploadImageService(externalImageServiceClient.Object, processingChannel.Object);
            Type sutType = typeof(UploadImageService);
            MethodInfo executeAsyncMethodInfo = sutType.GetMethod("ExecuteAsync", BindingFlags.NonPublic | BindingFlags.Instance);

            // Act
            executeAsyncMethodInfo.Invoke(sut, new object[] { CancellationToken.None });

            // Assert
            externalImageServiceClient.Verify(x => x.UploadImage(file.Object), Times.Never);
        }

        [Fact]
        public void ExecuteAsync_Fail_CancellationTokenCalled_Cancelled()
        {
            // Arrange
            var memorySTream = new MemoryStream();
            Mock<IFormFile> file = new Mock<IFormFile>(MockBehavior.Strict);
            file.Setup(x => x.OpenReadStream()).Returns(memorySTream);
            file.Setup(x => x.FileName).Returns("TestFileName");
            file.Setup(x => x.Length).Returns(memorySTream.Length);
            file.Setup(x => x.Name).Returns("ImageName.png");
            var outputFile = file.Object;

            var cancellationToken = new CancellationTokenSource();
            cancellationToken.Cancel();

            Mock<IExternalImageServiceClient> externalImageServiceClient = new Mock<IExternalImageServiceClient>(MockBehavior.Strict);
            //externalImageServiceClient.Setup(x => x.UploadImage(It.IsAny<CancellationToken>())).Returns())
            Mock<IProcessingChannel> processingChannel = new Mock<IProcessingChannel>();
            processingChannel.Setup(x => x.TryRead(out outputFile)).Returns(true);

            UploadImageService sut = new UploadImageService(externalImageServiceClient.Object, processingChannel.Object);
            Type sutType = typeof(UploadImageService);
            MethodInfo executeAsyncMethodInfo = sutType.GetMethod("ExecuteAsync", BindingFlags.NonPublic | BindingFlags.Instance);

            // Act
            executeAsyncMethodInfo.Invoke(sut, new object[] { cancellationToken.Token });

            // Assert
            externalImageServiceClient.Verify(x => x.UploadImage(file.Object), Times.Never);
          }
    }
}
