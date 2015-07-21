using System;
using System.Collections.Generic;
using System.Linq;
using NHibernate;
using RagingRudolf.UCommerce.CodeFirst.Core.Handlers;
using UCommerce.EntitiesV2;
using UCommerce.Infrastructure;

namespace RagingRudolf.UCommerce.CodeFirst.Core.Factories
{
    public class DefinitionFactory : IDefinitionFactory, IDisposable
    {
        private readonly ISession _session;
        private readonly IEnumerable<IHandler> _handlers;

        public DefinitionFactory()
            : this(ObjectFactory.Instance.Resolve<ISessionProvider>().GetSession())
        {
        }

        public DefinitionFactory(ISession session)
        {
            if (session == null) throw new ArgumentNullException("session");

            _session = session;
            _handlers = new IHandler[]
            {
                new DataTypeHandler(_session),
                new EnumDataTypeHandler(_session), 
                new CategoryDefinitionHandler(_session), 
                new ProductDefinitionHandler(_session),
                new CampaignDefinitionHandler(_session),
                new PaymentMethodDefinitionHandler(_session)
            };
        }

        public void Create(Type type)
        {
            var handler = _handlers.FirstOrDefault(x => x.CanHandle(type));

            if (handler == null)
                throw new InvalidOperationException(
                    string.Format("Can not find a handler for type '{0}'", type));

            handler.Handle(type);
        }

        public void Dispose()
        {
            _session.Flush();
        }
    }
}