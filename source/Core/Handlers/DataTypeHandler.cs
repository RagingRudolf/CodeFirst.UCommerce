using System;
using NHibernate;
using RagingRudolf.CodeFirst.UCommerce.Core.Attributes.DataType;
using RagingRudolf.CodeFirst.UCommerce.Core.Extensions;

namespace RagingRudolf.CodeFirst.UCommerce.Core.Handlers
{
	public class DataTypeHandler : DataTypeHandlerBase
	{
		private readonly ISession _session;

		public DataTypeHandler(ISession session) 
			: base(session)
		{
			_session = session;
		}

		public override bool CanHandle(Type type)
		{
			if (!type.IsDefined(typeof(DataTypeAttribute), false))
				return false;

			var attribute = type.AssertGetCustomAttribute<DataTypeAttribute>();

			return !StringComparer.OrdinalIgnoreCase.Equals(attribute.DefinitionName, BuiltInDataTypes.Enum.ToString());
		}

		public override void Handle(Type type)
		{
			_session.SaveOrUpdate(GetDataTypeEntity(type));
		}
	}
}