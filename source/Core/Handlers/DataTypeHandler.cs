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
	public class DataTypeHandler : IHandler
	{
		private readonly ISession _session;

		public DataTypeHandler(ISession session)
		{
			_session = session;
		}

		public bool CanHandle(Type type)
		{
			if (!type.IsDefined(typeof(DataTypeAttribute), false))
				return false;

			var attribute = type.AssertGetCustomAttribute<DataTypeAttribute>();

			return !StringComparer.OrdinalIgnoreCase.Equals(attribute.DefinitionName, BuiltInDataTypes.Enum.ToString());
		}

		public void Handle(Type type)
		{
			var attribute = type.AssertGetCustomAttribute<DataTypeAttribute>();

			string name = attribute.Name.IsNotEmpty()
				? attribute.Name
				: type.Name;

			var dataType = _session.QueryOver<DataType>()
				.Where(x => x.TypeName == name)
				.SingleOrDefault() ?? new DataType
				{
					TypeName = name
				};

			dataType.Deleted = false;
			dataType.DefinitionName = attribute.DefinitionName;
			dataType.Nullable = attribute.Nullable;
			dataType.ValidationExpression = attribute.ValidationExpression ?? string.Empty;

			_session.SaveOrUpdate(dataType);
		}
	}

	public class EnumDataTypeHandler : IHandler
	{
		private readonly ISession _session;

		public EnumDataTypeHandler(ISession session)
		{
			_session = session;
		}

		public bool CanHandle(Type type)
		{
			if (!type.IsDefined(typeof(DataTypeAttribute), false))
				return false;

			var attribute = type.AssertGetCustomAttribute<DataTypeAttribute>();

			return StringComparer.OrdinalIgnoreCase.Equals(attribute.DefinitionName, BuiltInDataTypes.Enum.ToString());
		}

		public void Handle(Type type)
		{
			var attribute = type.AssertGetCustomAttribute<DataTypeAttribute>();

			string name = attribute.Name.IsNotEmpty()
				? attribute.Name
				: type.Name;

			var dataType = _session.QueryOver<DataType>()
				.Where(x => x.TypeName == name)
				.SingleOrDefault() ?? new DataType
				{
					TypeName = name
				};

			dataType.Deleted = false;
			dataType.DefinitionName = attribute.DefinitionName;
			dataType.Nullable = attribute.Nullable;
			dataType.ValidationExpression = attribute.ValidationExpression ?? string.Empty;

			var properties = type.GetAttributedProperties<EnumValueAttribute>();
			
			foreach (var property in properties)
			{
				var enumDataType = _session.QueryOver<DataTypeEnum>()
					.Where(x => x.Value == property.Name && x.DataType == dataType)
					.SingleOrDefault() ?? new DataTypeEnum
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