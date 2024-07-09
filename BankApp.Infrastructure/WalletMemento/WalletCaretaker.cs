namespace BankApp.Infrastructure.WalletMemento
{
	public class WalletCaretaker
	{
        private List<WalletMemento> _mementos = new List<WalletMemento>();
        private WalletExtension _wallet;

		public WalletCaretaker() { }

        public WalletCaretaker(WalletExtension wallet)
		{
			ArgumentNullException.ThrowIfNull(wallet);
			_wallet = wallet;
		}

		public void Backup()
		{
			_mementos.Add(_wallet.SaveMemento());
		}

		public void Undo()
		{
			if (_mementos.Count == 0) return;
			WalletMemento memento = _mementos.Last();
			_mementos.Remove(memento);
			_wallet.RestoreMemento(memento);
		}
	}
}

