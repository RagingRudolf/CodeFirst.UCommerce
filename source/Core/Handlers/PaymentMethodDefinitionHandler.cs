using NHibernate;
using RagingRudolf.UCommerce.CodeFirst.Core.Attributes;

namespace RagingRudolf.UCommerce.CodeFirst.Core.Handlers
{
	public class PaymentMethodDefinitionHandler : BaseDefinitionHandler<PaymentMethodDefinitionAttribute>
	{
		public PaymentMethodDefinitionHandler(ISession session) 
			: base(session)
		{
		}

		public override string DefinitionType
		{
			get { return Constants.DefinitionType.PaymentMethod; }
		}
	}
}