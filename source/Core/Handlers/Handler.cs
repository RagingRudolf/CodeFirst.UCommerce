using System;
using NHibernate;

namespace RagingRudolf.UCommerce.CodeFirst.Core.Handlers
{
	public abstract class Handler<TDefinition> : IHandler
	{
		protected readonly ISession Session;

		protected Handler(ISession session)
		{
			Session = session;
		}
		
		public virtual void Handle(Type type)
		{
			TDefinition definition = HandleDefinition(type);
			TDefinition withFields = HandleFieldTypes(type, definition);

			Session.SaveOrUpdate(withFields);
		}

		public abstract bool CanHandle(Type type);

		protected abstract TDefinition HandleDefinition(Type type);
		protected abstract TDefinition HandleFieldTypes(Type type, TDefinition definition);
	}
}