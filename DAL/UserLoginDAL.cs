using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class UserLoginDAL
    {
        DataAccessEntity db = new DataAccessEntity();
        public List<USER> GetAllUsers()
        {
            List<USER> userdata = new List<USER>();
            var tb = from username
                        in db.USERs
                     select username;
            foreach (var item in tb)
            {
                USER user = new USER();
                user.UserID = item.UserID;
                user.Username = item.Username;
                user.PasswordHash = item.PasswordHash;
                userdata.Add(user);
            }
                return userdata;
        }
        public bool InsertUser(string username, string password)
        {
            USER newUser = new USER
            {
                UserID = Guid.NewGuid().ToString().Substring(0,5),
                Username = username,
                PasswordHash = password,
                Role = "Nhân viên",
                CreatedAt = DateTime.Now
            };

            try
            {
                db.USERs.Add(newUser);
                return db.SaveChanges() > 0;
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException ex)
            {
                foreach (var validationError in ex.EntityValidationErrors)
                {
                    Console.WriteLine($"Entity of type {validationError.Entry.Entity.GetType().Name} has the following validation errors:");
                    foreach (var error in validationError.ValidationErrors)
                    {
                        Console.WriteLine($"- Property: {error.PropertyName}, Error: {error.ErrorMessage}");
                    }
                }
                return false;
            }
        }

        public bool IsUsernameExists(string username)
        {
            return db.USERs.Any(u => u.Username == username);
        }
    }
}
