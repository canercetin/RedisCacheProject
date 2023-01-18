using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface IUserDal
    {
        List<User> GetAll();
        User GetById(int id);
        void Add(User user);
        void Delete(User user);
    }
}
