using BankApp.Shared.Interfaces;

namespace BankApp.Shared
{
	public class TransactionDto : IDeepCopyable<TransactionDto>
	{
        public string Id { get; set; }
        public string Type { get; set; }
        public string Message { get; set; }
        public string SourceWalletId { get; set; }
        public string DestinationWalletId { get; set; }
        public decimal TransactionAmount { get; set; }

        public void CopyTo(TransactionDto target)
        {
            ArgumentNullException.ThrowIfNull(target);
            target.Message = Message is not null ? (string)Message.Clone() : string.Empty;
            target.Type = Type is not null ? (string)Type.Clone() : string.Empty;
            target.SourceWalletId = (string)SourceWalletId.Clone();
            target.DestinationWalletId = (string)DestinationWalletId.Clone();
            target.TransactionAmount = TransactionAmount;
        }

        public virtual TransactionDto DeepCopy()
        {
            var copy = new TransactionDto();
            CopyTo(copy);
            return copy;
        }
    }

    public class TransactionByEmailDto : TransactionDto, IDeepCopyable<TransactionByEmailDto>
    {
        public string DestinationEmail { get; set; }

        public void CopyTo(TransactionByEmailDto target)
        {
            ArgumentNullException.ThrowIfNull(target);
            base.CopyTo(target);
            target.DestinationEmail = (string)DestinationEmail.Clone();
        }

        public override TransactionByEmailDto DeepCopy()
        {
            var copy = new TransactionByEmailDto();
            CopyTo(copy);
            return copy;
        }
    }

    public class TransactionByWalletCodeDto : TransactionDto, IDeepCopyable<TransactionByWalletCodeDto>
    {
        public string DestinationWalletCode { get; set; }

        public void CopyTo(TransactionByWalletCodeDto target)
        {
            ArgumentNullException.ThrowIfNull(target);
            base.CopyTo(target);
            target.DestinationWalletCode = (string)DestinationWalletCode.Clone();
        }

        public override TransactionByWalletCodeDto DeepCopy()
        {
            var copy = new TransactionByWalletCodeDto();
            CopyTo(copy);
            return copy;
        }
    }
}

