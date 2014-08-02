﻿using System.Configuration;

namespace RagingRudolf.CodeFirst.UCommerce.Core.Configuration
{
	public class CodeFirstConfiguration : ConfigurationSection
	{
		[ConfigurationProperty("assemblyname", IsRequired = true)]
		public string AssemblyName
		{
			get { return (string) this["assemblyname"]; }
			set { this["assemblyname"] = value; }
		}

		[ConfigurationProperty("synchronize", DefaultValue = "false")]
		public bool Synchronize
		{
			get { return (bool) this["synchronize"]; }
			set { this["synchronize"] = value; }
		}
	}
}