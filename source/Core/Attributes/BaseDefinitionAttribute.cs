namespace RagingRudolf.UCommerce.CodeFirst.Core.Attributes
{
	public abstract class BaseDefinitionAttribute : CodeFirstAttribute
	{
		protected BaseDefinitionAttribute(string name) 
			: base(name)
		{
		}

		public string Description { get; set; }
	}
}