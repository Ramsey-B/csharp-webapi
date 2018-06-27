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
    public User Register([FromBody]UserRegistration creds) 
    {
      if(ModelState.IsValid) 
      {
        return _repo.Register(creds);
      }
      return null;
    }
  }
}