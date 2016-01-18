using System;
using System.Collections.Generic;
using NHibernate;
using RagingRudolf.UCommerce.CodeFirst.Core.Attributes.Shared;
using RagingRudolf.UCommerce.CodeFirst.Core.Creators;
using RagingRudolf.UCommerce.CodeFirst.Core.Extensions;
using UCommerce.EntitiesV2;

namespace RagingRudolf.UCommerce.CodeFirst.Core.Factories
{
    public class DefinitionCreatorFactory : IDefinitionCreatorFactory
    {
        private readonly ISession _session;
        private readonly ITransaction _transaction;

        private readonly IDictionary<BuiltInDefinitionType, ICreator> _definitionCreators; 

        public DefinitionCreatorFactory(ISessionProvider sessionProvider)
        {
            if (sessionProvider == null) throw new ArgumentNullException(nameof(sessionProvider));
            _session = sessionProvider.GetSession();
            _transaction = _session.BeginTransaction();

            var definitionCreator = new DefinitionCreator(_session);
            var productDefinitionCreator = new ProductDefinitionCreator(_session);
            var dataTypeCreator = new DataTypeCreator(_session);

            _definitionCreators = new Dictionary<BuiltInDefinitionType, ICreator>
            {
                { BuiltInDefinitionType.CampaignItem, definitionCreator },
                { BuiltInDefinitionType.Category, definitionCreator },
                { BuiltInDefinitionType.PaymentMethod, definitionCreator },
                { BuiltInDefinitionType.Product, productDefinitionCreator },
                { BuiltInDefinitionType.DataType, dataTypeCreator }
            };
        }

        public void Cancel()
        {
            _transaction.Rollback();
        }

        public void Create(Type type)
        {
            var attribute = type.AssertGetAttribute<DefinitionAttribute>();

            ICreator definitionCreator;
            if (!_definitionCreators.TryGetValue(attribute.DefinitionType, out definitionCreator))
                throw new InvalidOperationException(
                    $"Cannot find any DefinitionCreator for type '{type.Name}'");

            definitionCreator.CreateOrUpdate(type);
        }

        public void Dispose()
        {
            if (!_transaction.WasRolledBack)
                _transaction.Commit();
        }
    }
}