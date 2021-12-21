using System;
using System.Collections.Generic;
using System.Text;

namespace Core.CrossCuttingConcerns.Caching
{
    public interface ICacheManager
    {
        T Get<T>(string key); //bir key vereceğiz, bellekten keye karşılık gelen datayı bize ver.
        object Get(string key);//not generic
        void Add(string key,object value, int duration);//key value şeklinde duracak cache ler, bir de duration cache ne kadar duracak süre belirlemek adına.
        bool IsAdd(string key);
        void Remove(string key);
        void RemoveByPattern(string pattern);

    }
}
