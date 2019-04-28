using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Dapper;
using WebApiDemo.Models;

namespace WebApiDemo.Data
{
    public class UserData : IUserData
    {
        public async Task<string> GetUserName(string connstring)
        {
            using (SqlConnection conn = new SqlConnection(connstring))
            {
                string sql = "SELECT [UID] AS UserId,UName AS UserName FROM dbo.[User] WHERE [UID] = 1;";
                var item = await conn.QueryAsync<UserBaseInfo>(sql);
                var temp = item.FirstOrDefault();
                if (temp != null)
                    return temp.UserName;

            }
            return "hello";
        }
    }

    public interface IUserData
    {
         Task<string> GetUserName(string connstring);
    }
}