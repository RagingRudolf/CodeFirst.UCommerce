using NHibernate;

using RagingRudolf.CodeFirst.UCommerce.Core.Attributes;

namespace RagingRudolf.CodeFirst.UCommerce.Core.Handlers
{
	public class CampaignDefinitionHandler : BaseDefinitionHandler<CampaignDefinitionAttribute>
	{
		public CampaignDefinitionHandler(ISession session)
			: base(session)
		{
		}

		public override string DefinitionType
		{
			get { return Constants.DefinitionType.CampaignItem; }
		}
	}
}