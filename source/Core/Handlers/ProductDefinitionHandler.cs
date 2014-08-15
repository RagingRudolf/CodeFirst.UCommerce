using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;

using NHibernate;

using RagingRudolf.CodeFirst.UCommerce.Core.Attributes;
using RagingRudolf.CodeFirst.UCommerce.Core.Extensions;

using UCommerce.EntitiesV2;

namespace RagingRudolf.CodeFirst.UCommerce.Core.Handlers
{
	public class ProductDefinitionHandler : IHandler
	{
		private readonly ISession _session;

		public ProductDefinitionHandler(ISession session)
		{
			_session = session;
		}

		public bool CanHandle(Type type)
		{
			return type.IsDefined(typeof (ProductDefinitionAttribute), false);
		}

		public void Handle(Type type)
		{
			ProductDefinition definition = HandleDefinition(_session, type);
			ProductDefinition fieldsAdded = HandleFields(_session, type, definition);

			_session.SaveOrUpdate(fieldsAdded);
		}

		private static ProductDefinition HandleDefinition(ISession session, Type type)
		{
			var attribute = type.AssertGetCustomAttribute<ProductDefinitionAttribute>();

			string name = attribute.Name.IsNotEmpty()
				? attribute.Name
				: type.Name;

			var definition = session
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

		private static ProductDefinition HandleFields(ISession session, Type type, ProductDefinition definition)
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
					var dataType = session
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