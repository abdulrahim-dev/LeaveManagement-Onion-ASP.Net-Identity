﻿using System.ComponentModel.DataAnnotations.Schema;
using LeaveManagement.Core.DomainModels.Identity;

namespace LeaveManagement.Core.DomainModels
{
    public class UserProfile:BaseEntity
    {
        public string Name { get; set; }
        public virtual int? UserId { get; set; }

        [ForeignKey("UserId")]
        public ApplicationIdentityUser ApplicationIdentityUser { get; set; }
    }
}
