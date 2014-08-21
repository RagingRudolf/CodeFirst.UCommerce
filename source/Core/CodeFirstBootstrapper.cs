using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using NHibernate;

using RagingRudolf.CodeFirst.UCommerce.Core.Configuration;
using RagingRudolf.CodeFirst.UCommerce.Core.Extensions;
using RagingRudolf.CodeFirst.UCommerce.Core.Handlers;

using UCommerce.EntitiesV2;
using UCommerce.Infrastructure;

namespace RagingRudolf.CodeFirst.UCommerce.Core
{
	public static class CodeFirstBootstrapper
	{
		public static void Initialize()
		{
			IConfigurationProvider configurationProvider = new ConfigurationProvider();

			if (!configurationProvider.Synchronize)
				return;

			Assembly assembly = configurationProvider.GetAssembly();
			IEnumerable<Type> sorted = assembly.GetUCommerceDefinitions();
			
			ISessionProvider sessionProvider = ObjectFactory.Instance.Resolve<ISessionProvider>();
			ISession session = sessionProvider.GetSession();

			IHandler[] handlers =
			{
				new CategoryDefinitionHandler(session), 
				new ProductDefinitionHandler(session),
				new CampaignDefinitionHandler(session)
			};

			foreach (Type type in sorted)
			{
				IHandler handler = handlers.FirstOrDefault(x => x.CanHandle(type));

				if (handler == null)
					throw new InvalidOperationException(
						string.Format("Can not find a handler for type '{0}'", type));

				handler.Handle(type);
			}

			session.Flush();
		}
	}
}