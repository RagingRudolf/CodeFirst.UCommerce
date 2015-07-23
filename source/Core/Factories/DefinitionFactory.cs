using System;
using System.Collections.Generic;
using NHibernate;
using RagingRudolf.UCommerce.CodeFirst.Core.Creators;
using UCommerce.EntitiesV2;
using UCommerce.Infrastructure;

namespace RagingRudolf.UCommerce.CodeFirst.Core.Factories
{
    public class DefinitionFactory : IDefinitionFactory, IDisposable
    {
        private readonly ISession _session;
        private readonly IDictionary<string, IDefinitionCreator> _definitionCreators; 

        public DefinitionFactory()
            : this(ObjectFactory.Instance.Resolve<ISessionProvider>().GetSession())
        {
        }

        public DefinitionFactory(ISession session)
        {
            if (session == null) throw new ArgumentNullException("session");

            _session = session;
            _definitionCreators = new Dictionary<string, IDefinitionCreator>
            {
                { typeof (DefinitionCreator).Name, new DefinitionCreator(_session) }
            };
        }

        public void Create(Type type)
        {
            IDefinitionCreator definitionCreator;
            if (!_definitionCreators.TryGetValue(type.Name, out definitionCreator))
                throw new InvalidOperationException(
                    string.Format("Cannot find any DefinitionCreator for type '{0}'", type.Name));

            definitionCreator.CreateOrUpdate(type);
        }

        public void Dispose()
        {
            _session.Flush();
        }
    }
}