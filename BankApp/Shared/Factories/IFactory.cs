using System;
namespace BankApp.Shared.Factories
{
    public interface IFactory<T>
    {
        T Create();
    }
}

