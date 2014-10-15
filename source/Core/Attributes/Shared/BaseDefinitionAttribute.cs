namespace RagingRudolf.CodeFirst.UCommerce.Core.Attributes.Shared
{
	public abstract class BaseDefinitionAttribute : UCommerceAttribute
	{
		protected BaseDefinitionAttribute(string name) 
			: base(name)
		{
		}

		public string Description { get; set; }
	}
}