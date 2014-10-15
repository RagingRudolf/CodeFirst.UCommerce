using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;

using NHibernate;

using RagingRudolf.CodeFirst.UCommerce.Core.Attributes;
using RagingRudolf.CodeFirst.UCommerce.Core.Attributes.Product;
using RagingRudolf.CodeFirst.UCommerce.Core.Extensions;

using UCommerce.EntitiesV2;

namespace RagingRudolf.CodeFirst.UCommerce.Core.Handlers
{
	public class ProductDefinitionHandler : Handler<ProductDefinition>
	{
		public ProductDefinitionHandler(ISession session) 
			: base(session)
		{
		}

		public override bool CanHandle(Type type)
		{
			return type.IsDefined(typeof (ProductDefinitionAttribute), false);
		}

		protected override ProductDefinition HandleDefinition(Type type)
		{
			var attribute = type.AssertGetCustomAttribute<ProductDefinitionAttribute>();

			string name = attribute.Name.IsNotEmpty()
				? attribute.Name
				: type.Name;

			var definition = Session
				.QueryOver<ProductDefinition>()
				.Fetch(x => x.ProductDefinitionFields).Eager
				.Where(x => x.Name == name)
				.SingleOrDefault() ?? new ProductDefinition
				{
					Name = name,
				};

			definition.Deleted = false;
			definition.Description = attribute.Description.IsNotEmpty()
				? attribute.Description
				: string.Empty;

			return definition;
		}

		protected override ProductDefinition HandleFieldTypes(Type type, ProductDefinition definition)
		{
			IEnumerable<PropertyInfo> properties = type.GetAttributedProperties<ProductDefinitionFieldAttribute>();

			foreach (PropertyInfo propertyInfo in properties)
			{
				var attribute = propertyInfo.AssertGetCustomAttribute<ProductDefinitionFieldAttribute>();
				string name = attribute.Name.IsNotEmpty()
					? attribute.Name
					: propertyInfo.Name;

				var field = definition.ProductDefinitionFields
					.SingleOrDefault(x => x.Name == name);

				if (field == null)
				{
					var dataType = Session
						.QueryOver<DataType>()
						.Where(x => x.TypeName == attribute.DataType)
						.SingleOrDefault<DataType>();

					field = new ProductDefinitionField
					{
						Name = name,
						DataType = dataType,
						ProductDefinition = definition
					};

					if (definition.ProductDefinitionFields == null)
						definition.ProductDefinitionFields = new Collection<ProductDefinitionField>();

					definition.ProductDefinitionFields.Add(field);
				}

				field.DisplayOnSite = attribute.DisplayOnSite;
				field.Multilingual = attribute.Multilingual;
				field.RenderInEditor = attribute.RenderInEditor;
				field.Searchable = attribute.Searchable;
				field.SortOrder = attribute.SortOrder;
				field.Facet = attribute.Facet;
				field.IsVariantProperty = attribute.IsVariantProperty;

				IEnumerable<LanguageDescriptionAttribute> descriptions = propertyInfo.GetCustomAttributes<LanguageDescriptionAttribute>();

				foreach (var description in descriptions)
				{
					ProductDefinitionFieldDescription fieldDescription = field.ProductDefinitionFieldDescriptions
						.SingleOrDefault(x => x.CultureCode == description.Language);

					if (fieldDescription == null)
					{
						fieldDescription = new ProductDefinitionFieldDescription
						{
							ProductDefinitionField = field,
							CultureCode = description.Language,
						};

						field.AddProductDefinitionFieldDescription(fieldDescription);
					}

					fieldDescription.DisplayName = description.DisplayName;
					fieldDescription.Description = description.Description ?? string.Empty;
				}
			}

			return definition;
		}
	}
}