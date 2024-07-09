using System;
namespace BankApp.Infrastructure.Adapters
{
    public interface IAdapter<TDestination, TSource>
    {
        TDestination ToDto(TSource source);
        TSource ToEntity(TDestination source);
    }
}

