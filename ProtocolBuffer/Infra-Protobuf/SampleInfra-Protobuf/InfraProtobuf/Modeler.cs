#region  Copyright 2014 Koninklijke Philips N.V.
// 
// All rights are reserved. Reproduction or transmission in whole or in part, in
// any form or by any means, electronic, mechanical or otherwise, is prohibited
// without the prior written permission of the copyright owner.
#endregion
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Philips.PmsMR.Protobuf.ModelReflection
{
    /// <summary>
    /// Modeler for creating Protobuf.Net data model from plain old data classes.
    /// </summary>
    /// <remarks>
    /// <see cref="Philips.PmsMR.Protobuf.ModelReflection.TypeMapper">Type mapper</see> instance has created
    /// a collection of types that need to be converted into Protobu.Net datamodel types.
    /// The modeler class handles this conversion by invoking runtime model compiler.
    /// </remarks>
    public class Modeler
    {
        /// <summary>
        /// Default ctor.
        /// </summary>
        public Modeler()
        {
            model = ProtoBuf.Meta.TypeModel.Create();

            // We do not need to support old v1, where a bad design choice of zero defaults were used.
            model.UseImplicitZeroDefaults = false;
            model.AutoAddMissingTypes = false;
        }

        /// <summary>
        /// Creates a protobuf model for the given C# datamodel.
        /// </summary>
        /// <param name="typeMapper"></param>
        internal void CreateModel(TypeMapper typeMapper)
        {            
            TypeMapper = typeMapper;

            var tag = typeMapper.Result.DataModelTag;
            if ((tag.DataModelOptions & Philips.PmsMR.Protobuf.DataModel.DataModelOptionTypes.GeneratedFromProtoFile) != 0)
            {
                CreateModelFromGeneratedTypes(tag as DataModel.IProtoFileTag);
            }
            else
            {
                CreateModelFromClasses(tag as DataModel.INamespaceTag);
            }

        }

        internal void CreateModelFromGeneratedTypes(DataModel.IProtoFileTag protoFileTag)
        {
            foreach (var type in protoFileTag.GetTypes())
            {
                model.Add(type, true);
            }
        }

        internal void CreateModelFromClasses(DataModel.INamespaceTag namespaceTag)
        {
            var mappingResult = TypeMapper.Result as ReflectedMappingResult;
            var tag = namespaceTag;

            var classModeler = new ClassModeler(model, mappingResult);

            var linker = new DerivedParentChildLinker(mappingResult, netTypeProtobufTypeMap);
            var extensibleRoot = tag.ExtensibleRootInfo.ExtensibleRoot;

            // We remove duplicate entries -
            // each field is a single value in the map type with multiple fields would get mentioned may times.
            foreach (var type in mappingResult.TypeMap.SelectMany(x => x.Value).Distinct().OrderBy(x => x.FullName))
            {
                var fieldItems = new List<KeyValuePair<Int32, NamespaceId.FieldItem>>();
                foreach (var pair in mappingResult.TypeMap)
                {
                    var namespaceId = pair.Key;
                    var usingTypes = pair.Value;
                    var fieldItem = namespaceId.Item as NamespaceId.FieldItem;
                    if (fieldItem != null && usingTypes.Contains(type))
                    {
                        fieldItems.Add(new KeyValuePair<Int32, NamespaceId.FieldItem>(namespaceId.Id, fieldItem));
                    }
                }

                var protobufType = classModeler.AddToModel(type, fieldItems);
                netTypeProtobufTypeMap.Add(type, protobufType);

                IEnumerable<Type> derivedChain;
                if (mappingResult.DerivationChain.TryGetValue(type, out derivedChain))
                {
                    if (derivedChain.Last() != type)
                    {
                        throw new ApplicationException("Derivation chain should start with the root base class and end with our type: " + type);
                    }

                    if ((tag.DataModelOptions & Philips.PmsMR.Protobuf.DataModel.DataModelOptionTypes.UsesPolymorphicMessages) != 0 &&
                        derivedChain.First() == extensibleRoot)
                    {
                        // If we need to use Protobuf.NET polymorphism, we need to register
                        // subclasses to parent classes.
                        var chainDepth = derivedChain.Count();
                        if (chainDepth >= 2)
                        {
                            var parentInChain = derivedChain.ElementAt(chainDepth - 2);
                            linker.RegisterLink(type, parentInChain);
                        }
                    }
                }
            }

            // Linker creates the inheritance hierarchy for the derived classes,
            // based on the registration done above,
            // so that the children will contain all the fields from the ancestor classes.
            linker.FormRegisteredLinks();
        }

        /// <summary>
        /// Access an exporter instance.
        /// </summary>
        public Exporter Exporter
        {
            get
            {
                return new Exporter(RuntimeTypeModel);
            }
        }

        /// <summary>
        /// Access to the compiled Protobuf.Net datamodel.
        /// </summary>
        /// <returns></returns>
        internal CompiledModelContainer PrecompileModels()
        {
            model.CompileInPlace();
            return new CompiledModelContainer(model);
        }

        internal ProtoBuf.Meta.RuntimeTypeModel RuntimeTypeModel
        {
            get { return model; }
        }

        internal TypeMapper TypeMapper { get; set; }

        private class DerivedParentChildLinker
        {
            public DerivedParentChildLinker(
                ReflectedMappingResult mappingResult,
                Dictionary<Type, ProtoBuf.Meta.MetaType> netTypeProtobufTypeMap)
            {
                this.mappingResult = mappingResult;
                this.netTypeProtobufTypeMap = netTypeProtobufTypeMap;
            }

            public void RegisterLink(Type subType, Type immediateParentType) {
                registered.Add(new KeyValuePair<Type,Type>(subType, immediateParentType));
            }

            public void FormRegisteredLinks() {
                foreach (var pair in registered) {
                    ProtoBuf.Meta.MetaType protobufParent;
                    if (!netTypeProtobufTypeMap.TryGetValue(pair.Value, out protobufParent))
                    {
                        throw new ApplicationException("Cannot form a link, protobuf parent for " + pair.Value + " does not exist");
                    }
                    var childType = pair.Key;
                    KeyValuePair<int, ReflectedMappingResult.IdentificationType> idIdentificationPair;
                    if (mappingResult.TypeToIdMap.TryGetValue(childType, out idIdentificationPair))
                    {
                        protobufParent.AddSubType(idIdentificationPair.Key, childType);
                    }
                    else
                    {
                        throw new ApplicationException("Cannot form a link from " + protobufParent + " to " + childType);
                    }
                }
            }

            private readonly List<KeyValuePair<Type,Type>> registered = new List<KeyValuePair<Type,Type>>();
            private readonly ReflectedMappingResult mappingResult;
            private readonly Dictionary<Type, ProtoBuf.Meta.MetaType> netTypeProtobufTypeMap;
        }

        private readonly ProtoBuf.Meta.RuntimeTypeModel model;
        private readonly Dictionary<Type, ProtoBuf.Meta.MetaType> netTypeProtobufTypeMap = new Dictionary<Type, ProtoBuf.Meta.MetaType>();
    }
}
