// using FishingDiary.API.EndpointFilters;

namespace FishingDiary.UnitTests;

public class FisheryIsLockedFilterTests
{
    // private readonly FisheryIsLockedFilter _filter;
    // private readonly Guid _lockedFisheryId;

    // public FisheryIsLockedFilterTests()
    // {
    //     _lockedFisheryId = Guid.NewGuid();
    //     _filter = new FisheryIsLockedFilter(_lockedFisheryId);
    // }

    // [Fact]
    // public async Task InvokeAsync_ReturnsProblem_WhenFisheryIdIsLocked()
    // {
    //     // Arrange
    //     var context = new Mock<EndpointFilterInvocationContext>();
    //     context.Setup(c => c.GetArgument<Guid>(1)).Returns(_lockedFisheryId);

    //     // Act
    //     var result = await _filter.InvokeAsync(context.Object, (ctx) => Task.FromResult(new object()));

    //     // Assert
    //     Assert.IsType<ProblemDetails>(result);
    // }

    // [Fact]
    // public async Task InvokeAsync_ReturnsNextResult_WhenFisheryIdIsNotLocked()
    // {
    //     // Arrange
    //     var context = new Mock<EndpointFilterInvocationContext>();
    //     context.Setup(c => c.GetArgument<Guid>(1)).Returns(Guid.NewGuid());

    //     // Act
    //     var result = await _filter.InvokeAsync(context.Object, (ctx) => Task.FromResult(new object()));

    //     // Assert
    //     Assert.NotNull(result);
    // }
}
