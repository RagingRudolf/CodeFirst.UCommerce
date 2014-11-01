using System;
using NHibernate;
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
			return type.IsDefined(typeof (DataTypeAttribute), false);
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
}