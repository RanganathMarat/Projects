using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Philips.PmsMR.Protobuf.DataModel
{

    /// <summary>
    /// Details about the data model implementation.
    /// </summary>
    [Flags]
    public enum DataModelOptionTypes
    {
        /// <summary>
        /// Unknown
        /// </summary>
        None = 0x00,
        /// <summary>
        /// If set, uses polymorphism with hierarchical messages.
        /// </summary>
        /// <remarks>
        /// If hierarchical messages are not used, leaf classes
        /// in .NET class hierarchy get populated with all fields
        /// from ancestor classes and Protobuf.NET class hierarchy is not used.
        /// If hierarchical messages are used, each subclass forms a message
        /// that declares only the new fields, and hierarchy is indicated
        /// by adding a message field to the parent class 
        /// (multiple children in hierarchy are indicated by multiple fields in
        /// the parent, of which only one is instantiated at a time).
        /// </remarks>
        UsesPolymorphicMessages = 0x01,
        /// <summary>
        /// Data model is defined by a proto-file.
        /// </summary>
        /// <remarks>
        /// Instead of using C# as the definition of a datamodel,
        /// this particular instance uses a protofile-generated classes as the model.
        /// This is useful for the cases where the model is provided by an external party,
        /// or initially developed to be a cross-platform model (C++ as a first-class citizen).
        /// </remarks>
        GeneratedFromProtoFile = 0x02,
    }

    /// <summary>
    /// Identifier for a protobuf data model.
    /// </summary>
    /// <remarks>
    /// The tag identifies C# classes that implement the data model.
    /// </remarks>
    public interface IDataModelTag
    {
        /// <summary>
        /// Contained protobuf-convertible types
        /// </summary>
        /// <remarks>
        /// A simple implementation can use reflection to list all public classes from an assembly datamodel namespace,
        /// and provide a root namespace string to weed out classes that are not within the given namespace.
        /// </remarks>
        /// <returns>Array of types that are valid protobuf message classes.</returns>
        Type[] GetTypes();

        /// <summary>
        /// Details about the data model.
        /// </summary>
        DataModelOptionTypes DataModelOptions { get; }
    }
}
