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
    }
}
