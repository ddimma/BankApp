namespace BankApp.Shared.Factories
{
    public class Factory<T> : IFactory<T> where T : new()
    {
        public T Create()
        {
            return new T();
        }
    }
}

