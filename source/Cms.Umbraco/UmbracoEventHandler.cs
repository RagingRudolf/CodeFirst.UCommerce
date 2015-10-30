﻿using RagingRudolf.UCommerce.CodeFirst.Core.Bootstrapping;
using Umbraco.Core;

namespace RagingRudolf.UCommerce.CodeFirst.Cms.Umbraco
{
	public class UmbracoEventHandler : ApplicationEventHandler
	{
		protected override void ApplicationStarted(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
		{
            // Required ConfigurationSection in web.config
            new AssemblyByConfigurationBootstrap().Initialize();

            // Requires no further configuration
            new AssemblyScanBootstrap().Initialize();
		}
	}
}