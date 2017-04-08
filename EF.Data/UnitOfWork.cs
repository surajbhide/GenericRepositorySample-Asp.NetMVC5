using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EF.Core;

namespace EF.Data
{
    public class UnitOfWork : IDisposable
    {
        private readonly EFDbContext context;
        private Dictionary<string, object> repositories;

        public UnitOfWork()
        {
            context = new EFDbContext();
        }

        public void Complete()
        {
            context.SaveChanges();
        }

        public Repository<T> Repository<T>() where T : BaseEntity
        {
            if (repositories == null)
            {
                repositories = new Dictionary<string, object>();
            }
            var type = nameof(T);
            if (!repositories.ContainsKey(type))
            {
                var repoType = typeof(Repository<>);
                // create the repo and pass context as an argument to constructor
                var repoInstance = Activator.CreateInstance(repoType.MakeGenericType(typeof(T)), context);
                repositories.Add(type, repoInstance);
            }
            return (Repository<T>)repositories[type];
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
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~UnitOfWork() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion

    }
}
