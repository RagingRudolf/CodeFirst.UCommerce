using NHibernate;
using RagingRudolf.UCommerce.CodeFirst.Core.Attributes;

namespace RagingRudolf.UCommerce.CodeFirst.Core.Handlers
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