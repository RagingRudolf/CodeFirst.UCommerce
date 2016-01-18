using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using NHibernate;
using RagingRudolf.UCommerce.CodeFirst.Core.Attributes.DataType;
using RagingRudolf.UCommerce.CodeFirst.Core.Attributes.Shared;
using RagingRudolf.UCommerce.CodeFirst.Core.Extensions;
using UCommerce.EntitiesV2;

namespace RagingRudolf.UCommerce.CodeFirst.Core.Creators
{
    public class DataTypeCreator : ICreator
    {
        private readonly ISession _session;

        public DataTypeCreator(ISession session)
        {
            if (session == null) throw new ArgumentNullException(nameof(session));

            _session = session;
        }

        public void CreateOrUpdate(Type type)
        {
            var dataTypeAttribute = type.AssertGetAttribute<DataTypeAttribute>();
            var dataType = EnsureDataType(type, dataTypeAttribute);
            var properties = type.GetPropertiesWithAttribute<EnumValueAttribute>().ToArray();

            if (properties.Any() && StringComparer.OrdinalIgnoreCase.Equals(dataTypeAttribute.DefinitionName, BuiltInDataType.Enum.ToString()))
            {
                foreach (var property in properties)
                {
                    AddMultilingualDescription(property, CreateField(dataType, property), dataType);
                }
            }

            _session.SaveOrUpdate(dataType);
        }

        private static void AddMultilingualDescription(PropertyInfo property, DataTypeEnum dataTypeEnum, DataType dataType)
        {
            foreach (var description in property.GetCustomAttributes<LanguageAttribute>())
            {
                var enumDescription = dataTypeEnum.DataTypeEnumDescriptions
                    .EmptyIfNull()
                    .SingleOrDefault(x => x.CultureCode == description.Language);

                if (enumDescription == null)
                {
                    enumDescription = new DataTypeEnumDescription
                    {
                        CultureCode = description.Language
                    };
                    dataTypeEnum.AddDescription(enumDescription);
                }

                enumDescription.DisplayName = description.DisplayName;
                enumDescription.Description = description.Description ?? string.Empty;
            }

            dataType.AddDataTypeEnum(dataTypeEnum);
        }

        private static DataTypeEnum CreateField(DataType dataType, PropertyInfo property)
        {
            var dataTypeEnum = dataType.DataTypeEnums
                .EmptyIfNull()
                .SingleOrDefault(x => x.Value == property.Name);

            if (dataTypeEnum == null)
            {
                dataTypeEnum = new DataTypeEnum
                {
                    DataTypeEnumDescriptions = new List<DataTypeEnumDescription>(),
                    Value = property.Name
                };

                dataType.AddDataTypeEnum(dataTypeEnum);
            }

            return dataTypeEnum;
        }

        private DataType EnsureDataType(Type type, DataTypeAttribute dataTypeAttribute)
        {
            var name = dataTypeAttribute.Name.IsNotEmpty()
                ? dataTypeAttribute.Name
                : type.Name;
            var dataType = _session.QueryOver<DataType>()
                .Fetch(x => x.DataTypeEnums).Eager
                .Fetch(x => x.DataTypeEnums.First().DataTypeEnumDescriptions).Eager
                .Where(x => x.TypeName == name)
                .SingleOrDefault() ?? new DataType
                {
                    TypeName = name
                };

            dataType.Deleted = false;
            dataType.DefinitionName = dataTypeAttribute.DefinitionName;
            dataType.Nullable = dataTypeAttribute.Nullable;
            dataType.ValidationExpression = dataTypeAttribute.ValidationExpression ?? string.Empty;

            return dataType;
        }
    }
}