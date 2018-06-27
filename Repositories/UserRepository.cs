using System;
using System.Data;
using Dapper;
using smoothie_shack.Models;
using BCrypt.Net;
using System.Linq;

namespace smoothie_shack.Repositories
{
  public class UserRepository
  {
    private readonly IDbConnection _db;

    public UserRepository(IDbConnection db)
    {
      _db = db;
    }

    public User Register(UserRegistration creds)
    {
      try
      {
        var sql = @"
      INSERT INTO users (id, name, email, password)
      VALUES (@Id, @Name, @Email, @Password);
      SELECT LAST_INSERT_ID();";

        string id = Guid.NewGuid().ToString();

        creds.Password = BCrypt.Net.BCrypt.HashPassword(creds.Password, 12);

        _db.ExecuteScalar<string>(sql, new
        {
          Id = id,
          Name = creds.Name,
          Email = creds.Email,
          Password = creds.Password
        });

        return new User()
        {
          Id = id,
          Name = creds.Name,
          Email = creds.Email,
        };
      }
      catch (Exception e)
      {
        System.Console.WriteLine(e.Message);
        return null;
      }
    }
    public User Login(UserLogin creds)
    {
      User user = GetUserByEmail(creds.Email);
      if (user == null) { return null; }
      if (BCrypt.Net.BCrypt.Verify(creds.Password, user.Password))
      {
        return user;
      }
      return null;
    }

    public User GetUserById(string id)
    {
      return _db.Query<User>("SELECT * FROM users WHERE id = @id", new { id }).FirstOrDefault();
    }

    public User GetUserByEmail(string email)
    {
      return _db.Query<User>("SELECT * FROM users WHERE email = @email", new { email }).FirstOrDefault();
    }
  }
}