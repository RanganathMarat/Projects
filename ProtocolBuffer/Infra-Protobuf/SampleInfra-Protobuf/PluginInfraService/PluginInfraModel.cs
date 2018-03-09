using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Philips.PmsMR.Protobuf.DataModel;
using PluginInfraServices;

namespace PluginInfraServices
{
    /// <summary>
    /// Datamodel for the plugin's infrastructural services.
    /// </summary>
    /// <remarks>
    /// Plugin's Infrastructure services needs to support multiple interfaces from a single TCP port,
    /// therefore an union of interface namespaces is needed to create a common model
    /// to ensure that the ids for messages are unique within the union
    /// (and not just within a namespace).
    /// </remarks>
    public class PluginInfraModel : NamespaceTag
    {

        /// <summary>
        /// Do not limit types to a single namespace - use the explicitly generated type list (see below)
        /// </summary>
        public override string RootNamespace
        {
            get { return null; }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override Type[] GetTypes()
        {
            var excludedTypes = new HashSet<Type>();

            // The following interfaces call a single plugin REP server.
            var namespaces = new[]
            {
                typeof(QueryLicensesRequestMessage).Namespace,
                typeof(LogEntriesRequestMessage).Namespace,
                typeof(QueryVersionRequestMessage).Namespace
            };
            return namespaces.SelectMany(x => GetPublicTypesFromNamespace(GetType().Assembly, x, excludedTypes)).ToArray();
        }
    }
}
