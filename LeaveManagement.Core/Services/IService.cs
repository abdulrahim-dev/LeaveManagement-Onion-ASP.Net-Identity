using System;
using LeaveManagement.Core.Data;

namespace LeaveManagement.Core.Services
{
    public interface IService : IDisposable
    {
        IUnitOfWork UnitOfWork { get; }
    }
}
