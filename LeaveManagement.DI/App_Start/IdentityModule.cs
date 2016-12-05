﻿using System.Data.Entity;
using System.Web;
using Autofac;
using Autofac.Integration.Mvc;
using LeaveManagement.Core.DomainModels;
using LeaveManagement.Core.Identity;
using LeaveManagement.Data;
using LeaveManagement.Data.Identity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Module = Autofac.Module;

namespace LeaveManagement.DI
{
    public class IdentityModule : Module
    {
        /// <summary>
        /// Registering all Interfaces related to ASP.Net Identity implementation.
        /// </summary>
        /// <param name="builder"></param>
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType(typeof(ApplicationUserManager)).As(typeof(IApplicationUserManager)).InstancePerHttpRequest();
            builder.RegisterType(typeof(ApplicationRoleManager)).As(typeof(IApplicationRoleManager)).InstancePerHttpRequest();
            builder.RegisterType(typeof(ApplicationIdentityUser)).As(typeof(IUser<int>)).InstancePerHttpRequest();
            builder.Register(b => b.Resolve<IEntitiesContext>() as DbContext).InstancePerHttpRequest();
            builder.Register(b =>
            {
                var manager = IdentityFactory.CreateUserManager(b.Resolve<DbContext>());
                if (Startup.DataProtectionProvider != null)
                {
                    manager.UserTokenProvider =
                        new DataProtectorTokenProvider<ApplicationIdentityUser, int>(
                            Startup.DataProtectionProvider.Create("ASP.NET Identity"));
                }
                return manager;
            }).InstancePerHttpRequest();
            builder.Register(b => IdentityFactory.CreateRoleManager(b.Resolve<DbContext>())).InstancePerHttpRequest();
            builder.Register(b => HttpContext.Current.GetOwinContext().Authentication).InstancePerHttpRequest();
            
        }
    }
}
