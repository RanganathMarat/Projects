using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProtoBuf;

namespace ProtocolBuf12.ECViewModel
{
    [ProtoContract]
    public enum AdaptingOptions
    {
        /// <summary>
        /// Adding an child element on collection changed
        /// </summary>
        Add = 0,

        /// <summary>
        ///  Removing a child element
        /// </summary>
        Remove = 1
    }

    public interface IAdapter
    {
        void ReleaseResource();
        void Map(object source, object destination, int index, AdaptingOptions option);
        void Map(object source, object destination, string propertyName);
        TDestination Map<TSource, TDestination>(TSource source, TDestination destination, bool HoldReference);
    }

    [ProtoContract(ImplicitFields=ImplicitFields.AllPublic)]
    public static class AnatomiesAnatomicalRegionHelper
    {
        public static List<string> GetAnatomies()
        {
            return new List<string>();
        }
    }

    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    public static class ResourceUtility
    {
        public static List<string> GetLateralities()
        {
            return new List<string>();
        }

        public static string GetLateralityDisplayToText(string lat)
        {
            return lat;
        }
    }

    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    public class GeneralConfiguration: IDisposable
    {
        public string PatientWeightUnit = "bla";

        public void Dispose()
        {
            ;
        }
    }

    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    public class ECModel
    {
        public class ConfirmReviewType
        {

        }
    }

    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    public enum SmartPlanType
    {
        AWPLAN_SMARTPLAN_TYPE_NONE = 0
    }

    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    public enum GeometryOrientation
    {
        Undefined=0
    }

    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    public enum SortAttributes
    {
        /// <summary>
        /// Sort using image type
        /// </summary>
        IMAGE_TYPE,

        /// <summary>
        /// Sort using slice number
        /// </summary>
        SLICE_NUMBER,

        /// <summary>
        /// Sort using echo number
        /// </summary>
        ECHO_NUMBER,

        /// <summary>
        /// Sort using phase number
        /// </summary>
        PHASE_NUMBER,

        /// <summary>
        /// Sort using dynamic scan number
        /// </summary>
        DYNAMIC_SCAN,

        /// <summary>
        /// Sort using chemical shift
        /// </summary>
        CHEMICAL_SHIFT,

        /// <summary>
        /// Sort on diffusion
        /// </summary>
        DIFF_B_VALUE_NO

    };

    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    public enum SortOrder
    {
        /// <summary>
        /// Display images using Ascending order
        /// </summary>
        ASCENDING,

        /// <summary>
        /// Display images using Descending order
        /// </summary>
        DESCENDING
    };

    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    public enum SettingsCell
    {
        /// <summary>
        /// If no setting needs to be displayed
        /// </summary>
        Nothing,
        /// <summary>
        /// If User start needs to be displayed or not.
        /// </summary>
        UserStartRequired,
        /// <summary>
        /// Contrast needs to be displayed or not.
        /// </summary>
        Contrast,
        /// <summary>
        /// If Manual is selected
        /// </summary>
        Manual,
        /// <summary>
        /// Breathold information
        /// </summary>
        Breathold,
        /// <summary>
        /// If the contrast syringe is confirmed
        /// </summary>
        CipSyringeConfirmed,
        /// <summary>
        /// If the contrast syringe is ignored
        /// </summary>
        CipSyringeIgnore
    }
    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    public enum EditFields
    {
        /// <summary>
        /// Cancelling the edit mode or the 
        /// edit mode is being exited.
        /// </summary>
        None,
        /// <summary>
        /// Settings field in edit mode
        /// </summary>
        Settings,
        /// <summary>
        /// Name field is in edit mode
        /// </summary>
        Name,
        /// <summary>
        /// Geometry field is in edit mode
        /// </summary>
        GeometryName,
        /// <summary>
        /// Geo link field is in edit mode
        /// </summary>
        GeoLink,
        /// <summary>
        /// Laterality field is in edit mode
        /// </summary>
        Laterality
    }

}
