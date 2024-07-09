using MediatR;
namespace BankApp.Application.Commands.UpdateCommision
{
	public class UpdateCommisionCommand : IRequest<CommandsStatus>
	{
		public string CommisionId { get; set; }
		public string WalletType { get; set; }
		public decimal CommisionRate { get; set; }
	}
}

