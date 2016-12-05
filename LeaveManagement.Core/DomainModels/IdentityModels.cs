using Microsoft.AspNet.Identity.EntityFramework;

namespace LeaveManagement.Core.DomainModels
{
    // You can add profile data for the user by adding more properties to your ApplicationIdentityUser class.
    /// <summary>
    /// To Implement Onion Archicture with ASP.Net Identity, ApplicationIdentityUser class is moved from DATA to CORE project.
    /// Then only we can give foreign key references to other tables from AspNetUsers table.
    /// </summary>

    public class ApplicationIdentityUser :
        IdentityUser<int, ApplicationIdentityUserLogin, ApplicationIdentityUserRole, ApplicationIdentityUserClaim>
    {
       
    }


    public class ApplicationIdentityRole : IdentityRole<int, ApplicationIdentityUserRole>
    {
        public ApplicationIdentityRole()
        {
        }

        public ApplicationIdentityRole(string name)
        {
            Name = name;
        }
    }

    public class ApplicationIdentityUserRole : IdentityUserRole<int>
    {
    }

    public class ApplicationIdentityUserClaim : IdentityUserClaim<int>
    {
    }

    public class ApplicationIdentityUserLogin : IdentityUserLogin<int>
    {
    }

}