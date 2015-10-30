using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using RagingRudolf.UCommerce.CodeFirst.Core.Attributes.Shared;
using RagingRudolf.UCommerce.CodeFirst.Core.Extensions;
using RagingRudolf.UCommerce.CodeFirst.Core.Factories;
using UCommerce.EntitiesV2;
using UCommerce.Infrastructure;

namespace RagingRudolf.UCommerce.CodeFirst.Core.Bootstrapping
{
    public class AssemblyScanBootstrap : ICanBootstrap
    {
        private readonly ISessionProvider _sessionProvider;

        public AssemblyScanBootstrap(ISessionProvider sessionProvider)
        {
            if (sessionProvider == null) throw new ArgumentNullException(nameof(sessionProvider));
            _sessionProvider = sessionProvider;
        }

        public AssemblyScanBootstrap()
            : this(ObjectFactory.Instance.Resolve<ISessionProvider>())
        {
        }

        public void Initialize()
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            var codeFirstTypes = new List<Type>();

            foreach (Assembly assembly in assemblies)
            {
                IEnumerable<Type> types = assembly.GetTypes()
                    .EmptyIfNull()
                    .WithAttribute<CodeFirstAttribute>()
                    .ToList();

                codeFirstTypes.AddRange(types);
            }

            using (var factory = new DefinitionCreatorFactory(_sessionProvider))
            {
                foreach (var type in codeFirstTypes)
                    factory.Create(type);
            }
        }
    }
}