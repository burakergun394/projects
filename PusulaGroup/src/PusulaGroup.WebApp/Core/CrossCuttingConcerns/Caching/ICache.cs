using System.Linq.Expressions;

namespace PusulaGroup.WebApp.Core.CrossCuttingConcerns.Caching
{
    public interface ICache
    {
        object Get(string key);

        bool TryGet<T>(string key, out T value);

        bool IsAdd(string key);

        void Add(string key, object value, int duration);

        void Remove(string key);
        void RemoveKeysByStartingValue(string key);

    }
}
