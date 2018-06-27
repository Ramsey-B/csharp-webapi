using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace smoothie_shack.Models
{
  public class User
  {
    public string Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    private ClaimsPrincipal _principal { get; set; }

    public ClaimsPrincipal GetPrincipal()
    {
      return _principal;
    }

    public void SetClaims()
    {
      var claims = new List<Claim>
      {
        new Claim(ClaimTypes.Email, Email),
        new Claim(ClaimTypes.Name, Id)
      };
      var userIdentity = new ClaimsIdentity(claims, "login");
      _principal = new ClaimsPrincipal(userIdentity);
    }
  }

  public class UserRegistration
  {
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
  }

  public class UserLogin
  {
    public string Email { get; set; }
    public string Password { get; set; }
  }


}