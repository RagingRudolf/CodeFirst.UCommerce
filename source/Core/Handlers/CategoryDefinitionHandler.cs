﻿using System;
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
	public class CategoryDefinitionHandler : IHandler
	{
		private readonly ISession _session;

		public CategoryDefinitionHandler(ISession session)
		{
			_session = session;
		}

		public bool CanHandle(Type type)
		{
			return type.IsDefined(typeof (CategoryDefinitionAttribute), false);
		}

		public void Handle(Type type)
		{
			Definition definition = HandleDefinition(_session, type);
			Definition fieldsAdded = HandleFieldTypes(_session, type, definition);

			_session.SaveOrUpdate(fieldsAdded);
		}

		private static Definition HandleDefinition(ISession session, Type type)
		{
			var attribute = type.AssertGetCustomAttribute<CategoryDefinitionAttribute>();

			string name = attribute.Name.IsNotEmpty()
				? attribute.Name
				: type.Name;
			
			var definition = session
				.QueryOver<Definition>()
				.Fetch(x => x.DefinitionFields).Eager
				.Where(x => x.Name == name)
				.SingleOrDefault();

			if (definition == null)
			{
				var categoryDefinitionType = session
					.QueryOver<DefinitionType>()
					.Where(x => x.Name == "Category")
					.SingleOrDefault<DefinitionType>();

				if (categoryDefinitionType == null)
					throw new InvalidOperationException(
						string.Format("Could not find definition type 'Category'"));

				definition = new Definition
				{
					Name = name,
					DefinitionType = categoryDefinitionType
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
			}

			return definition;
		}
	}
}