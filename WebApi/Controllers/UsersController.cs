using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IUserService _userService;
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpGet("getall")]
        public List<User> GetAll()
        {
            return _userService.GetAll();
        }
        [HttpGet("getbyid")]
        public User GetById(int userId)
        {
            return _userService.GetById(userId);
        }
        [HttpPost("add")]
        public void Add(User user)
        {
            _userService.Add(user);
        }
        [HttpDelete("delete")]
        public void DeleteById(int userId)
        {
            _userService.DeleteById(userId);
        }
        [HttpGet("clearcache")]
        public void ClearCache()
        {
            _userService.ClearCache();
        }
    }
}
