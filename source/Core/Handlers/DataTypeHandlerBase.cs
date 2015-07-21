using System;
using System.Linq;
using NHibernate;
using RagingRudolf.UCommerce.CodeFirst.Core.Attributes.DataType;
using RagingRudolf.UCommerce.CodeFirst.Core.Extensions;
using UCommerce.EntitiesV2;

namespace RagingRudolf.UCommerce.CodeFirst.Core.Handlers
{
	public abstract class DataTypeHandlerBase : IHandler
	{
		private readonly ISession _session;

		protected DataTypeHandlerBase(ISession session)
		{
			_session = session;
		}

		public abstract bool CanHandle(Type type);
		public abstract void Handle(Type type);

		public DataType GetDataTypeEntity(Type type)
		{
			var attribute = type.AssertGetCustomAttribute<DataTypeAttribute>();

			string name = attribute.Name.IsNotEmpty()
				? attribute.Name
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
			dataType.DefinitionName = attribute.DefinitionName;
			dataType.Nullable = attribute.Nullable;
			dataType.ValidationExpression = attribute.ValidationExpression ?? string.Empty;

			return dataType;
		}
	}
}