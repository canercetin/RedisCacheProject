using Core.Cache.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfUserDal : IUserDal
    {
        int sleeptime = 3000;
        public List<User> GetAll()
        {
            using (TestDbContext context = new TestDbContext())
            {
                System.Threading.Thread.Sleep(sleeptime);
                Console.WriteLine("DAL:GETALL");
                return context.Users.ToList();                
            }         
            
        }
        public User GetById(int id)
        {
            using (TestDbContext context = new TestDbContext())
            {
                System.Threading.Thread.Sleep(sleeptime);
                Console.WriteLine("DAL:GETBYID");
                return context.Users.SingleOrDefault(x => x.Id == id);                
            } 
            
        }
        public void Add(User user)
        {
            using (TestDbContext context = new TestDbContext())
            {
                System.Threading.Thread.Sleep(sleeptime);
                context.Users.Add(user);
                context.SaveChanges();                
            } 
            Console.WriteLine("DAL:ADDED");
        }

        public void Delete(User user)
        {
            using (TestDbContext context = new TestDbContext())
            {
                System.Threading.Thread.Sleep(sleeptime);
                context.Users.Remove(user);
                context.SaveChanges();                
            }   
            Console.WriteLine("DAL:DELETED");
        }
    }
}
