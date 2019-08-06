using System;
using System.Collections.Generic;
using System.Text;

namespace CoreArchitectureDesign.Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        int Commit();
    }
}
