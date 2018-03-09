using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IManageApp
{
   /// <summary>
    /// IManageApplication
    /// </summary>
    public interface IManageApplication
   {
       /// <summary>
       /// Initializes the specified callback.
       /// </summary>
       /// <param name="callback">The callback.</param>
       void Initialize(IManageAppCallback callback);

        /// <summary>
        /// Starts this instance.
        /// </summary>
        void Start();

        /// <summary>
        /// Closes this instance.
        /// </summary>
       void Close();

        /// <summary>
        /// Pings this instance.
        /// </summary>
        /// <returns></returns>
       bool Ping();
   }
}
