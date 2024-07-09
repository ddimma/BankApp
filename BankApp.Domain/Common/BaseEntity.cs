namespace BankApp.Domain.Common
{
    public class BaseEntity
    {
        public Guid Id { get; } = Guid.NewGuid();
        public DateTime TimeStamp { get; } = DateTime.Now;
    }
}

