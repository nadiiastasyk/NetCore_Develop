using Fiction_DZ6.Infrastructure;
using Fiction_DZ6.Services;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace FictionTests
{
    public class LoadImageServiceTests
    {
        [Fact]
        public void ExecuteAsync_Success_ImageLoadedToCache()
        {
            // Assert
            Mock<IExternalImageServiceClient> externalImageServiceClient = new Mock<IExternalImageServiceClient>(MockBehavior.Strict);
            var expected = new byte[] { 0, 1, 2, 3, 4 };
            string imageName = "Image_name.png";

            IMemoryCache memoryCache = new MemoryCache(new MemoryCacheOptions());
            Mock<ICacheHelper> cacheHelper = new Mock<ICacheHelper>();
            cacheHelper.Setup(x => x.ProcessCache(imageName)).Returns(expected);
            Mock<IOptions<IFictionConfiguration>> configuration = new Mock<IOptions<IFictionConfiguration>>(MockBehavior.Strict);
            configuration.Setup(x => x.Value.ImageName).Returns(imageName);
            LoadImageService sut = new LoadImageService(externalImageServiceClient.Object, cacheHelper.Object, configuration.Object);
            Type sutType = typeof(LoadImageService);
            MethodInfo executeAsyncMethodInfo = sutType.GetMethod("ExecuteAsync", BindingFlags.NonPublic | BindingFlags.Instance);

            // Act
            executeAsyncMethodInfo.Invoke(sut, new object[] { CancellationToken.None });

            // Assert
            externalImageServiceClient.Verify(x => x.GetImage(), Times.Never);
            cacheHelper.Verify(x => x.ProcessCache(imageName), Times.Once);

            byte[] result = cacheHelper.Object.ProcessCache(imageName);
            Assert.Equal(expected, result);
        }
    }
}
