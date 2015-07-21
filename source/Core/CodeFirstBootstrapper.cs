﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using NHibernate;
using RagingRudolf.UCommerce.CodeFirst.Core.Attributes;
using RagingRudolf.UCommerce.CodeFirst.Core.Configuration;
using RagingRudolf.UCommerce.CodeFirst.Core.Extensions;
using RagingRudolf.UCommerce.CodeFirst.Core.Handlers;
using UCommerce.EntitiesV2;
using UCommerce.Infrastructure;

namespace RagingRudolf.UCommerce.CodeFirst.Core
{
	public static class CodeFirstBootstrapper
	{
		public static void Initialize()
		{
			IConfigurationProvider configurationProvider = new ConfigurationProvider();

			if (!configurationProvider.Synchronize)
				return;

			Assembly assembly = configurationProvider.GetAssembly();
			IEnumerable<Type> types = assembly.GetTypes()
                .EmptyIfNull()
                .WithAttribute<CodeFirstAttribute>();

			ISessionProvider sessionProvider = ObjectFactory.Instance.Resolve<ISessionProvider>();
			ISession session = sessionProvider.GetSession();

			IHandler[] handlers =
			{
				new CategoryDefinitionHandler(session), 
				new ProductDefinitionHandler(session),
				new CampaignDefinitionHandler(session),
				new PaymentMethodDefinitionHandler(session), 
			};

			foreach (Type type in types)
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