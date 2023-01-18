using Core.Cache.Abstract;
using Core.Cache.Concrete.Redis;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore.Storage;
using Newtonsoft.Json;
using ServiceStack.Redis;
using System.Text;

public class Program
{
    public static void Main()
    {
        IUserDal efUserDal = new EfUserDal();
        ICacheService cacheService = new RedisCacheManager();

        using (IRedisClient client = new RedisClient())
        {
            //client.SetEntryInHash("Test", "1", "user1");
            /*var result = client.GetHashValues("Test");
            foreach (var item in result)
            {
                Console.WriteLine(item);
            }*/
            var result = client.GetValueFromHash("Test", "2");
            Console.WriteLine(result);
        }

        Console.WriteLine("Console: OK");
   }
}
