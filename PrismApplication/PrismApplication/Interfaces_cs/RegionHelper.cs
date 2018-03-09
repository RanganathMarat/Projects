using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrismApplication.Interfaces
{
    /// <summary>
    /// See
    /// ((Microsoft.Practices.Prism.Regions.RegionViewRegistry)(Microsoft.Practices.ServiceLocation.ServiceLocator.Current.GetInstance<IRegionViewRegistry>())).registeredContent
    /// </summary>
    public class RegionHelper : IDisposable
    {
        private static readonly object _lockObject = new object();
        private static readonly Hashtable _instanceMap = new Hashtable();

        private object _obj;

        private RegionHelper(object obj)
        {
            _obj = obj;
        }

        public static RegionHelper GetInstance(object obj)
        {
            RegionHelper instance;
            lock (_lockObject)
            {
                if (!_instanceMap.Contains(obj))
                {
                    instance = new RegionHelper(obj);
                    _instanceMap.Add(obj, instance);
                }
                else
                {
                    instance = (RegionHelper)_instanceMap[obj];
                }
            }
            return instance;
        }

        public object GetContentDelegate()
        {
            return _obj;
        }

        public void Dispose()
        {
            lock (_lockObject)
            {
                if (_instanceMap.Contains(_obj))
                {
                    _instanceMap.Remove(_obj);
                }
                // Must be set to null. As GetContentDelegate will 
                // always be retained by Microsoft.Practices.Prism.Regions.RegionViewRegistry
                // Consequently this instance will always remain in memory.
                _obj = null;
            }
        }
    }
}
