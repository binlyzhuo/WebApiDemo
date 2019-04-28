using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiDemo.Repository
{
    public class LogRepository:ILogRepository
    {
        public int GetCode()
        {
            return DateTime.Now.Millisecond;
        }
    }
}