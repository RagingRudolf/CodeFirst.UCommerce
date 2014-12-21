using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using NHibernate;
using RagingRudolf.CodeFirst.UCommerce.Core.Attributes;
using RagingRudolf.CodeFirst.UCommerce.Core.Attributes.DataType;
using RagingRudolf.CodeFirst.UCommerce.Core.Extensions;
using UCommerce.EntitiesV2;

namespace RagingRudolf.CodeFirst.UCommerce.Core.Handlers
{
	public class EnumDataTypeHandler : DataTypeHandlerBase
	{
		private readonly ISession _session;

		public EnumDataTypeHandler(ISession session) 
			: base(session)
		{
			_session = session;
		}

		public override bool CanHandle(Type type)
		{
			if (!type.IsDefined(typeof(DataTypeAttribute), false))
				return false;

			var attribute = type.AssertGetCustomAttribute<DataTypeAttribute>();

			return StringComparer.OrdinalIgnoreCase.Equals(attribute.DefinitionName, BuiltInDataTypes.Enum.ToString());
		}

		public override void Handle(Type type)
		{
			var dataType = GetDataTypeEntity(type);
			var properties = type.GetAttributedProperties<EnumValueAttribute>();
			
			foreach (var property in properties)
			{
				var enumDataType =	dataType.DataTypeEnums
					.EmptyIfNull()
					.SingleOrDefault(x => x.Value == property.Name) ?? new DataTypeEnum
					{
						DataType = dataType,
						DataTypeEnumDescriptions = new List<DataTypeEnumDescription>(),
						Value = property.Name
					};

				IEnumerable<LanguageDescriptionAttribute> descriptions = property.GetCustomAttributes<LanguageDescriptionAttribute>();

				foreach (var description in descriptions)
				{
					DataTypeEnumDescription enumDescription = enumDataType.DataTypeEnumDescriptions
						.SingleOrDefault(x => x.CultureCode == description.Language);

					if (enumDescription == null)
					{
						enumDescription = description.AsEnumDescription(enumDataType);
						enumDataType.AddDescription(enumDescription);
					}

					enumDescription.DisplayName = description.DisplayName;
					description.Description = description.Description ?? string.Empty;
				}

				dataType.AddDataTypeEnum(enumDataType);
			}

			_session.SaveOrUpdate(dataType);
		}
	}
}