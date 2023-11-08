using FishingDiary.API.EndpointFilters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Moq;

namespace FishingDiary.UnitTests;

public class FisheryIsLockedFilterTests
{
    private readonly FisheryIsLockedFilter _filter;
    private readonly Guid _lockedFisheryId;

    public FisheryIsLockedFilterTests()
    {
        _lockedFisheryId = Guid.NewGuid();
        _filter = new FisheryIsLockedFilter(_lockedFisheryId);
    }

    [Fact]
    public async Task InvokeAsync_ReturnsProblem_WhenFisheryIdIsLocked()
    {
        // Arrange
        var context = new Mock<EndpointFilterInvocationContext>();
        context.Setup(c => c.GetArgument<Guid>(1)).Returns(_lockedFisheryId);
        var endpointFilterDelegate = new Mock<EndpointFilterDelegate>();

        // Act
        var result = await _filter.InvokeAsync(context.Object, endpointFilterDelegate.Object);

        // Assert
        Assert.IsType<ProblemHttpResult>(result);
    }

    [Fact]
    public async Task InvokeAsync_ReturnsNextResult_WhenFisheryIdIsNotLocked()
    {
        // Arrange
        var context = new Mock<EndpointFilterInvocationContext>();
        context.Setup(c => c.GetArgument<Guid>(1)).Returns(Guid.NewGuid());
        var endpointFilterDelegate = new Mock<EndpointFilterDelegate>();
        endpointFilterDelegate.Setup(e => e(context.Object)).ReturnsAsync(TypedResults.Ok());

        // Act
        var result = await _filter.InvokeAsync(context.Object, endpointFilterDelegate.Object);

        // Assert
        Assert.NotNull(result);
    }
}
