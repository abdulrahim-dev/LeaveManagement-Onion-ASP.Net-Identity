using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(LeaveManagement.DI.Startup))]
namespace LeaveManagement.DI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}


//**********************************************

// Startup class is defined in this project. [assembly: OwinStartup(typeof(LeaveManagement.DI.Startup))] is used to make this class as startup handler class.
// Web project is only dependant with Core and Services
// We dont need to install Identity, Owin, EntityFramework in Web Project.
// We are installing all the references to this project.
// Autofac is used for Dependancy Injection.
