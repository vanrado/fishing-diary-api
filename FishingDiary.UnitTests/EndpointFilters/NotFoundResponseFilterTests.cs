using FishingDiary.API.EndpointFilters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Logging;
using Moq;

namespace FishingDiary.UnitTests;

public class NotFoundResponseFilterTests
{
    private readonly NotFoundResponseFilter _filter;
    private readonly Mock<ILogger<NotFoundResponseFilter>> _mockLogger;

    public NotFoundResponseFilterTests()
    {
        _mockLogger = new Mock<ILogger<NotFoundResponseFilter>>();
        _filter = new NotFoundResponseFilter(_mockLogger.Object);
    }

    [Fact]
    public async Task InvokeAsync_LogsInformation_WhenStatusCodeIsNotFound()
    {
        // Arrange
        var httpContext = new DefaultHttpContext();
        var context = new Mock<EndpointFilterInvocationContext>();
        context.Setup(c => c.HttpContext).Returns(httpContext);
        var next = new Mock<EndpointFilterDelegate>();
        next.Setup(n => n(context.Object)).ReturnsAsync(TypedResults.NotFound());

        // Act
        await _filter.InvokeAsync(context.Object, next.Object);

        // Assert
        _mockLogger.Verify(
               x => x.Log(
                   LogLevel.Information,
                   It.IsAny<EventId>(),
                   It.IsAny<It.IsAnyType>(),
                   It.IsAny<Exception>(),
                   (Func<It.IsAnyType, Exception, string>)It.IsAny<object>()),
               Times.Once);
    }

    [Fact]
    public async Task InvokeAsync_DoesNotLogInformation_WhenStatusCodeIsNotNotFound()
    {
        // Arrange
        var context = new Mock<EndpointFilterInvocationContext>();
        var next = new Mock<EndpointFilterDelegate>();
        next.Setup(n => n(context.Object)).ReturnsAsync(TypedResults.Ok());

        // Act
        await _filter.InvokeAsync(context.Object, next.Object);

        // Assert
        _mockLogger.Verify(
               x => x.Log(
                   LogLevel.Information,
                   It.IsAny<EventId>(),
                   It.IsAny<It.IsAnyType>(),
                   It.IsAny<Exception>(),
                   (Func<It.IsAnyType, Exception, string>)It.IsAny<object>()),
               Times.Never);
    }
}
