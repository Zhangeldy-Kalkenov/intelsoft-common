namespace Intelsoft.Common.UseCases.Tests;

public class CreateOrderRequest
{
    public Guid CustomerId { get; set; }
}

public class CreateOrderResponse
{
    public Guid OrderId { get; set; }
}

public class CreateOrderUseCase : IUseCase<CreateOrderRequest, CreateOrderResponse>
{
    public Task<CreateOrderResponse> ExecuteAsync(CreateOrderRequest request, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(new CreateOrderResponse { OrderId = Guid.NewGuid() });
    }
}

public class UseCaseTests
{
    [Fact]
    public async Task UseCase_Returns_OrderId()
    {
        // Arrange
        var useCase = new CreateOrderUseCase();
        var request = new CreateOrderRequest { CustomerId = Guid.NewGuid() };

        // Act
        var response = await useCase.ExecuteAsync(request);

        // Assert
        Assert.NotNull(response);
        Assert.NotEqual(Guid.Empty, response.OrderId);
    }
}
