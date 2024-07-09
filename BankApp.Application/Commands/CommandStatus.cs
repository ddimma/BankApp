namespace BankApp.Application.Commands
{
    public class CommandsStatus
    {
        public bool IsSuccessful { get; set; } = true;
        public string Error { get; set; } = string.Empty;

        public static CommandsStatus Failed(string error)
        {
            return new()
            {
                IsSuccessful = false,
                Error = error
            };
        }
    }
}

