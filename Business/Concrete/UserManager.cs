using Business.Abstract;
using Core.Cache.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class UserManager : IUserService
    {
        private IUserDal _userDal;
        private ICacheService _cacheService;
        public UserManager(IUserDal userDal, ICacheService cacheService)
        {
            _userDal = userDal;
            _cacheService = cacheService;
        }
        string _hashId = "User";
        public void Add(User user)
        {
            if(_cacheService.Get<User>(_hashId, user.Id) != null && _userDal.GetById(user.Id) == null)
            {
                _userDal.Add(user);
                Console.WriteLine("User already exist! Added to Db.");
            }
            else if(_userDal.GetById(user.Id) != null && _cacheService.Get<User>(_hashId, user.Id) == null)
            {
                _cacheService.Set(_hashId, user.Id, user);
                Console.WriteLine("User already exist! Added to cache.");
            }else if(_cacheService.Get<User>(_hashId, user.Id) != null && _userDal.GetById(user.Id) != null)
            {
                Console.WriteLine("User already exist!");
            }
            else
            {
                _userDal.Add(user);
                _cacheService.Set(_hashId, user.Id, user);
                Console.WriteLine("User added!");
            }
        }
        public void DeleteById(int userId)
        {
            User user = GetById(userId);
            if(_userDal.GetById(user.Id) == null && _cacheService.Get<User>(_hashId, user.Id) == null)
            {
                Console.WriteLine("No such user!");
            }else if(_userDal.GetById(user.Id) != null && _cacheService.Get<User>(_hashId, user.Id) == null)
            {
                _userDal.Delete(user);
                Console.WriteLine("User has been deleted from db.");
            }else if(_userDal.GetById(user.Id) == null && _cacheService.Get<User>(_hashId, user.Id) != null)
            {
                _cacheService.Delete(_hashId, user.Id);
                Console.WriteLine("User has been deleted from cache.");
            }
            else
            {
                _userDal.Delete(user);
                _cacheService.Delete(_hashId, user.Id);
                Console.WriteLine("User has been deleted!");
            }
        }
        public List<User> GetAll()
        {
            List<User> list = _cacheService.GetAll<User>(_hashId);
            bool isEmpty = list.Count == 0;
            if (! isEmpty)
            {
                return list;
            }else if(_userDal.GetAll() != null)
            {
                List<User> _list = _userDal.GetAll();
                foreach (var item in _list)
                {
                    _cacheService.Set(_hashId, item.Id, item);
                }
                Console.WriteLine("Not found on cache, returned from db.");
                return _list;
                
            }else
            {
                Console.WriteLine("There is no user!");
                return null;
            }
        }
        public User GetById(int id)
        {
            if (_cacheService.Get<User>(_hashId, id) != default(User))
            {
                return _cacheService.Get<User>(_hashId, id);
            }
            else if (_userDal.GetById(id) != null)
            {
                User user = _userDal.GetById(id);
                _cacheService.Set(_hashId, id, user);
                Console.WriteLine("Not found on cache, returned from db.");
                return user;
            }
            else
            {
                Console.WriteLine("There is no such user!");
                return null;
            }
        }
        public void ClearCache()
        {
            _cacheService.DeleteAll(_hashId);            
            /*foreach (var item in _userDal.GetAll())
            {
                _cacheService.HSet(_hashId, item.Id, item);
            }*/
        }
    }
}
