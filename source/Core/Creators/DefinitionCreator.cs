using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using NHibernate;
using RagingRudolf.UCommerce.CodeFirst.Core.Attributes;
using RagingRudolf.UCommerce.CodeFirst.Core.Extensions;
using UCommerce.EntitiesV2;

namespace RagingRudolf.UCommerce.CodeFirst.Core.Creators
{
    public class DefinitionCreator : IDefinitionCreator
    {
        private readonly ISession _session;

        public DefinitionCreator(ISession session)
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
            var definition = _session.QueryOver<Definition>()
                .Fetch(x => x.DefinitionFields).Eager
                .Where(x => x.Name == definitionName)
                .SingleOrDefault();

            definition = EnsureDefinition(definition, definitionAttribute, definitionName);
            definition.Deleted = false;
            definition.Description = definitionAttribute.Description.IsNotEmpty()
                ? definitionAttribute.Description
                : string.Empty;

            foreach (var propertyInfo in type.GetPropertiesWithAttribute<FieldAttribute>())
            {
                AddMultilingualDescriptions(propertyInfo, CreateField(propertyInfo, definition));
            }
        }

        private static void AddMultilingualDescriptions(PropertyInfo propertyInfo, DefinitionField field)
        {
            foreach (var language in propertyInfo.GetCustomAttributes<LanguageAttribute>())
            {
                var fieldDescription = field.DefinitionFieldDescriptions
                    .EmptyIfNull()
                    .SingleOrDefault(x => x.CultureCode == language.Language);

                if (fieldDescription == null)
                {
                    fieldDescription = new DefinitionFieldDescription
                    {
                        CultureCode = language.Language
                    };
                    field.AddDescription(fieldDescription);
                }

                fieldDescription.DisplayName = language.DisplayName;
                fieldDescription.Description = language.Description ?? string.Empty;
            }
        }

        private DefinitionField CreateField(PropertyInfo propertyInfo, Definition definition)
        {
            var propertyAttribute = propertyInfo.AssertGetAttribute<FieldAttribute>();
            var fieldName = propertyAttribute.Name.IsNotEmpty()
                ? propertyAttribute.Name
                : propertyInfo.Name;

            var field = definition.DefinitionFields
                .EmptyIfNull()
                .SingleOrDefault(x => x.Name == fieldName);

            if (field == null)
            {
                var dataType = _session.QueryOver<DataType>()
                    .Where(x => x.TypeName == propertyAttribute.DataType)
                    .SingleOrDefault();

                field = new DefinitionField
                {
                    Name = fieldName,
                    DataType = dataType
                };

                if (definition.DefinitionFields == null)
                    definition.DefinitionFields = new Collection<DefinitionField>();
                definition.AddDefinitionField(field);
            }

            field.DisplayOnSite = propertyAttribute.DisplayOnSite;
            field.Multilingual = propertyAttribute.Multilingual;
            field.RenderInEditor = propertyAttribute.RenderInEditor;
            field.Searchable = propertyAttribute.Searchable;
            field.SortOrder = propertyAttribute.SortOrder;
            field.DefaultValue = propertyAttribute.DefaultValue;

            return field;
        }

        private Definition EnsureDefinition(Definition definition, DefinitionAttribute attribute, string name)
        {
            if (definition == null)
            {
                string definitionTypeName = attribute.DefinitionType.ToString();

                var definitionType = _session.QueryOver<DefinitionType>()
                    .Where(x => x.Name == definitionTypeName)
                    .SingleOrDefault();

                if (definitionType == null)
                    throw new InvalidOperationException(
                        string.Format("Could not find definition type '{0}'", definitionTypeName));

                definition = new Definition
                {
                    Name = name,
                    DefinitionType = definitionType
                };
            }
            return definition;
        }
    }
}