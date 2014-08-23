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
	public abstract class BaseDefinitionHandler<TAttribute> : Handler<Definition>
		where TAttribute : BaseDefinitionAttribute
	{
		protected BaseDefinitionHandler(ISession session) 
			: base(session)
		{
		}

		public override bool CanHandle(Type type)
		{
			return type.IsDefined(typeof(TAttribute), false);
		}
		
		protected override Definition HandleDefinition(Type type)
		{
			var attribute = type.AssertGetCustomAttribute<TAttribute>();

			string name = attribute.Name.IsNotEmpty()
				? attribute.Name
				: type.Name;

			Definition definition = Session
				.QueryOver<Definition>()
				.Fetch(x => x.DefinitionFields).Eager
				.Where(x => x.Name == name)
				.SingleOrDefault();

			if (definition == null)
			{
				string definitionTypeName = Constants.DefinitionType.CampaignItem;

				DefinitionType campaignDefinitionType = Session
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

		protected override Definition HandleFieldTypes(Type type, Definition definition)
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
					var dataType = Session
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

		public abstract string DefinitionType { get; }
	}
}