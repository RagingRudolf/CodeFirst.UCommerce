using System;
using System.Collections.Generic;
using NHibernate;
using RagingRudolf.UCommerce.CodeFirst.Core.Attributes;
using RagingRudolf.UCommerce.CodeFirst.Core.Creators;
using RagingRudolf.UCommerce.CodeFirst.Core.Extensions;
using UCommerce.EntitiesV2;
using UCommerce.Infrastructure;

namespace RagingRudolf.UCommerce.CodeFirst.Core.Factories
{
    public class DefinitionCreatorFactory : IDefinitionCreatorFactory, IDisposable
    {
        private readonly ISession _session;
        private readonly IDictionary<string, IDefinitionCreator> _definitionCreators; 

        public DefinitionCreatorFactory()
            : this(ObjectFactory.Instance.Resolve<ISessionProvider>().GetSession())
        {
        }

        public DefinitionCreatorFactory(ISession session)
        {
            if (session == null) throw new ArgumentNullException("session");

            _session = session;
            _definitionCreators = new Dictionary<string, IDefinitionCreator>
            {
                { typeof (DefinitionAttribute).Name, new DefinitionCreator(_session) }
            };
        }

        public void Create(Type type)
        {
            var attribute = type.AssertGetAttribute<CodeFirstAttribute>();

            IDefinitionCreator definitionCreator;
            if (!_definitionCreators.TryGetValue(attribute.GetType().Name, out definitionCreator))
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