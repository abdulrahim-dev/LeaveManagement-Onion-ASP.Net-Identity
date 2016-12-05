using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeaveManagement.Core.DomainModels;

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
            //context.Student.Add(
            //new EmployeeDetails
            //{
            //    StudentId =100,
            //    Name = "Rahul Kapoor"

            //});
            //context.Student.Add(
            //new EmployeeDetails
            //{
            //    StudentId = 101,
            //    Name = "Salman Khan"
            //});
            base.Seed(context);
        }
    }
}
