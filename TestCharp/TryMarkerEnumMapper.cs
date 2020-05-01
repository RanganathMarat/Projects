using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TestCharp
{
    class TryMarkerEnumMapper
    {
    }


    public enum DiMarkerDefinitions
    {
        None = 0,
        VcgTriggerStart = 12,
        VcgTriggerEnd = 14,
        PeakVcgTrigger = 16,
        TroughVcgTrigger = 18,
        DiMarkerDefinitionsUnknown = 999
    }

    public enum MrMarkerDefinitions
    {
        None,
        MrVcgTriggerStart,
        MrVcgTriggerEnd
    }

    public enum CtMarkerDefinitions
    {
        None, 
        CtPeakVcgTrigger,
        CtTroughVcgTrigger
    }

    public interface IModalityMarkerToDiMarkerProvider
    {
        Dictionary<uint, DiMarkerDefinitions> GetMapper();
    }

    /// <summary>
    /// MR's marker mapper provider
    /// </summary>
    public class MrMarkerToDIMarkerMapperProvider : IModalityMarkerToDiMarkerProvider
    {
        private Dictionary<uint, DiMarkerDefinitions> mapper = new Dictionary<uint, DiMarkerDefinitions>();

        public MrMarkerToDIMarkerMapperProvider()
        {
            mapper[(uint)MrMarkerDefinitions.MrVcgTriggerStart] = DiMarkerDefinitions.VcgTriggerStart;
            mapper[(uint) MrMarkerDefinitions.MrVcgTriggerEnd] = DiMarkerDefinitions.VcgTriggerEnd;
        }
        public Dictionary<uint, DiMarkerDefinitions> GetMapper()
        {
            return mapper;

        }
    }

    public class CtMarkerToDiMarkerMapperProvider : IModalityMarkerToDiMarkerProvider
    {
        private Dictionary<uint, DiMarkerDefinitions> ctMapper = new Dictionary<uint, DiMarkerDefinitions>();

        public CtMarkerToDiMarkerMapperProvider()
        {
            ctMapper[(uint)CtMarkerDefinitions.CtPeakVcgTrigger] = DiMarkerDefinitions.PeakVcgTrigger;
            ctMapper[(uint)CtMarkerDefinitions.CtTroughVcgTrigger] = DiMarkerDefinitions.TroughVcgTrigger;
        }

        public Dictionary<uint, DiMarkerDefinitions> GetMapper()
        {
            return ctMapper;
        }
    }
}


