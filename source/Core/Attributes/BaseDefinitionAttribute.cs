namespace RagingRudolf.CodeFirst.UCommerce.Core.Attributes
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