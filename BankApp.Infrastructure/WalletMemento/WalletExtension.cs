using BankApp.Domain.Models;

namespace BankApp.Infrastructure.WalletMemento
{
	public class WalletExtension : Wallet
	{
        public WalletMemento SaveMemento()
		{
			return new WalletMemento(base.Amount,
				base.Type,
				base.WalletCode,
				base.IsFavorite,
				base.IsMainWallet);
		}

		public void RestoreMemento(WalletMemento memento)
		{
			base.Amount = memento.Amount;
			base.Type = memento.Type;
			base.WalletCode = memento.WalletCode;
			base.IsFavorite = memento.IsFavorite;
			base.IsMainWallet = memento.IsMainWallet;
		}
	}
}

