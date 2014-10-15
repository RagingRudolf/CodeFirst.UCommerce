using NHibernate;

using RagingRudolf.CodeFirst.UCommerce.Core.Attributes.Category;

namespace RagingRudolf.CodeFirst.UCommerce.Core.Handlers
{
	public class CategoryDefinitionHandler : BaseDefinitionHandler<CategoryDefinitionAttribute>
	{
		public CategoryDefinitionHandler(ISession session) 
			: base(session)
		{
		}

		public override string DefinitionType
		{
			get { return Constants.DefinitionType.Category; }
		}
	}
}