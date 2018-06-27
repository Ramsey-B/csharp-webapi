using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using smoothie_shack.Models;
using smoothie_shack.Repositories;

namespace smoothie_shack.Controllers
{
  [Route("[controller]")]
  public class AccountController : Controller
  {
    private readonly UserRepository _repo;
    public AccountController(UserRepository repo)
    {
        _repo = repo;
    }

    [HttpPost("register")]
    public async Task<User> Register([FromBody]UserRegistration creds) 
    {
      if(ModelState.IsValid) 
      {
        User user = _repo.Register(creds);
        if(user == null){ return null; } //prob want an exception
        user.SetClaims();
        await HttpContext.SignInAsync(user.GetPrincipal());
        return user;
      }
      return null;
    }

    [HttpPost("login")]
    public async User Login([FromBody]UserLogin creds) 
    {
      if(ModelState.IsValid) 
      {
        User user = _repo.Login(creds);
        if(user == null){ return null; } //prob want an exception
        user.SetClaims();
        await HttpContext.SignInAsync(user.GetPrincipal());
        return user;
      }
      return null;
    }

    [HttpGet]
    public User Authenticate()
    {
      var user = HttpContext.User;
      string id = user.Identity.Name;
      return _repo.GetUserById(id);
    }
  }
}