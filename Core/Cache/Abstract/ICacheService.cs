using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Cache.Abstract
{
    public interface ICacheService
    {
        void Set(string hashId, int key, Object _object);
        T Get<T>(string hashId, int key);
        List<T> GetAll<T>(string hashId);
        void Delete(string hashId, int key);
        void DeleteAll(string hashId);
    }
}
