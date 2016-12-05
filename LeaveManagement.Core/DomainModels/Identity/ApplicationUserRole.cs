using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeaveManagement.Core.DomainModels.Identity
{
    public class ApplicationUserRole
    {
        
        public virtual int RoleId { get; set; }
        
        public virtual int UserId { get; set; }
        
    }
}
