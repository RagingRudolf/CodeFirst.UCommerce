using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using NHibernate;
using RagingRudolf.UCommerce.CodeFirst.Core.Attributes.Product;
using RagingRudolf.UCommerce.CodeFirst.Core.Attributes.Shared;
using RagingRudolf.UCommerce.CodeFirst.Core.Extensions;
using UCommerce.EntitiesV2;

namespace RagingRudolf.UCommerce.CodeFirst.Core.Creators
{
    public class ProductDefinitionCreator : ICreator
    {
        private readonly ISession _session;

        public ProductDefinitionCreator(ISession session)
        {
            if (session == null) throw new ArgumentNullException("session");
            
            _session = session;
        }

        public void CreateOrUpdate(Type type)
        {
            var definitionAttribute = type.AssertGetAttribute<DefinitionAttribute>();
            var definitionName = definitionAttribute.Name.IsNotEmpty()
                ? definitionAttribute.Name
                : type.Name;
            var definition = EnsureDefinition(definitionName);

            definition.Deleted = false;
            definition.Description = definitionAttribute.Description.IsNotEmpty()
                ? definitionAttribute.Description
                : string.Empty;

            foreach (PropertyInfo propertyInfo in type.GetPropertiesWithAttribute<ProductFieldAttribute>())
            {
                AddMultilingualDescriptions(propertyInfo, CreateFiield(propertyInfo, definition));
            }

            _session.SaveOrUpdate(definition);
        }

        private ProductDefinitionField CreateFiield(PropertyInfo propertyInfo, ProductDefinition definition)
        {
            var fieldAttribute = propertyInfo.AssertGetAttribute<ProductFieldAttribute>();
            var fieldName = fieldAttribute.Name.IsNotEmpty()
                ? fieldAttribute.Name
                : propertyInfo.Name;

            var field = definition.ProductDefinitionFields
                .EmptyIfNull()
                .SingleOrDefault(x => x.Name == fieldName);

            if (field == null)
            {
                var dataType = _session.QueryOver<DataType>()
                    .Where(x => x.TypeName == fieldAttribute.DataType)
                    .SingleOrDefault();

                field = new ProductDefinitionField
                {
                    Name = fieldName,
                    DataType = dataType
                };

                definition.AddProductDefinitionField(field);
            }

            field.DisplayOnSite = fieldAttribute.DisplayOnSite;
            field.Multilingual = fieldAttribute.Multilingual;
            field.RenderInEditor = fieldAttribute.RenderInEditor;
            field.Searchable = fieldAttribute.Searchable;
            field.SortOrder = fieldAttribute.SortOrder;
            field.Facet = fieldAttribute.Facet;
            field.IsVariantProperty = fieldAttribute.IsVariantProperty;
            return field;
        }

        private static void AddMultilingualDescriptions(PropertyInfo propertyInfo, ProductDefinitionField field)
        {
            foreach (var description in propertyInfo.GetCustomAttributes<LanguageAttribute>())
            {
                var fieldDescription = field.ProductDefinitionFieldDescriptions.
                    EmptyIfNull()
                    .SingleOrDefault(x => x.CultureCode == description.Language);

                if (fieldDescription == null)
                {
                    fieldDescription = new ProductDefinitionFieldDescription
                    {
                        CultureCode = description.Language
                    };

                    field.AddProductDefinitionFieldDescription(fieldDescription);
                }

                fieldDescription.DisplayName = description.DisplayName;
                fieldDescription.Description = description.Description;
            }
        }

        private ProductDefinition EnsureDefinition(string definitionName)
        {
            var definition = _session.QueryOver<ProductDefinition>()
                .Fetch(x => x.ProductDefinitionFields).Eager
                .Where(x => x.Name == definitionName)
                .SingleOrDefault() ?? new ProductDefinition()
                {
                    Name = definitionName,
                    ProductDefinitionFields = new Collection<ProductDefinitionField>()
                };

            return definition;
        }
    }
}