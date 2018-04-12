using System.Linq;
using Microsoft.EntityFrameworkCore;
using Models;

namespace DatabaseController.Repositories
{
    public class UserRepository
    {
        public bool CheckIfUserExist(string login)
        {
            using (var context = new FlyDbContext())
            {
                foreach (var user in context.Users)
                {
                    if (user.Login == login)
                        return true;
                }
                return false;
            }
        }

        private bool CheckIfUserExist(string login, FlyDbContext context)
        {
            foreach (var user in context.Users)
            {
                if (user.Login == login)
                    return true;
            }
            return false;
        }
        public bool AddUser(string login, string password, string email)
        {
            using (var context = new FlyDbContext())
            {
                if (CheckIfUserExist(login, context))
                    return false;

                var user = new User { Login = login, Email = email, Password = PasswordHasher.HashPassword(password) };
                context.Add(user);
                context.SaveChanges();
            }
            return true;
        }

        public void DeleteUser(string login)
        {
            using (var context = new FlyDbContext())
            {
                context.RemoveRange(context.Users.Include(x => x.Devices).FirstOrDefault(x => x.Login == login).Devices);
                context.Users.RemoveRange(context.Users.Where(x => x.Login == login));
                context.SaveChanges();
            }
        }

        public bool VerifyUser(string login, string password)
        {
            using (var context = new FlyDbContext())
            {
                if (!CheckIfUserExist(login, context))
                    return false;

                User user = context.Users.FirstOrDefault(x => x.Login == login);
                if (PasswordHasher.VerifyHashedPassword(user.Password, password))
                    return true;
            }
            return false;
        }

        public User GetUserByDeviceId(string deviceId)
        {
            using (var context = new FlyDbContext())
            {
                foreach (User user in context.Users.Include(x => x.Devices))
                {
                    foreach (Device device in user.Devices)
                    {
                        if (device.DeviceId == deviceId)
                            return user;
                    }
                }
            }
            return null;
        }
    }
}
