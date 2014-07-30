using System.Collections.Generic;
using System.Reflection;

namespace RagingRudolf.CodeFirst.UCommerce.Core.Configuration
{
	public interface IConfigurationProvider
	{
		IEnumerable<Assembly> GetAssemblies();
	}
}