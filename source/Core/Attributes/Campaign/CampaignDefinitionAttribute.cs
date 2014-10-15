using System;

using RagingRudolf.CodeFirst.UCommerce.Core.Attributes.Shared;

namespace RagingRudolf.CodeFirst.UCommerce.Core.Attributes.Campaign
{
	[AttributeUsage(AttributeTargets.Class, Inherited = true)]
	public class CampaignDefinitionAttribute : BaseDefinitionAttribute
	{
		public CampaignDefinitionAttribute(string name) 
			: base(name)
		{
		}
	}
}