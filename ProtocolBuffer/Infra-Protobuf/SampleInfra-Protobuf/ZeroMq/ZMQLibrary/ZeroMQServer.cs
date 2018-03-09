using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Philips.PmsMR.Coreservices.ZMQ
{
    public abstract class ZeroMQServer: IDisposable
    {
        /// <summary>
        /// Initialize the server.
        /// </summary>
        public abstract void Initialize();

        /// <summary>
        /// Start the Server.
        /// </summary>
        public abstract void Start(CancellationToken cancellationToken);

        /// <summary>
        /// Stop the Server.
        /// </summary>
        public abstract void Stop();

        #region IDisposable members
        /// <summary>
        /// Dispose implementation
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        
        /// <summary>
        /// Finalizer
        /// </summary>
        ~ZeroMQServer()
        {
            Dispose(false);
        }

        /// <summary>
        /// Disposal of resources
        /// </summary>
        /// <param name="disposing">Is set when explicitly called</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing){ }
                else 
                {
                    System.Diagnostics.Debug.Assert(false, "Forgot to dispose object of type ZeroMQServer");
                }
                disposed = true;
            }
        }

        /// <summary>
        /// Flag to indicate disposed state of the object
        /// </summary>
        protected bool disposed;

        /// <summary>
        /// A check for disposal.
        /// </summary>        
        /// <exception cref="ObjectDisposedException">Throws if already disposed of</exception>
        protected void CheckDisposed()
        {
            if (disposed)
            {
                System.Diagnostics.Debug.Assert(false, "Trying to use disposed object of type ZeroMQServer");
                throw new ObjectDisposedException("ZeroMQServer");
            }
        }

        #endregion

    }
}
