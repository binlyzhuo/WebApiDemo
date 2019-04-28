using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using WebApiDemo.Data;

namespace WebApiDemo.Repository
{
    public class UserRepository:IUserRepository
    {
        private readonly IUserData _userData;
       

        public UserRepository(IUserData userData)
        {
            _userData = userData;
        }

        public async Task<string> GetUserName(string connstring)
        {
            return await _userData.GetUserName(connstring);
        }
    }

    public interface IUserRepository
    {
         Task<string> GetUserName(string connstring);
    }
}