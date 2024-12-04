using Project.Domain;
using Project.Domain.Helpers.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Core
{
    public abstract class CoreService : IDisposable
    {
        private PaginationProvider _paginationProvider;

        protected CoreService(AppDbContext dbContext)
        {
            Db = dbContext;
        }

        protected AppDbContext Db { get; private set; }

        protected PaginationProvider Pagination
        {
            get
            {
                if (_paginationProvider == null)
                {
                    _paginationProvider = new PaginationProvider(Db);
                }

                return _paginationProvider;
            }
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                    if (Db != null)
                    {
                        Db.Dispose();
                        Db = null;
                    }

                    if (_paginationProvider != null)
                    {
                        _paginationProvider.Dispose();
                        _paginationProvider = null;
                    }
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~CoreService()
        // {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        void IDisposable.Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}
