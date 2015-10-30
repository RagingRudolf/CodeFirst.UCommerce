# CodeFirst for uCommmerce  


CodeFirst for uCommerce is a framework which aims for helping developers deploying their uCommerce solution to other environments. 
Example you have been working in your development environment for the last 3 months and you are ready for first deployment to your customer's test environment.

In normal cases when you have deployed your uCommerce solution you start creating changes to your category definitions or product definitions. 
First time you deploy you can just do a database copy but what about the second or third time?

CodeFirst for uCommerce tries to eliminate the process of creating definitions manually after a deployment by creating definitions for you.

Don't know what uCommerce is? 
uCommerce is a e-commerce platform build on .NET. 
You can find more information about it [here](http://www.ucommerce.net/ "uCommerce")


## Requirements

CodeFirst for uCommerce requires your solution running uCommerce 6.7.5.15219 at this point. 
Support for older versions of uCommerce will not officially be supported. 
But you're welcome to contribute so it can!


## Installing

A nuget package is now available at [nuget.org](http://www.nuget.org/packages/RagingRudolf.CodeFirst.UCommerce.Core/ "Nuget"). 
You can install it with using Package Manager Console by running command

	Install-Package RagingRudolf.CodeFirst.UCommerce.Core

Or if you are looking at a feature which is not available in nuget yet you can download the source code and compile a version yourself.

### AssemblyScan or Configuration by web.config

When you're going to use CodeFirst for uCommerce, you will have to decide whether CodeFirst will look them up by assembly scan or by manually specifying an assembly 
to look for in a configuration section in web.config.

If you're going with manually specifying an assembly you have to add the following to your web.config

	<sectionGroup name="RagingRudolf">
		<section name="CodeFirst" type="RagingRudolf.CodeFirst.UCommerce.Core.Configuration.CodeFirstConfiguration" />
	</sectionGroup>

	<RagingRudolf>
		<CodeFirst synchronize="<# true/false #>" assemblyname="<# AssemblyName #>" />
	</RagingRudolf>

Replace the value in between <# #> according to your project.

synchronize: Tells CodeFirst whether it should synchronize definitions.

assemblyname: In which assembly should CodeFirst be looking for definitions?

### Initializing CodeFirst in Umbraco 6/7

Initializing CodeFirst for uCommerce is very easy. Create a new class which inherits from ApplicationEventHandler in Umbraco and 
call CodeFirstBootstrapper.Initialize() and you're good to go.

Example by using specified assembly:

	public class UmbracoEventHandler : ApplicationEventHandler
	{
		protected override void ApplicationStarted(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
		{
			new AssemblyByConfigurationBootstrap().Initialize();
		}
	}

Example by using assembly scan:

    	public class UmbracoEventHandler : ApplicationEventHandler
	    {
		    protected override void ApplicationStarted(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
		    {
			    new AssemblyScanBootstrap().Initialize();
		    }
	    }

## Your first definition

Creating definitions with CodeFirst for uCommerce is simple and I will be working on keeping it that way! The basics are that you mark a class with an attribute that you wish should be a definition and mark properties with an attribute as well if you want being created as properties for your definition. In the example below we will create a category definition with a property:

	[Definition((BuiltInDefinitionType.Category, "Default Category 1", Description = "Description is updated")]
	public class DefaultCategoryDefinition
	{
		[Field("IsPrimaryCategoryAlt", Constants.BuiltInDataTypes.Number, DisplayOnSite = true, RenderInEditor = true)]
		public bool IsPrimaryCategory { get; set; }
	}

## Creating descriptions for multiple languages

Now we have create a basic category definition and that is quite fine. Though now a customer want to support multiple languages so our definition needs some descriptions for each language. 
Defining a description and display name for a language is done by setting a LanguageDescription attribute on a property on your definition. For each language you want to give a description you create a LanguageDescription attribute. 

Below can you see how it will look like if we use our code from before.

	[Definition(BuiltInDefinitionType.Category, "Default Category 1", Description = "Description is updated")]
	public class DefaultCategoryDefinition
	{
		[Field("IsPrimaryCategoryAlt", Constants.BuiltInDataTypes.Number, Multilingual = true, DisplayOnSite = true, RenderInEditor = true)]
		[Language("en-US", "Primary category", Description = "This is primary category for a product.")]
		[Language("da-DK", "Primær kategori", Description = "Dette er den primære kategori for et produkt.")]
		public bool IsPrimaryCategory { get; set; }
	}


## The future

Next thing I plan in my pipeline is better documenation. Documentation is also the hardest part to do. Mostly because it's not creating feature but as a developer myself I know
that good documentation is important. So I will work on getting some documentation that go more into depths and more real world near examples.

And I might look into performance enhancements but at this point I'm not sure how much gain it will give.

Happy coding!