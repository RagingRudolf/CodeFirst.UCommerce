using System;
using System.Collections.Generic;
using NHibernate;
using RagingRudolf.UCommerce.CodeFirst.Core.Attributes.Shared;
using RagingRudolf.UCommerce.CodeFirst.Core.Creators;
using RagingRudolf.UCommerce.CodeFirst.Core.Extensions;
using UCommerce.EntitiesV2;
using UCommerce.Infrastructure;

namespace RagingRudolf.UCommerce.CodeFirst.Core.Factories
{
    public class DefinitionCreatorFactory : IDefinitionCreatorFactory, IDisposable
    {
        private readonly ISession _session;
        private readonly IDictionary<BuiltInDefinitionType, ICreator> _definitionCreators; 

        public DefinitionCreatorFactory()
            : this(ObjectFactory.Instance.Resolve<ISessionProvider>().GetSession())
        {
        }

        public DefinitionCreatorFactory(ISession session)
        {
            if (session == null) throw new ArgumentNullException("session");

            _session = session;
            
            var definitionCreator = new DefinitionCreator(session);
            var productDefinitionCreator = new ProductDefinitionCreator(session);
            var dataTypeCreator = new DataTypeCreator(session);

            _definitionCreators = new Dictionary<BuiltInDefinitionType, ICreator>
            {
                { BuiltInDefinitionType.CampaignItem, definitionCreator },
                { BuiltInDefinitionType.Category, definitionCreator },
                { BuiltInDefinitionType.PaymentMethod, definitionCreator },
                { BuiltInDefinitionType.Product, productDefinitionCreator },
                { BuiltInDefinitionType.DataType, dataTypeCreator }
            };
        }

        public void Create(Type type)
        {
            var attribute = type.AssertGetAttribute<DefinitionAttribute>();

            ICreator definitionCreator;
            if (!_definitionCreators.TryGetValue(attribute.DefinitionType, out definitionCreator))
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