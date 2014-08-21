using System;

using NHibernate;

using RagingRudolf.CodeFirst.UCommerce.Core.Attributes;
using RagingRudolf.CodeFirst.UCommerce.Core.Extensions;

using UCommerce.EntitiesV2;

namespace RagingRudolf.CodeFirst.UCommerce.Core.Handlers
{
	public class CampaignDefinitionHandler : IHandler
	{
		private readonly ISession _session;

		public CampaignDefinitionHandler(ISession session)
		{
			_session = session;
		}

		public bool CanHandle(Type type)
		{
			return type.IsDefined(typeof (CampaignDefinitionAttribute), false);
		}

		public void Handle(Type type)
		{
			Definition definition = HandleDefinition(_session, type);

			_session.SaveOrUpdate(definition);
		}

		private static Definition HandleDefinition(ISession session, Type type)
		{
			var attribute = type.AssertGetCustomAttribute<CampaignDefinitionAttribute>();

			string name = attribute.Name.IsNotEmpty()
				? attribute.Name
				: type.Name;
			
			Definition definition = session
				.QueryOver<Definition>()
				.Fetch(x => x.DefinitionFields).Eager
				.Where(x => x.Name == name)
				.SingleOrDefault();

			if (definition == null)
			{
				string definitionTypeName = Constants.DefinitionType.CampaignItem;

				DefinitionType campaignDefinitionType = session
					.QueryOver<DefinitionType>()
					.Where(x => x.Name == definitionTypeName)
					.SingleOrDefault();

				if (campaignDefinitionType == null)
					throw new InvalidOperationException(
						string.Format("Could not find definition type '{0}'", definitionTypeName));

				definition = new Definition
				{
					Name = name,
					DefinitionType = campaignDefinitionType
				};
			}

			definition.Deleted = false;
			definition.Description = attribute.Description.IsNotEmpty()
				? attribute.Description
				: string.Empty;

			return definition;
		}
	}
}