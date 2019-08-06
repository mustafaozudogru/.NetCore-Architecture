using CoreArchitectureDesign.Core.Interfaces;
using CoreArchitectureDesign.Data.Concrete.EntityFramework;
using System;

namespace CoreArchitectureDesign.Business
{
    public class UnitOfWork : IUnitOfWork
    {
        private NorthwndContext context;

        public UnitOfWork(NorthwndContext context)
        {
            this.context = context;
        }

        public int Commit()
        {
            var transId = -1;
            if (this.context.ChangeTracker.HasChanges())
            {
                using (var dbContextTransaction = this.context.Database.BeginTransaction())
                {
                    try
                    {
                        if (this.context != null)
                        {
                            transId = this.context.SaveChanges();
                            dbContextTransaction.Commit();
                        }
                    }
                    catch (Exception ex)
                    {
                        dbContextTransaction.Rollback();
                        throw new Exception(ex.ToString());
                    }
                }
            }

            return transId;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (!disposing) return;
            if (this.context == null) return;
            this.context.Dispose();
            this.context = null;
        }
    }
}
