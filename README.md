# CodeFirst for uCommmerce  


CodeFirst for uCommerce is a framework which aims for helping developers deploying their uCommerce solution to other environments. Example you have been working in your development environment for the last 3 months and you are ready for first deployment to your customer's test environment.

In normal cases when you have deployed your uCommerce solution you start creating changes to your category definitions or product definitions. First time you deploy you can just do a database copy but what about the second or third time?

CodeFirst for uCommerce tries to eliminate the process of creating definitions manually after a deployment by creating definitions for you.

Don't know what uCommerce is? uCommerce is a e-commerce platform build on .NET. You can find more information about it [here](http://www.ucommerce.net/ "uCommerce")


## Requirements

CodeFirst for uCommerce requires your solution running uCommerce 6.1.0.14195 at this point. Support for older versions of uCommerce will not officially be supported. But you're welcome to contribute so it can!


## Installing


Currently you have to get the source code and compile it yourself. This will change in near future. The plan is to make a Nuget package which is easily installed into almost every solution.

For time being you have to compile source yourself and reference RagingRudolf.CodeFirst.UCommerce.Core. When you have referenced your assembly add the following configuration sections to your web/app.config.

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

Initializing CodeFirst for uCommerce is very easy. Create a new class which inherits from ApplicationEventHandler in Umbraco and call CodeFirstBootstrapper.Initialize() and you're good to go.
Example:

	public class UmbracoEventHandler : ApplicationEventHandler
	{
		protected override void ApplicationStarted(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
		{
			CodeFirstBootstrapper.Initialize();
		}
	}

## My first definition

Creating definitions with CodeFirst for uCommerce is simple and I will be working on keeping it that way! The basics are that you mark a class with an attribute that you wish should be a definition and mark properties with an attribute as well if you want being created as properties for your definition. In the example below we will create a category definition with a property:

	[CategoryDefinition("Default Category 1", Description = "Description is updated")]
	public class DefaultCategoryDefinition
	{
		[DefinitionField("IsPrimaryCategoryAlt", "Number",
			DisplayOnSite = true,
			RenderInEditor = true
		)]
		public bool IsPrimaryCategory { get; set; }
	}

## The future

I have a lot of plans but it will take time to execute them all. Currently I'm working on getting the first Nuget package created and getting out there. Right after that I will be working on getting better multilingual support. Currently you can choose to make a property multilingual but you don't have the posibility to give it a name for a certain language. And last one will be coming slowly, tests! When you start using it there might show up some hidden features I need to kill. 

Happy coding!