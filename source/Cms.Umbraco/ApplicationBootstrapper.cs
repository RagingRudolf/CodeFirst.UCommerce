using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using RagingRudolf.CodeFirst.UCommerce.Core;
using RagingRudolf.CodeFirst.UCommerce.Core.Configuration;
using RagingRudolf.CodeFirst.UCommerce.Core.Extensions;
using RagingRudolf.CodeFirst.UCommerce.Core.Handlers;
using Umbraco.Core;

namespace RagingRudolf.CodeFirst.UCommerce.Cms.Umbraco
{
	public class ApplicationBootstrapper : ApplicationEventHandler
	{
		protected override void ApplicationStarted(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
		{
			CodeFirstBootstrapper.Initialize();
		}
	}
}