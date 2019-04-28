using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using WebApiDemo.Config;
using WebApiDemo.Helper;
using WebApiDemo.Models;
using WebApiDemo.Repository;

namespace WebApiDemo.Controllers
{
    public class UserController : ApiController
    {
        private readonly ILogRepository _log;
        private readonly IUserRepository _userRep;
        private readonly IConfigService _configService;
        public UserController(ILogRepository log,IUserRepository userRep,IConfigService configService)
        {
            _log = log;
            _userRep = userRep;
            _configService = configService;
        }

        //[HttpPost]
        [HttpGet]
        [Route("api/demo1")]
        public async Task<Demo1Response> Demo1()
        {
            var rsp = new Demo1Response();
            await RunCountAsync();
            rsp.RspCode = 1000;
            return rsp;
        }

        [HttpGet]
        [Route("api/demo2")]
        public Demo1Response Demo2()
        {
            var rsp = new Demo1Response();
            int num = 1;
            CancellationToken token = new CancellationToken();
            Task.Factory.StartNew(CountNumber,num,token);
            rsp.RspCode = 1000;
            return rsp;
        }

        [HttpGet]
        [Route("api/demo3")]
        public async Task<Demo1Response> Demo3()
        {
            var rsp = new Demo1Response();
            await RunCountAsync();
            rsp.RspCode = 1000;
            return rsp;
        }

        [HttpGet]
        [Route("api/demo4")]
        public async Task<Demo1Response> Demo4()
        {
            var rsp = new Demo1Response();
            int len = await WebPageLengthCount();
            await RunCountAsync();

            
            rsp.RspCode = len;
            return rsp;
        }

        [HttpGet]
        [Route("api/demo5")]
        public Demo1Response Demo5()
        {
            System.Diagnostics.Debug.WriteLine("Thread.CurrentThread.ManagedThreadId1:" + Thread.CurrentThread.ManagedThreadId);
            var rsp = new Demo1Response();
            int num = WebPageLengthCount().Result;
            rsp.RspCode = num;

            int code = _log.GetCode();
            
            return rsp;
        }

        [HttpGet]
        [Route("api/demo6")]
        public async Task<Demo1Response> Demo6()
        {
            var rsp = new Demo1Response();

            string name = await _userRep.GetUserName(_configService.ConfigGet().ConnString);
            rsp.RspTxt = name;
            return rsp;
        }

        [HttpGet]
        [Route("api/demo7")]
        public Demo1Response Demo7()
        {
            var rsp = new Demo1Response();
            var configItem = _configService.ConfigGet();
            return rsp;
        }

        public async Task RunCountAsync()
        {
            Task t = Task.Run(() => { Task.Delay(1000 * 10); });
            
            await t;
        }

        public async Task<int> WebPageLengthCount()
        {
            System.Diagnostics.Debug.WriteLine("Thread.CurrentThread.ManagedThreadId2:" + Thread.CurrentThread.ManagedThreadId);
            string url = "https://www.taobao.com";
            
            using (HttpClient client = new HttpClient())
            {
                var task = await client.GetStringAsync(url).ConfigureAwait(false);
               
                int len = task.Length;
                return len;
            }
        }

        public async void RunCount()
        {
            await RunCountAsync();
        }

        private void CountNumber(object obj)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            int count = 10000;
            string str = "";
            for (int i = 0; i < count * 10; i++)
            {
                str += i;
            }
            sw.Stop();
            LogHelper.Debug("CountNumber:"+sw.Elapsed.TotalMilliseconds);
        }


    }
}
