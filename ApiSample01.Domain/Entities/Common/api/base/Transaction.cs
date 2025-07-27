namespace ApiSample01.Domain.Entities.Common.Api.Base;

public class Transaction
{
    public string LocalTransactionId { get; set; } = string.Empty;
    public DateTime LocalTransactionDate { get; set; }
}