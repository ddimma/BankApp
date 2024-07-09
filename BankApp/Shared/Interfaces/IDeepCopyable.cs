namespace BankApp.Shared.Interfaces
{
	public interface IDeepCopyable<T> where T : new()
	{
        void CopyTo(T target);
        T DeepCopy()
        {
            T t = new T();
            CopyTo(t);
            return t;
        }
    }
}

