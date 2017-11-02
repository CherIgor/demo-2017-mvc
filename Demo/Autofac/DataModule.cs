using Autofac;
using Demo.Services.IServices;
using Demo.Services.Services;

namespace Demo.Autofac
{
    public class DataModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<UsersService>().As<IUsersService>().InstancePerRequest();

            base.Load(builder);
        }
    }
}