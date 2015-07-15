using System;

namespace RagingRudolf.UCommerce.CodeFirst.Core.Attributes
{
	[AttributeUsage(AttributeTargets.Class)]
	public class CampaignDefinitionAttribute : BaseDefinitionAttribute
	{
		public CampaignDefinitionAttribute(string name) 
			: base(name)
		{
		}
	}
}