using Core.Cache.Abstract;
using Newtonsoft.Json;
using ServiceStack.Redis;

namespace Core.Cache.Concrete.Redis
{
    public class RedisCacheManager : ICacheService
    {
        public void Set(string hashId, int key, Object _object)
        {
            using (IRedisClient client = new RedisClient())
            {
                String value = JsonConvert.SerializeObject(_object);
                client.SetEntryInHash(hashId, Convert.ToString(key), value);
                //client.ExpireEntryIn(hashId, TimeSpan.FromMinutes(15));
            }
        }
        public T Get<T>(string hashId, int key)
        {
            using (IRedisClient client = new RedisClient())
            {             
                var result = client.GetValueFromHash(hashId, Convert.ToString(key));
                if (result == null)
                {
                    //client.ExpireEntryIn(hashId, TimeSpan.FromMinutes(15));
                    return default(T);
                }

                    //client.ExpireEntryIn(hashId, TimeSpan.FromMinutes(15));
                return JsonConvert.DeserializeObject<T>(result);

            }
                
        }
        public List<T> GetAll<T>(string hashId)
        {
            using (IRedisClient client = new RedisClient())
            {
                List<T> result = new List<T>();
                foreach (var item in client.GetHashValues(hashId))
                {
                    result.Add(JsonConvert.DeserializeObject<T>(item));
                }
                return result;
            }                
        }
        public void Delete(string hashId, int key)
        {
            using(IRedisClient client = new RedisClient())
            {
                client.RemoveEntryFromHash(hashId, Convert.ToString(key));
            }
        }
        public void DeleteAll(string hashId)
        {
            using (IRedisClient client = new RedisClient())
            {
                client.Remove(hashId);
            }
        }

    }
}
