using NHibernate;

using RagingRudolf.CodeFirst.UCommerce.Core.Attributes;

namespace RagingRudolf.CodeFirst.UCommerce.Core.Handlers
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