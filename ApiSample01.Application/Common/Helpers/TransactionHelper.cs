namespace ApiSample01.Application.Common.Helpers;

using ApiSample01.Domain.Entities.Common.Api.Base;

public static class TransactionHelper
{
    public static Transaction CreateTransaction() => new()
    {
        LocalTransactionId = Guid.NewGuid().ToString(),
        LocalTransactionDate = DateTime.UtcNow
    };
}