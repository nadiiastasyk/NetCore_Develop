using Fiction_DZ6.Infrastructure;
using Fiction_DZ6.Services;
using Microsoft.Extensions.Caching.Memory;
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
        public void ExecuteAsync_Success_ImageLoadedToChache()
        {
            // Assert
            Mock<IExternalImageServiceClient> externalImageServiceClient = new Mock<IExternalImageServiceClient>(MockBehavior.Strict);
            var expected = new byte[] { 0, 1, 2, 3, 4 };
            externalImageServiceClient.Setup(x => x.GetImage()).Returns(expected);

            IMemoryCache memoryCache = new MemoryCache(new MemoryCacheOptions());
            Mock<IFictionConfiguration> configuration = new Mock<IFictionConfiguration>(MockBehavior.Strict);

            LoadImageService sut = new LoadImageService(externalImageServiceClient.Object, memoryCache, configuration.Object);
            Type sutType = typeof(LoadImageService);
            MethodInfo executeAsyncMethodInfo = sutType.GetMethod("ExecuteAsync", BindingFlags.NonPublic | BindingFlags.Instance);

            // Act
            executeAsyncMethodInfo.Invoke(sut, new object[] { CancellationToken.None });

            // Assert
            var cacheKey = $"Image_name.png_{DateTime.UtcNow.Date}";
            var result = memoryCache.Get<byte[]>(cacheKey);
            Assert.Equal(expected, result);
        }
    }
}
