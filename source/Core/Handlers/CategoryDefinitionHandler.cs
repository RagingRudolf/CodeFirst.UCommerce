using System;

using NHibernate;

using RagingRudolf.CodeFirst.UCommerce.Core.Attributes;
using RagingRudolf.CodeFirst.UCommerce.Core.Extensions;

using UCommerce.EntitiesV2;

namespace RagingRudolf.CodeFirst.UCommerce.Core.Handlers
{
	public class CategoryDefinitionHandler : IHandler
	{
		private readonly ISessionProvider _provider;

		public CategoryDefinitionHandler(ISessionProvider provider)
		{
			_provider = provider;
		}

		public bool CanHandle(Type type)
		{
			return type.IsDefined(typeof (CategoryDefinitionAttribute), false);
		}

		public void Handle(Type type)
		{
			var attribute = type.AssertGetAttribute<CategoryDefinitionAttribute>();

			string name = !string.IsNullOrWhiteSpace(attribute.Name)
				? attribute.Name
				: type.Name;

			ISession session = _provider.GetSession();
			var definition = session.QueryOver<Definition>()
				.Where(x => x.Name == name)
				.SingleOrDefault<Definition>();

			if (definition == null)
			{
				var categoryDefinitionType = session
					.QueryOver<DefinitionType>()
					.Where(x => x.Name == "Category")
					.SingleOrDefault<DefinitionType>();

				if (categoryDefinitionType == null)
					throw new InvalidOperationException();

				definition = new Definition
				{
					Name = name,
					DefinitionType = categoryDefinitionType
				};
			}

			definition.Deleted = false;
			definition.Description = !string.IsNullOrWhiteSpace(attribute.Description)
				? attribute.Description
				: string.Empty;

			session.Save(definition);
			session.Flush();
		}
	}
}