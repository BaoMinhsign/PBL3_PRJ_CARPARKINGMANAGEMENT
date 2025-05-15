using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
namespace BLL
{
    public class UserLoginBLL
    {
        public USER CheckLogin(string username, string password)
        {
            UserLoginDAL userLoginDAL = new UserLoginDAL();
            return userLoginDAL.GetUserByUsernameAndPassword(username, password);
        }

        public bool AddUser(string username, string password)
        {
            UserLoginDAL userLoginDAL = new UserLoginDAL();

            if (userLoginDAL.IsUsernameExists(username))
                return false;

            return userLoginDAL.InsertUser(username,password);
        }

    }
}
