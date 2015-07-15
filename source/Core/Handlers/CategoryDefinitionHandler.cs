using NHibernate;
using RagingRudolf.UCommerce.CodeFirst.Core.Attributes;

namespace RagingRudolf.UCommerce.CodeFirst.Core.Handlers
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