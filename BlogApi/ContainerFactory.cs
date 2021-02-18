using Autofac;
using BlogApi.common;
using BlogApi.Jwt;
using BlogApi.Models;
using DataBaseLayer;
using DataBaseLayer.Common;
using Microsoft.Extensions.Configuration;


namespace BlogApi
{
    public class ContainerFactory
    {
        public IContainer container { get; private set; }
        public IConfiguration Configuration { get; set; }
        public ContainerFactory(IConfiguration config)
        {
            Configuration = config;
            BuilAllDependency();
        }

        private void BuilAllDependency()
        {
            var builder = new ContainerBuilder();
            // get appsettings 
            var appSettings = new AppSettings();
            var appSettings2 = new DbAppSettings();
            Configuration.GetSection(nameof(AppSettings)).Bind(appSettings);
            Configuration.GetSection(nameof(AppSettings)).Bind(appSettings2);
            builder.RegisterInstance(appSettings).As<AppSettings>();
            builder.RegisterInstance(appSettings2).As<DbAppSettings>();
            builder.RegisterType<MongoCURD>().As<IMongoCURD>();
            builder.RegisterType<MongoUser>().As<IMongoUser>();
            builder.RegisterType<JwtAuthManager>().As<IJwtAuthManager>();
            //builder.Register((c, p) => new MongoCURD(p.Named<string>("database"), p.Named<string>("connectionString"))).As<IMongoCURD>();
            //builder.Register((c, p) => new MongoUser(p.Named<string>("connectionString"), p.Named<string>("dbName"))).As<IMongoUser>();
            container = builder.Build();
        }
    }
}
