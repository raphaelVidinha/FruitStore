using System.Collections.Generic;
using System.Linq;
using FruitStore.Models;

namespace FruitStore.Repositories
{
    public static class UserRepository
    {
        public static User Get(string username, string password)
        {
            var users = new List<User>();
            users.Add(new User { Id = 1, Username = "admin", Password = "admin", Role = "admin" });
            users.Add(new User { Id = 2, Username = "user", Password = "123456", Role = "user" });
            return users.Where(x => x.Username.ToLower() == username.ToLower() && x.Password == x.Password).FirstOrDefault();
        }
    }
}
