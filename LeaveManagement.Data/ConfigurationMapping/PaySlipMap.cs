using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeaveManagement.Core.DomainModels;

namespace LeaveManagement.Data.ConfigurationMapping
{
    public class PaySlipMap : EntityTypeConfiguration<PaySlip>
    {
        public PaySlipMap()
        {
            ToTable("PaySlip");
            HasKey(x=>x.Id);
            Property(x => x.Month);
            Property(x => x.Year);
            Property(x => x.SavedPath);
            Property(x => x.UserId);
        }
    }
}
