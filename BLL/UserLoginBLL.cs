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
        public bool CheckLogin(string username, string password)
        {
            UserLoginDAL userLoginDAL = new UserLoginDAL();
            List<USER> users = userLoginDAL.GetAllUsers();
            foreach (var user in users)
            {
                if (user.Username == username && user.PasswordHash == password)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
