namespace ApiSample01.Api.Tests.Filters;

using ApiSample01.Api.Filters;
using ApiSample01.Domain.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Routing;
using Moq;

public class ModelValidationFilterTests
{
    [Fact]
    public void OnActionExecuting_WhenModelStateIsValid_ShouldNotSetResult()
    {
        // Arrange
        var filter = new ModelValidationFilter();
        var context = CreateActionExecutingContext();

        // Act
        filter.OnActionExecuting(context);

        // Assert
        Assert.Null(context.Result);
    }

    [Fact]
    public void OnActionExecuting_WhenModelStateIsInvalid_ShouldSetBadRequestResult()
    {
        // Arrange
        var filter = new ModelValidationFilter();
        var context = CreateActionExecutingContext();
        context.ModelState.AddModelError("TestField", "Test error message");

        // Act
        filter.OnActionExecuting(context);

        // Assert
        var result = Assert.IsType<BadRequestObjectResult>(context.Result);
        Assert.Equal(400, result.StatusCode);
    }

    [Fact]
    public void OnException_WhenNotET002Exception_ShouldNotHandleException()
    {
        // Arrange
        var filter = new ModelValidationFilter();
        var context = CreateExceptionContext(new ArgumentException("Test"));

        // Act
        filter.OnException(context);

        // Assert
        Assert.Null(context.Result);
        Assert.False(context.ExceptionHandled);
    }

    [Fact]
    public void OnException_WhenET002Exception_ShouldHandleExceptionAndSetResult()
    {
        // Arrange
        var filter = new ModelValidationFilter();
        var exception = new ET002FieldSizeError("TestField", 10, "string", 5, "ApiSample01");
        var context = CreateExceptionContext(exception);

        // Act
        filter.OnException(context);

        // Assert
        Assert.NotNull(context.Result);
        Assert.True(context.ExceptionHandled);
        var result = Assert.IsType<ObjectResult>(context.Result);
        Assert.Equal(400, result.StatusCode);
    }

    private static ActionExecutingContext CreateActionExecutingContext()
    {
        var httpContext = new DefaultHttpContext();
        var actionContext = new ActionContext(httpContext, new RouteData(), new ActionDescriptor());
        return new ActionExecutingContext(actionContext, new List<IFilterMetadata>(), new Dictionary<string, object?>(), new object());
    }

    private static ExceptionContext CreateExceptionContext(Exception exception)
    {
        var httpContext = new DefaultHttpContext();
        var actionContext = new ActionContext(httpContext, new RouteData(), new ActionDescriptor());
        return new ExceptionContext(actionContext, new List<IFilterMetadata>()) { Exception = exception };
    }
}