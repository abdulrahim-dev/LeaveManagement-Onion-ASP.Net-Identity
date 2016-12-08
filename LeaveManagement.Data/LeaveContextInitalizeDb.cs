using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeaveManagement.Core.DomainModels;
using Microsoft.AspNet.Identity.EntityFramework;

namespace LeaveManagement.Data
{
    //---------------
    //CreateDatabaseIfNotExists
    //DropCreateDatabaseIfModelChanges
    //DropCreateDatabaseAlways
    public class LeaveContextInitalizeDb : CreateDatabaseIfNotExists<LeaveContext>
    {
        protected override void Seed(LeaveContext context)
        {
            context.Roles.Add(new ApplicationIdentityRole()
            {
                Name = "Admin"
            });
            context.Roles.Add(new ApplicationIdentityRole()
            {
                Name = "Manager"
            });
            context.Roles.Add(new ApplicationIdentityRole()
            {
                Name = "User"
            });

            context.Users.Add(new ApplicationIdentityUser()
            {
                Email="abc@aol.com",UserName="abc@aol.com",PasswordHash="12345",Roles = { new ApplicationIdentityUserRole() {RoleId=1} }
            });

            
            base.Seed(context);
        }
    }

   
}
