using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using LeaveManagement.Core.Data;
using LeaveManagement.Core.Services;
using LeaveManagement.Data;
using LeaveManagement.DI;
using LeaveManagement.Services;
using LeaveManagement.Web;


// Adding IocConfig file to Application Start method for implement Dependancy Injection
[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(IocConfig), "RegisterDependencies")]
namespace LeaveManagement.DI
{
    public class IocConfig
    {
        public static void RegisterDependencies()
        {
            var builder = new ContainerBuilder();

            // Registering Controllers for the Web Application
            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            builder.RegisterModule<AutofacWebTypesModule>();

            // Registering Interfaces We have Created in Core Project
            builder.RegisterGeneric(typeof(EntityRepository<>)).As(typeof(IRepository<>)).InstancePerRequest();
            builder.RegisterGeneric(typeof(Service<>)).As(typeof(IService<>)).InstancePerRequest();
            builder.RegisterType(typeof(UnitOfWork)).As(typeof(IUnitOfWork)).InstancePerRequest();

            builder.Register<IEntitiesContext>(b =>
            {
                var context = new LeaveContext();
                return context;
            }).InstancePerRequest();


            builder.RegisterModule(new IdentityModule());

            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}
