using FishingDiary.API.EndpointFilters;
using FishingDiary.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Moq;

namespace FishingDiary.UnitTests;

public class ValidateAnnotationsFilterTests
{
    private readonly ValidateAnnotationsFilter _filter;

    public ValidateAnnotationsFilterTests()
    {
        _filter = new ValidateAnnotationsFilter();
    }

    [Fact]
    public async Task InvokeAsync_ReturnsNextResult_WhenDtoIsValid()
    {
        // Arrange
        var context = new Mock<EndpointFilterInvocationContext>();
        context.Setup(c => c.GetArgument<FisheryForCreationDto>(2)).Returns(new FisheryForCreationDto { Title = "Test" });
        var next = new Mock<EndpointFilterDelegate>();
        next.Setup(n => n(context.Object)).ReturnsAsync(TypedResults.Ok());

        // Act
        var result = await _filter.InvokeAsync(context.Object, next.Object);

        // Assert
        Assert.NotNull(result);
    }

    [Fact]
    public async Task InvokeAsync_ReturnsValidationProblem_WhenDtoIsInvalid()
    {
        // Arrange
        var context = new Mock<EndpointFilterInvocationContext>();
        context.Setup(c => c.GetArgument<FisheryForCreationDto>(2)).Returns(new FisheryForCreationDto { Title = "1" });
        var next = new Mock<EndpointFilterDelegate>();
        next.Setup(n => n(context.Object)).ReturnsAsync(TypedResults.NotFound());

        // Act
        var result = await _filter.InvokeAsync(context.Object, next.Object);

        // Assert
        Assert.IsType<ValidationProblem>(result);
    }
}