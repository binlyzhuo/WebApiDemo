using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using WebApiDemo.Config;
using WebApiDemo.Data;
using WebApiDemo.Repository;

namespace WebApiDemo.Helper
{
    public class AutofacConfig
    {
        public static void Register(HttpConfiguration config)
        {
            var builder = new ContainerBuilder();

            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());//注册api容器的实现
            builder.RegisterControllers(Assembly.GetExecutingAssembly());//注册mvc容器的实现

            builder.RegisterType<UserRepository>().As<IUserRepository>().InstancePerRequest();
            builder.RegisterType<LogRepository>().As<ILogRepository>().InstancePerRequest();
            builder.RegisterType<UserData>().As<IUserData>().InstancePerRequest();
            builder.RegisterType<ConfigService>().As<IConfigService>().SingleInstance();

            IContainer container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);//注册api容器需要使用HttpConfiguration对象
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));//注册MVC容器
        }
    }
}