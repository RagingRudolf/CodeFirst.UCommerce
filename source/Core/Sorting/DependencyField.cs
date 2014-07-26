using System;

namespace RagingRudolf.CodeFirst.UCommerce.Core.Sorting
{
	public class DependencyField<T>
	where T : class
	{
		public string Alias { get; private set; }
		public string[] DependsOn { get; private set; }
		public Lazy<T> Item { get; private set; }

		public DependencyField(string alias, string[] dependsOn, T item)
		{
			Alias = alias;
			DependsOn = dependsOn;
			Item = new Lazy<T>(() => item);
		}
	}
}