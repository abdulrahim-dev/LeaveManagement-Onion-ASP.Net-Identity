using System.Data.Entity.ModelConfiguration;
using LeaveManagement.Core.DomainModels;

namespace LeaveManagement.Data.ConfigurationMapping
{
    /// <summary>
    /// Fluent API is another way to configure your domain classes. Fluent API provides more functionality for configuration than DataAnnotations....
    /// EntityTypeConfiguration is an important class in Fluent API. EntityTypeConfiguration provides you important methods to configure entities and its properties to override various Code-First conventions. It can be obtained by calling the Entity<TEntity>() method of DbModelBuilder class
    /// </summary>
    public class EmployeeDetailsMap : EntityTypeConfiguration<UserProfile>
    {
        public EmployeeDetailsMap()
        {
            ToTable("EmployeeDetails");
            Property(x => x.Name);
            //this.HasOptional(x => x.ApplicationIdentityUser).WithMany().HasForeignKey(x => x.UserId);

        }
    }
}