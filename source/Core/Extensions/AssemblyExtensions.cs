using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace RagingRudolf.UCommerce.CodeFirst.Core.Extensions
{
    public static class AssemblyExtensions
    {
        public static IEnumerable<Type> GetLoadableTypes(this Assembly assembly)
        {
            if (assembly == null) throw new ArgumentNullException(nameof(assembly));

            try
            {
                return assembly.GetTypes().ToList();
            }
            catch (ReflectionTypeLoadException e)
            {
                return e.Types.Where(type => type != null).ToList();
            }
        }
    }
}