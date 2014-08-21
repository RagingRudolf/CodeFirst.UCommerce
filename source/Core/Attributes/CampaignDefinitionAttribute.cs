using System;

namespace RagingRudolf.CodeFirst.UCommerce.Core.Attributes
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