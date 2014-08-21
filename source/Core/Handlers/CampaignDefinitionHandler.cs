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
	public class CampaignDefinitionHandler : IHandler
	{
		private readonly ISession _session;

		public CampaignDefinitionHandler(ISession session)
		{
			_session = session;
		}

		public bool CanHandle(Type type)
		{
			return type.IsDefined(typeof (CampaignDefinitionAttribute), false);
		}

		public void Handle(Type type)
		{
			Definition definition = HandleDefinition(_session, type);
			Definition addedFields = HandleFieldTypes(_session, type, definition);

			_session.SaveOrUpdate(addedFields);
		}

		private static Definition HandleDefinition(ISession session, Type type)
		{
			var attribute = type.AssertGetCustomAttribute<CampaignDefinitionAttribute>();

			string name = attribute.Name.IsNotEmpty()
				? attribute.Name
				: type.Name;
			
			Definition definition = session
				.QueryOver<Definition>()
				.Fetch(x => x.DefinitionFields).Eager
				.Where(x => x.Name == name)
				.SingleOrDefault();

			if (definition == null)
			{
				string definitionTypeName = Constants.DefinitionType.CampaignItem;

				DefinitionType campaignDefinitionType = session
					.QueryOver<DefinitionType>()
					.Where(x => x.Name == definitionTypeName)
					.SingleOrDefault();

				if (campaignDefinitionType == null)
					throw new InvalidOperationException(
						string.Format("Could not find definition type '{0}'", definitionTypeName));

				definition = new Definition
				{
					Name = name,
					DefinitionType = campaignDefinitionType
				};
			}

			definition.Deleted = false;
			definition.Description = attribute.Description.IsNotEmpty()
				? attribute.Description
				: string.Empty;

			return definition;
		}

		private static Definition HandleFieldTypes(ISession session, Type type, Definition definition)
		{
			IEnumerable<PropertyInfo> properties = type.GetAttributedProperties<CategoryDefinitionFieldAttribute>();

			foreach (PropertyInfo propertyInfo in properties)
			{
				var attribute = propertyInfo.AssertGetCustomAttribute<CategoryDefinitionFieldAttribute>();
				string name = attribute.Name.IsNotEmpty()
					? attribute.Name
					: propertyInfo.Name;

				var field = definition.DefinitionFields
					.SingleOrDefault(x => x.Name == name);

				if (field == null)
				{
					var dataType = session
						.QueryOver<DataType>()
						.Where(x => x.TypeName == attribute.DataType)
						.SingleOrDefault<DataType>();

					field = new DefinitionField
					{
						Name = name,
						DataType = dataType,
						Definition = definition
					};

					if (definition.DefinitionFields == null)
						definition.DefinitionFields = new Collection<DefinitionField>();

					definition.DefinitionFields.Add(field);
				}

				field.DisplayOnSite = attribute.DisplayOnSite;
				field.Multilingual = attribute.Multilingual;
				field.RenderInEditor = attribute.RenderInEditor;
				field.Searchable = attribute.Searchable;
				field.SortOrder = attribute.SortOrder;
				field.DefaultValue = attribute.DefaultValue;

				IEnumerable<LanguageDescriptionAttribute> descriptions = propertyInfo.GetCustomAttributes<LanguageDescriptionAttribute>();

				foreach (var description in descriptions)
				{
					DefinitionFieldDescription fieldDescription = field.DefinitionFieldDescriptions
						.SingleOrDefault(x => x.CultureCode == description.Language);

					if (fieldDescription == null)
					{
						fieldDescription = new DefinitionFieldDescription
						{
							DefinitionField = field,
							CultureCode = description.Language,
						};

						field.AddDescription(fieldDescription);
					}

					fieldDescription.DisplayName = description.DisplayName;
					fieldDescription.Description = description.Description ?? string.Empty;
				}
			}

			return definition;
		}
	}
}